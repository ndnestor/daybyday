using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Mario {
	public class Pipe {

		private readonly PipeSettings _pipeSettings;

		public enum ConnectionType {
			CmdArgs
		}

		public enum FlowDirection {
			In,
			Out,
			Bidirectional
		}

		// Pipe infrastructure
		private PipeStream _pipeIn;
		private PipeStream _pipeOut;
		private StreamReader _pipeInReader;
		private StreamWriter _pipeOutWriter;
		private readonly Timer _readPipeTimer = new Timer();
		private readonly Mutex _flowInMutex = new Mutex();
		private readonly Mutex _messageBufferMutex = new Mutex();
		private readonly List<string> _messageBuffer = new List<string>();
		
		// Constructor
		public Pipe(PipeSettings pipeSettings) {
			
			// Set pipe settings
			_pipeSettings = pipeSettings;
		}

		public Pipe() {
			
			// Set pipe settings to default values
			_pipeSettings = new PipeSettings();
		}

		// Starts the transfer of data
		public void Start() {
			
			Action startInFlow;
			Action startOutFlow;

			switch(_pipeSettings.ConnectionType) {
				case ConnectionType.CmdArgs:

					string[] cmdArgs = Environment.GetCommandLineArgs();
					
					startInFlow = () => {
						if(_pipeSettings.TargetProcess == null) {
							
							// This is for the client process
							string pipeInHandle = cmdArgs[2];
							_pipeIn = new AnonymousPipeClientStream(PipeDirection.In, pipeInHandle);
							_pipeInReader = new StreamReader(_pipeIn);
						} else {
							
							// This is for the server process
							_pipeIn = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable);
							_pipeInReader = new StreamReader(_pipeIn);
						}
						
						_readPipeTimer.Start();
					};

					startOutFlow = () => {
						if(_pipeSettings.TargetProcess == null) {
							
							// This is for the client process
							string pipeOutHandle = cmdArgs[1];
							_pipeOut = new AnonymousPipeClientStream(PipeDirection.Out, pipeOutHandle);
							_pipeOutWriter = new StreamWriter(_pipeOut);
						} else {
							
							// This is for the server process
							_pipeOut = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable);
							_pipeOutWriter = new StreamWriter(_pipeOut);
							
							_pipeSettings.TargetProcess.StartInfo.Arguments =
								$"{((AnonymousPipeServerStream)_pipeIn).GetClientHandleAsString()} " +
								$"{((AnonymousPipeServerStream)_pipeOut).GetClientHandleAsString()}";
							
							_pipeSettings.TargetProcess.Start();
						}
					};
					break;
				default:
					throw new NotImplementedException(
						$"Connection type \"{_pipeSettings.ConnectionType}\" has not been implemented yet");
			}

			// Initialize the data transfer
			switch(_pipeSettings.FlowDirection) {
				case FlowDirection.In:
					startInFlow();
					break;
				case FlowDirection.Out:
					startOutFlow();
					break;
				case FlowDirection.Bidirectional:
					startInFlow();
					startOutFlow();
					break;
			}

			// Start the read pipe timer if necessary
			FlowDirection flowDirection = _pipeSettings.FlowDirection;
			if(flowDirection == FlowDirection.Bidirectional || flowDirection == FlowDirection.In) {
				
				// Set up timer that reads pipe every FlowInterval milliseconds
				_readPipeTimer.Interval = _pipeSettings.FlowInterval;
				_readPipeTimer.Elapsed += FlowIn;
				_readPipeTimer.AutoReset = true;
			}
		}
		// Read from the pipe
		private void FlowIn(object sender, ElapsedEventArgs e) {
			
			// Prevent trying to read the pipe from another thread
			_flowInMutex.WaitOne();
		
			// Check for sync message
			string pipeMessage = _pipeInReader.ReadLine();
			if(pipeMessage != null && pipeMessage != "SYNC") {
			
				// No message was found
				_flowInMutex.ReleaseMutex();
				return;
			}
		
			// Get all messages in the pipe
			List<string> pipeMessageLines = new List<string>();
			do {
				pipeMessage = _pipeInReader.ReadLine();
				pipeMessageLines.Add(pipeMessage);
			} while(pipeMessage != null && pipeMessage != "END");
		
			// Allow pipe reading from other threads
			_flowInMutex.ReleaseMutex();
			
			// Add messages to message buffer
			if(_pipeSettings.SaveContents) {
				_messageBufferMutex.WaitOne();
				_messageBuffer.AddRange(pipeMessageLines);
				_messageBufferMutex.ReleaseMutex();
			}

			// Call callback to send read messages to end-user
			_pipeSettings.FlowInCallback(pipeMessageLines.ToArray());

		}
		
		// Returns the message buffer in a thread safe manner
		// TODO: Add timeout time using parameter
		public string[] GetContents(bool clearContents=false) {
			
			// Throw error if given an improper argument
			if(!_pipeSettings.SaveContents) {
				throw new InvalidOperationException(
					"Cannot use GetContents method on a pipe that does not save its contents." +
					" This can be fixed in the pipe settings");
			}
			
			// Lock the message buffer
			_messageBufferMutex.WaitOne();
			
			// Get the contents of the message buffer
			string[] contents = _messageBuffer.ToArray();

			// Clear the message buffer if needed
			if(clearContents) {
				_messageBuffer.Clear();
			}
			
			// Unlock the message buffer
			_messageBufferMutex.ReleaseMutex();

			return contents;
		}

		// Write to the pipe
		public void FlowOut(IList<string> messages) {
			
			// Throw error if given an improper argument
			if(messages == null) {
				throw new ArgumentNullException(nameof(messages), "Messages list cannot be null");
			} else if(!messages.Any()) {
				throw new ArgumentException("Messages list must contain at least one element");
			}
			
			// Send the SYNC message to let recipient know a new message is being sent
			_pipeOutWriter.WriteLine("SYNC");
			// TODO: Test to make sure this line isn't needed
			//_pipeOut.WaitForPipeDrain();

			// Send the message(s)
			foreach(string message in messages) {
				_pipeOutWriter.WriteLine(message);
			}
			_pipeOutWriter.WriteLine("END");
			_pipeOutWriter.Flush();
		}
		
		// Single message overload of FlowOut(string[] messages)
		public void FlowOut(string message) {
			// Throw error if given an improper argument
			if(message == null) {
				throw new ArgumentNullException(nameof(message), "Message cannot be null");
			}
			FlowOut(new string[] { message });
		}

	}
}