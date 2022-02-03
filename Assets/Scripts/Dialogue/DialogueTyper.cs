using System;
using Game.Dialogue.Nodes.Misc;
using Game;
using Game.Registry;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue {
	//This will play text based on effects
	public class DialogueTyper : MonoBehaviour
	{

		[Header("Default Values")]
		[Space(2)]
		[Tooltip("Characters per second")]
		public float DefaultTextSpeed;

		private TextMeshProUGUI textUI;

		public TextMeshProUGUI NameTextUI;
		public TextMeshProUGUI AvatarTextUI;
		public TextMeshProUGUI NamelessTextUI;
		public TextMeshProUGUI nameUI;
		public TextMeshProUGUI nameAvatarUI;

		public Image IndicatorWriting;
		public Image IndicatorDone;

		public Image AvatarImage;

		private Coroutine typingCoroutine;

		private void Start() {
			Debug.Log("TextUI Started");
			textUI = NamelessTextUI;
			typingCoroutine = null;
			if(typingCoroutine != null) StopCoroutine(typingCoroutine);
		}

		/// <summary>
		/// Plays the text given with the effect and speed.
		/// </summary>
		/// <param name="TextSpeed">The amount of characters per second</param>
		public void Play(float TextSpeed, string Text, TextEffectNode.TextEffect Effect, bool clear = true) {

			if(!IsFinished()) {
				if (WrapUp) {
					ForceWrapUp();
				} else {
					Debug.LogWarning("Text was trying to play ontop of already playing text!");
					return;
				}
			}

			switch (Effect) {
				default:
					print($"Typing text '{Text}'");
					typingCoroutine = StartCoroutine(NormalType(TextSpeed, Text, clear));
					break;

			}

		}

		private TextEffectNode effectNode;

		public void SetTextEffect(TextEffectNode node) {
			effectNode = node;
		}

		private AgentNode currentAgent;

		public void SetAgent(AgentNode node) {
			currentAgent = node;
		}


		public void Play(string Text, bool clear = true) {


			if(effectNode != null) {
				Play(effectNode.TextSpeed, Text, effectNode.effect, clear);
			} else {
				Play(DefaultTextSpeed, Text, TextEffectNode.TextEffect.normal, clear);
			}
		}

		private bool WrapUp = false;

		public void ForceFinish() {
			if (!IsFinished()) {
				WrapUp = true;
			}
		}

		private void ForceWrapUp() {
			Debug.Log("Forcing wrap up " + startingText);
			currentText = startingText + TextToWrite;
			textUI.text = currentText;
			StopCoroutine(typingCoroutine);
		}

		public bool IsFinished() {
			return typingCoroutine == null;
		}

		public void SetIndicator(bool Writing) {
			if (Writing) {
				IndicatorWriting.enabled = true;
				IndicatorDone.enabled = false;
			} else {
				IndicatorWriting.enabled = false;
				IndicatorDone.enabled = true;
			}
		}

		public void SwitchTextLayout(int layout) {
			AvatarImage.enabled = false;
			switch (layout) {
				case 1:
					NameTextUI.text = textUI.text.ToString();
					textUI.enabled = false;
					textUI = NameTextUI;
					textUI.enabled = true;
					break;
				case 2:
					AvatarImage.enabled = true;
					AvatarTextUI.text = textUI.text.ToString();
					textUI.enabled = false;
					textUI = AvatarTextUI;
					textUI.enabled = true;
					break;
				default:
					NamelessTextUI.text = textUI.text.ToString();
					textUI.enabled = false;
					textUI = NamelessTextUI;
					textUI.enabled = true;
					break;
			}
		}

		public void ResetTyper() {
			currentAgent = null;
			effectNode = null;
		}

		private string currentText;
		private string startingText;
		private string TextToWrite;

		public IEnumerator NormalType(float TextSpeed, string Text, bool clear = true) {
			if(Text == "Done") {
				yield break; // NOTE: Janky setup. Hopefully does not break anything - Nathan
			}
			
			var agentRegistry = MainInstances.Get<AgentRegistry>().registryObject;

			nameUI.text = "";
			nameAvatarUI.text = "";
			//if(textUI != null) Debug.Log("Before textUI " + textUI.text);
			if (currentAgent != null) {
				var prop = agentRegistry.Get(currentAgent.AgentName);
				if (prop != null) {

					var image = agentRegistry.GetEmotion(currentAgent.AgentName, currentAgent.Emotion);
					if (image != null) {

						AvatarImage.sprite = image;
						nameAvatarUI.text = prop.name;
						SwitchTextLayout(2);
					} else {
						nameUI.text = prop.name;
						SwitchTextLayout(1);
					}

				} else {
					SwitchTextLayout(1);
				}

			} else {
				SwitchTextLayout(0);

			}
			//Debug.Log("Current textUI " + textUI.text);


			int index = 0;
			startingText = clear ? "" : textUI.text;
			currentText = startingText;
			TextToWrite = Text;

			while (index < Text.Length) {

				if (WrapUp) {
					Debug.Log("Wrapping up text: " + startingText + Text);
					currentText = startingText + Text;
					break;
				}

				textUI.text = currentText;

				currentText += Text[index];
				index++;



				yield return new WaitForSeconds(1 / TextSpeed);
			
			}

			textUI.text = currentText;
			typingCoroutine = null;
			WrapUp = false;
		}
	}


}