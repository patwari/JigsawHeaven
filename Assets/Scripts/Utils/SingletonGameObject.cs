using System.Collections.Generic;
using UnityEngine;

/**
 * Attach this script to any gameobject that you want to be created only once, and never destroyed throughout it's lifespan.
 */
namespace Utils
{
    public class SingletonGameObject : MonoBehaviour
    {
        private static Dictionary<string, GameObject> items = new Dictionary<string, GameObject>();

        [SerializeField] private string singletonId = "";

        private void Awake()
        {
            if (string.IsNullOrEmpty(singletonId)) throw new System.Exception("SingletonGameObject :: singletonId is empty");

            if (items.ContainsKey(singletonId))
            {
                Destroy(gameObject);
                return;
            }

            items.Add(singletonId, gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}