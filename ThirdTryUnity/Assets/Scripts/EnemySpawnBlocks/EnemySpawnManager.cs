using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public void Spawn(Dictionary<Vector3, GameObject> bugPositionDictionary)
    {
        foreach (var bugPos in bugPositionDictionary)
            Instantiate(bugPos.Value, bugPos.Key, bugPos.Value.transform.rotation);
    }
}
