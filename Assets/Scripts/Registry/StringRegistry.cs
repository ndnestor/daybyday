using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Registry
{
    public class StringRegistry : MonoBehaviour, IGameInstance
    {
        public StringRegistryObject registryObject;


        private void Awake()
        {
            MainInstances.Add(this);
        }

        public bool CleanListOnStart = false;

        private void Start()
        {
            if (CleanListOnStart)
            {
                registryObject.CleanList();
            }

            registryObject.Start();
        }

        public void Set(string id, string value)
        {
            registryObject.Set(id, value);
        }

        public string Get(string id)
        {
            return registryObject.Get(id);
        }
    }
}