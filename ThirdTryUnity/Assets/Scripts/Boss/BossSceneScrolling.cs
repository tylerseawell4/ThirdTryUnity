using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSceneScrolling : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = transform.position.y - .05f;
        transform.position = new Vector3(transform.position.x, newPos);
    }
}
