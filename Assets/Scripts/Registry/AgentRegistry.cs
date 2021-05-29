using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Registry {
    public class AgentRegistry : MonoBehaviour, IGameInstance {
        public AgentRegistryObject registryObject;



        private void Awake() {
            MainInstances.Add(this);
        }

        private void Start() {
            registryObject.Start();
        }

        public void Set(string id, AgentProperties value) {
            registryObject.Set(id, value);
        }

        public AgentProperties Get(string id) {
            return registryObject.Get(id);
        }
    }
}