using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;


//Maybe you have to add values before you adjust them?
//For now this will be automatic

namespace Game.Registry {


    public abstract class RegistryObject<T> : ScriptableObject {

        protected Dictionary<string, T> registry = new Dictionary<string, T>();

        [SerializeField]
        public List<ValueObjectPair> initPairList = new List<ValueObjectPair>();

        [Serializable]
        public struct ValueObjectPair {
            public string id;
            public T value;
        }
        
        private bool InitiatedPlayer = false;

        
        public virtual void Start() {
            
            //Make sures everytime the unity editor is loaded it reloads the registry
            #if UNITY_EDITOR
            
            Init();
            
            #else
            
            if (InitiatedPlayer) {
                Debug.Log("Did not need to initiate registry, continuing with what is loaded");
                return;
            }

            InitiatedPlayer = true;

            Init();
            
            #endif
        }

        protected virtual void Init() {

            Debug.Log("Initiating Registry '" + name + "'");
            for (int i = 0; i < initPairList.Count; i++) {
                var pair = initPairList[i];

                Set(pair.id, pair.value);
            }
        }

        public virtual void Set(string id, T obj) {
            registry[id.ToLower()] = obj;
        }

        public virtual T Get(string id) {

            T value;

            id = id.ToLower();

            if (registry.TryGetValue(id, out value)) {
                return value;
            } else {
                //Debug.Log("Could not find id: " + id);
                Set(id, default); //Set value to 0
                return default; //aka false
            }
        }

        public virtual bool Has(string id) {
            return registry.ContainsKey(id);
        }

        public void CleanList() {
            Debug.Log("Cleaning value registry object '" + name + "'");
            registry.Clear();
        }


    }

    [CreateAssetMenu(fileName = "ValueRegistry", menuName = "Objects/ValueRegistry")]
    public class ValueRegistryObject : RegistryObject<int> {

    }

}