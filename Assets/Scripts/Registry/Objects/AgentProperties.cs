using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Registry {

	//Textures are hardcoded in to make it easier on the inspector since dictionaries are not serializable
	[Serializable]
	[CreateAssetMenu(fileName = "Agent", menuName = "Objects/Agent")]
	public class AgentProperties : ScriptableObject {

		public string AgentName;

		public Color AgentNameColor;

		public Sprite Emotion_Neutral;
		public Sprite Emotion_Happy;
		public Sprite Emotion_Sad;
		public Sprite Emotion_Excited;
		public Sprite Emotion_Determined;
		public Sprite Emotion_Angry;

	}
}
