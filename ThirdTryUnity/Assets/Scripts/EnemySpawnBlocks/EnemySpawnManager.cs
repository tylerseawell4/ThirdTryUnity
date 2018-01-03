using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public void Spawn(Dictionary<float, GameObject> bugPositionDictionary)
    {
        foreach (var bugPos in bugPositionDictionary)
            Instantiate(bugPos.Value, new Vector3(0, bugPos.Key, bugPos.Value.transform.position.z), bugPos.Value.transform.rotation);
    }
}
