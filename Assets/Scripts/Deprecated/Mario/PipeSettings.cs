using System;
using System.Diagnostics;

namespace Mario {
	public class PipeSettings {
		public int FlowInterval = 33; // In milliseconds
		public bool SaveContents = true;
		public Process TargetProcess = null;
		public Action<string[]> FlowInCallback = (string[] _) => { /* Intentionally empty */ };
		public Pipe.ConnectionType ConnectionType = Pipe.ConnectionType.CmdArgs;
		public Pipe.FlowDirection FlowDirection = Pipe.FlowDirection.Bidirectional;
	}
}