using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpike : MonoBehaviour
{
    private TapManager _tapManager;
    private Rigidbody2D _myRigidBody;
    public GameObject _iceSpike;
    public int _iceSpikeTime = 2;
    private PlayerControl _playerControl;
    private Vector3 _initialSize;
    private Vector3 _endSize;
    // Use this for initialization
    void Start()
    {
        _playerControl = FindObjectOfType<PlayerControl>();
        _tapManager = FindObjectOfType<TapManager>();
        _myRigidBody = GetComponent<Rigidbody2D>();
        _initialSize = new Vector3(0, 0, 0);
        _endSize = _iceSpike.transform.localScale;
        _iceSpike.transform.localScale = new Vector3(0, 0, _iceSpike.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (_iceSpike.activeSelf)
        {
            _iceSpike.transform.localScale = Vector3.Lerp(_iceSpike.transform.localScale, _endSize, .5f);

        }
        if (_tapManager._doubleTap)
        {
            //enable the spike
            _iceSpike.SetActive(true);
            StartCoroutine("SpikeActiveTime");
            //flip back to false
            _tapManager._doubleTap = false;
        }
        else
        {
            _tapManager._doubleTap = false;
        }
    }
    IEnumerator SpikeActiveTime()
    {
        yield return new WaitForSeconds(_iceSpikeTime);
        _iceSpike.SetActive(false);
    }
}
