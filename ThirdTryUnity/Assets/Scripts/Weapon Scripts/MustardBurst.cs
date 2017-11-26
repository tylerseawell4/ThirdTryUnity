using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardBurst : MonoBehaviour
{
    public bool _isBursted;
    private TapManager _tapManager;
    public GameObject _mustardBlast;
    private Color _inWormColor;

    private void Awake()
    {
        _inWormColor = new Color(.25f, .25f, .25f, .6f);
        //if (gameObject.GetComponent<SpriteRenderer>().color == _inWormColor)
        //{
        //    gameObject.GetComponent<Collider2D>().isTrigger = false;
        //    _mustardBlast.GetComponent<SpriteRenderer>().color = _inWormColor;
        //}
    }
    // Use this for initialization
    void Start()
    {
        _tapManager = FindObjectOfType<TapManager>();
    }

    // Update is called once per frame
    void Update()
    {
       if(_tapManager._singleTap)
        {
            if (gameObject.GetComponent<SpriteRenderer>().color == _inWormColor)
            {
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -135f)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -45)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.identity).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 45)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<SpriteRenderer>().color = _inWormColor;
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 135)).GetComponent<SpriteRenderer>().color = _inWormColor;
            }
            else
            {
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -135f));
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -90));
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -45));
                Instantiate(_mustardBlast, transform.position, Quaternion.identity);
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 45));
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 90));
                Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 135));
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
