using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianBug : MonoBehaviour {

    public GameObject _bugToSpawn;
    
    private float _transformX;
    private float _transformY;
    private float _transformZ;

    public void Start()
    {
        _transformX = gameObject.transform.localScale.x;
        _transformY = gameObject.transform.localScale.y;
        _transformZ = gameObject.transform.localScale.z;
    }
    public void SpawnBugs()
    {
        if (_transformX > 0.75f)
        {
            GameObject instance = Instantiate(_bugToSpawn, new Vector3(transform.position.x, transform.position.y + 3f, _bugToSpawn.transform.position.z), Quaternion.identity);
            instance.GetComponent<Collider2D>().enabled = true;

            _transformX = gameObject.transform.localScale.x - .75f;
            _transformY = gameObject.transform.localScale.y - .75f;
            _transformZ = gameObject.transform.localScale.z - .75f;
            
            instance.transform.localScale = new Vector3(_transformX, _transformY, _transformZ);            
        }
    }
}
