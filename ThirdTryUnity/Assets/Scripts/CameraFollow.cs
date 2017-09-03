using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject _target;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - _target.transform.position;
        // _target = GameObject.Find("Player").transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _target.transform.position + offset;
    }
}
