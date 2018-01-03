using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock2 : EnemyBlockParent
{
    public DifficultyEnums Difficulty = DifficultyEnums.Easy;

    public EnemyBlock2() : base()
    {
        _bugs = new GameObject[] { Resources.Load("Bee") as GameObject, Resources.Load("Wasp") as GameObject };
    }
    public override Dictionary<Vector3, GameObject> SpawnEnemyBlock(float cameraYPoint, bool hitHeight)
    {
        if (!hitHeight)
        {
            _bugPositionDictionary.Add(new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1.5f, cameraYPoint + 5, _bugs[1].transform.position.z), _bugs[1]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint + 10, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1.5f, cameraYPoint + 5, _bugs[1].transform.position.z), _bugs[1]);
        }
        else
        {
            _bugPositionDictionary.Add(new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x + 1.5f, cameraYPoint - 5, _bugs[1].transform.position.z), _bugs[1]);
            _bugPositionDictionary.Add(new Vector3(0, cameraYPoint - 10, _bugs[0].transform.position.z), _bugs[0]);
            _bugPositionDictionary.Add(new Vector3(Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x - 1.5f, cameraYPoint - 5, _bugs[1].transform.position.z), _bugs[1]);
        }

        return _bugPositionDictionary;
    }
}
