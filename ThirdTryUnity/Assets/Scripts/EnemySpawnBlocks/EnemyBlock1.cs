using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock1 : EnemyBlockParent
{
    public DifficultyEnums Difficulty = DifficultyEnums.Easy;
    public EnemyBlock1() : base()
    {
        _bugs = new GameObject[2];
        _bugs[0] = Resources.Load("Bee") as GameObject;
        _bugs[1] = Resources.Load("fly") as GameObject;
    }
    public override Dictionary<float, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight)
    {
        if (!hitHeight)
        {
            _bugPositionDictionary.Add(cameraYPoint + 2, _bugs[0]);
            _bugPositionDictionary.Add(cameraYPoint + 9, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint + 16, _bugs[0]);
        }
        else
        {
            _bugPositionDictionary.Add(cameraYPoint - 2, _bugs[0]);
            _bugPositionDictionary.Add(cameraYPoint - 9, _bugs[1]);
            _bugPositionDictionary.Add(cameraYPoint - 16, _bugs[0]);
        }

        return _bugPositionDictionary;
    }
}
