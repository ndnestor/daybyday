using Game.Dialogue.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace Game.Dialogue {
	public class DialogueChoiceButtons : MonoBehaviour
	{
		[Serializable]
		public struct ButtonPackage {
			public Button button;
			public TextMeshProUGUI textUI;
		}
	
		public List<ButtonPackage> buttons = new List<ButtonPackage>();

		public void Awake() {
			ResetButtons();
		}


		public void Show(List<string> choices) {

			//Enables the amount of buttons that 
			if(choices.Count > buttons.Count) {
				Debug.LogError("Too many choices for buttons! Cutting off them!");
			}

			for(int i = 0; i < choices.Count && i < buttons.Count; i++) {

				var button = buttons[i];
				var choiceText = choices[i];

				button.button.gameObject.SetActive(true);
				button.button.onClick.RemoveAllListeners();

				int currentIndex = i;
				button.button.onClick.AddListener(delegate { OnButtonClicked(currentIndex); });

				button.textUI.text = choiceText;

			}


		}
	

		public void OnButtonClicked(int index) {
			playerChose = true;
			playerChoseIndex = index;

		}

		private bool playerChose = false;
		private int playerChoseIndex = 0;
		/// <summary>
		/// Gets the choosen index via out
		/// </summary>
		/// <param name="index"></param>
		/// <returns>True if a option has been choosen</returns>
		public bool GetChoosen(out int index) {

			if (playerChose) {

				index = playerChoseIndex;
				ResetButtons();

				return true;
			}

			index = -1;

			return false;

		}


		public void ResetButtons() {
			playerChose = false;
			playerChoseIndex = 0;

			for (int i = 0; i < buttons.Count; i++) {
				var button = buttons[i];

				button.button.gameObject.SetActive(false);
				button.button.onClick.RemoveAllListeners();

				button.textUI.text = "NO TEXT";
			}

		}
	}


}