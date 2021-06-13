using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue {
    public class DialogueFadeIn : DialogueTransitionIn {

        public float TransitionLength = 0.5f;

        public Image image;
        
        public override IEnumerator TransitionEnumerator() {


            float curTime = 0;

            Color color = image.color;

            color.a = 0;
            image.color = color;
            while (curTime < TransitionLength) {
                curTime += Time.deltaTime;

                color.a = curTime / TransitionLength;
                image.color = color;
                yield return null;
            }


            color.a = 1;
            image.color = color;
            IsFinished = true;
            currentCor = null;





        }
    }
}