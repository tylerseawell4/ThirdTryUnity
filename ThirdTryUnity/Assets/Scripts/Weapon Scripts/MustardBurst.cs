using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MustardBurst : MonoBehaviour
{
    public bool _isBursted;
    private TapManager _tapManager;
    public GameObject _mustardBlast;
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
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -135f));
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -90));
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, -45));
            Instantiate(_mustardBlast, transform.position, Quaternion.identity);
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 45));
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 90));
            Instantiate(_mustardBlast, transform.position, Quaternion.Euler(0, 0, 135));
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
