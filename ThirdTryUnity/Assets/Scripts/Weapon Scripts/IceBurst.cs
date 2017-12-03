using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBurst : MonoBehaviour {
    public bool _isBursted;
    private TapManager _tapManager;
    public GameObject _iceBurst;
    private Color _inWormColor;

    private void Awake()
    {
        _inWormColor = new Color(.25f, .25f, .25f, .6f);
    }
    // Use this for initialization
    void Start()
    {
        _tapManager = FindObjectOfType<TapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_tapManager._singleTap)
        {
            if (gameObject.GetComponent<SpriteRenderer>().color == _inWormColor)
            {
               // Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -135f)).GetComponent<SpriteRenderer>().color = _inWormColor;
               // Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -45)).GetComponent<SpriteRenderer>().color = _inWormColor;
               Instantiate(_iceBurst, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<SpriteRenderer>().color = _inWormColor;
               // Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<SpriteRenderer>().color = _inWormColor;
                //Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<SpriteRenderer>().color = _inWormColor;
            }
            else
            {
                //Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -135f));
               // Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -90));
                Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, -45));
                Instantiate(_iceBurst, transform.position, Quaternion.identity);
                Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 45));
              //  Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 90));
               // Instantiate(_iceBurst, transform.position, Quaternion.Euler(0, 0, 135));
            }

            _isBursted = true;
            _tapManager._singleTap = false;
            Destroy(gameObject);
        }
        else
        {
            _isBursted = false;
        }
    }
}
