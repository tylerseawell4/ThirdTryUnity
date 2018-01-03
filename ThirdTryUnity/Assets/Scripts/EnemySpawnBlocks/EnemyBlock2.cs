using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock2 : EnemyBlockParent
{
    public DifficultyEnums Difficulty = DifficultyEnums.Easy;

    public EnemyBlock2() : base()
    {
        _bugs = new GameObject[2];
        _bugs[0] = Resources.Load("Bee") as GameObject;
        _bugs[1] = Resources.Load("fly") as GameObject;
    }
    public override Dictionary<float, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight)
    {
        if (!hitHeight)
        {
            _bugPositionDictionary.Add(cameraYPoint + 2, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint + 9, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint + 16, _bugs[1]);
        }
        else
        {
            _bugPositionDictionary.Add(cameraYPoint - 2, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint - 9, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint - 16, _bugs[1]);
        }

        return _bugPositionDictionary;
    }
}
