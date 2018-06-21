using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectPooling : MonoBehaviour {
    public static ObjectPooling _current;
    [Tooltip("The Game Object we want to load on startup")]
    public GameObject _pooledObject;
    [Tooltip("How many game objects we want to instantiate on startup")]
    public int _pooledAmount = 5;
    [Tooltip("Determines if the number of pooled game objects can grow or not")]
    public bool _willGrow = true;

    List<GameObject> _pooledObjects;

    private void Awake()
    {
        _current = this;
    }

    void Start () {
        //creating a new list for the game objects that will be pooled
        _pooledObjects = new List<GameObject>();

        //instantiating the game objects, adding them to this list, and setting them inactive so they aren't visible in the game view
        for (int i = 0; i < _pooledAmount; i++)
        {
            GameObject obj = Instantiate(_pooledObject);
            obj.SetActive(false);
            _pooledObjects.Add(obj);
        }
	}

    public GameObject GetPooledObject()
    {
        //looping through the pooled object list and determining if the game object is active, if inactive it will return the game object
        for (int i = 0; i < _pooledObjects.Count; i++)
        {
            if (!_pooledObjects[i].activeInHierarchy)
            {
                return _pooledObjects[i];
            }
        }

        //if the _willGrow is set to true, we will instantiate new game objects during run time
        if(_willGrow)
        {
            GameObject obj = Instantiate(_pooledObject);
            _pooledObjects.Add(obj);
            return obj;
        }
        
        return null;
    }
}
