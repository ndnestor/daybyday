using System.Collections;
using System.Collections.Generic;
using Mario;
using UnityEngine;

public class Piano2 : MonoBehaviour {

	private PipeSettings pipeSettings;
	private Pipe pipe;

	private void InitializePiano() {
		pipeSettings = new PipeSettings();
		pipe = new Pipe();
	}

}
