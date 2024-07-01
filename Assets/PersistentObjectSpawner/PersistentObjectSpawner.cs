using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject persistentPrefabObject = null;

    private static bool wasSpawned = false;

    private void Awake()
    {
        if(wasSpawned) return;

        SpawnPersistentObject();
        wasSpawned = true;
    }

    private void SpawnPersistentObject()
    {
        if (persistentPrefabObject == null)
        {
            Debug.LogError("Persistent prefab object is not assigned.",this);
            return;
        }
        
        GameObject persistentObject = Instantiate(persistentPrefabObject);
        DontDestroyOnLoad(persistentObject);
    }
}
