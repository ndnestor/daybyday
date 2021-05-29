using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Registry {
    public class ValueRegistry : MonoBehaviour, IGameInstance {
        public ValueRegistryObject registryObject;


        private void Awake() {
            MainInstances.Add(this);
        }

        public bool CleanListOnStart = false;

        private void Start() {
            if (CleanListOnStart) {
                registryObject.CleanList();
            }
            registryObject.Start();
        }

        public void Set(string id, int value) {
            registryObject.Set(id, value);
        }

        public int Get(string id) {
            return registryObject.Get(id);
        }
    }
}