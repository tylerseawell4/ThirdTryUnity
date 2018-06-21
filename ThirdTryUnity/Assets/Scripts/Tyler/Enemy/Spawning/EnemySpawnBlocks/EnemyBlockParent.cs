using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBlockParent
{
    public Dictionary<Vector3, GameObject> _bugPositionDictionary;
    public GameObject[] _bugs;

    public EnemyBlockParent()
    {
        _bugPositionDictionary = new Dictionary<Vector3, GameObject>();
    }

    public void ClearDictionary()
    {
        _bugPositionDictionary.Clear();
    }

    public abstract Dictionary<Vector3, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight);
}
