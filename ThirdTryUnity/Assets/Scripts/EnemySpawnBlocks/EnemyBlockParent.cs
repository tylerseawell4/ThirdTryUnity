using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBlockParent
{
    public Dictionary<float, GameObject> _bugPositionDictionary;
    public GameObject[] _bugs;

    public EnemyBlockParent()
    {
        _bugPositionDictionary = new Dictionary<float, GameObject>();
    }

    public void ClearDictionary()
    {
        _bugPositionDictionary.Clear();
    }

    public abstract Dictionary<float, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight);
}
