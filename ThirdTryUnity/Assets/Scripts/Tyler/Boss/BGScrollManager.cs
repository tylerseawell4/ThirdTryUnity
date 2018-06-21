using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrollManager : MonoBehaviour
{
    public GameObject _bg;
    public GameObject _spawnLoc;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpawnPoint")
        {
            _spawnLoc = Instantiate(_bg, new Vector3(0, _spawnLoc.transform.position.y + _bg.GetComponent<SpriteRenderer>().bounds.extents.y), _bg.transform.rotation).transform.Find("SpawnLoc").gameObject;
        }
        else if (collision.tag == "DeathPoint")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
