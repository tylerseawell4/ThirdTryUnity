using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUV : MonoBehaviour
{
    public Transform _player;
    public float _parralax = 2f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var mat = GetComponent<MeshRenderer>().material;
        var offset = mat.mainTextureOffset;
        offset.x =  _player.transform.position.x / transform.localScale.x / _parralax;
        mat.mainTextureOffset = Vector3.Lerp(mat.mainTextureOffset , offset, .125f);
    }
}
