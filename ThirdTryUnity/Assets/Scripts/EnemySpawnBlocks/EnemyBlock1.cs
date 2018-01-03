using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock1 : EnemyBlockParent
{
    public DifficultyEnums Difficulty = DifficultyEnums.Medium;
    public EnemyBlock1() : base()
    {
        _bugs = new GameObject[] { Resources.Load("Bee") as GameObject, Resources.Load("fly") as GameObject };
    }
    public override Dictionary<Vector3, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight)
    {
        if (!hitHeight)
        {
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint + 2, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint + 9, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint + 17, _bugs[0].transform.position.z), _bugs[1]);
        }
        else
        {
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint - 2, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint - 9, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint - 17, _bugs[0].transform.position.z), _bugs[1]);
        }

        return _bugPositionDictionary;
    }
}
