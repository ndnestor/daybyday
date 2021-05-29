using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Registry {
	public enum AgentEmotions {
		NEUTRAL, HAPPY, SAD, EXCITED, DETERMINED, ANGRY
	}



	[CreateAssetMenu(fileName = "AgentRegistry", menuName = "Objects/AgentRegistry")]
	public class AgentRegistryObject : RegistryObject<AgentProperties> {

		public List<TestValueObjectPair> agentPairs = new List<TestValueObjectPair>();

		[Serializable]
		public struct TestValueObjectPair {
			public string id;
			public AgentProperties value;
		}

		public Sprite ErrorTexture;

		public string GetName(string id) {

			var agentProperty = Get(id);
			
			if (agentProperty == null) {
				return "NO_NAME_ERR";
			}

			return agentProperty.AgentName;
		}

		public Color GetNameColor(string id) {

			var agentProperty = Get(id);


			if (agentProperty == null) {
				return Color.white;
			}

			return agentProperty.AgentNameColor;
		}

		public Sprite GetEmotion(string id, AgentEmotions emotion) {
			/*
			if (!Has(id)) {
				return ErrorTexture;
			}*/


			var agentProperty = Get(id);

			if(agentProperty == null) {
				return ErrorTexture;
			}

			switch (emotion) {
				case AgentEmotions.NEUTRAL:
					return agentProperty.Emotion_Neutral;
				case AgentEmotions.HAPPY:
					return agentProperty.Emotion_Happy;
				case AgentEmotions.SAD:
					return agentProperty.Emotion_Sad;
				case AgentEmotions.EXCITED:
					return agentProperty.Emotion_Excited;
				case AgentEmotions.DETERMINED:
					return agentProperty.Emotion_Determined;
			}

			return ErrorTexture;

		}


		protected override void Init() {
			for (int i = 0; i < agentPairs.Count; i++) {
				var agent = agentPairs[i];

				initPairList.Add(new ValueObjectPair { id = agent.id, value = agent.value }) ;
			}

			base.Init();
		}

	}
}
