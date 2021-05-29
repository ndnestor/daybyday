using System.Collections;
using UnityEngine;

namespace Game.Dialogue {
    public abstract class DialogueTransitionIn : MonoBehaviour {

        protected Coroutine currentCor;
        
        public void StartTransition() {

            if (currentCor != null) {
                StopCoroutine(currentCor);
            }
            
            IsFinished = false;
            
            currentCor = StartCoroutine(TransitionEnumerator());
            
        }

        public void StopTransition() {
            IsFinished = true;
            if (currentCor != null) {
                StopCoroutine(currentCor);
            }
        }

        /// <summary>
        /// Handles the actual transition, make sure that make IsFinished == true
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator TransitionEnumerator() {
            yield return null;
            IsFinished = true;
            currentCor = null;
        }
        
        
        public bool IsFinished { get; protected set; }

    }
}
