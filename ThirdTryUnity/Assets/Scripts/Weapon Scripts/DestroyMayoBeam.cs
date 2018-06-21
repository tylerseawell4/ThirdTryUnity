using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMayoBeam : MonoBehaviour {

    private MayoShoot _mayoShoot;
	// Use this for initialization
	void Start () {
        _mayoShoot = FindObjectOfType<MayoShoot>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            _mayoShoot.DestroyBeam();
        }
    }
}
