using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll2 : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var mat = GetComponent<MeshRenderer>().material;
        var offset = mat.mainTextureOffset;
        offset.y += Time.deltaTime / 10f;
        mat.mainTextureOffset = offset;
    }
}
