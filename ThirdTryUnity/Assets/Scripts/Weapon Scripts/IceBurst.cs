using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBurst : MonoBehaviour {
  
    public GameObject _iceBurst;

    private void Awake()
    {
       
    }
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer.Equals(12))
        {
            collision.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
