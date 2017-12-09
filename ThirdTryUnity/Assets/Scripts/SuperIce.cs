using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperIce : MonoBehaviour {
    public GameObject _iceOrb;
    public bool _superIceActivated;
    public float _iceOrbSpeed = .03f;
    public float _distanceToFreezeEnemyFromTop = 5;

    private TapManager _tapManager;
    private Image _superBar;
    private Transform _camera;

    private bool _shouldShake;
    private bool _superInstantiated;
    private float _leftOutterBounds;
    private float _rightOutterBounds;
    private float _topOutterBounds;
    private float _bottomOutterBounds;
    private float _calculatedDistance;
    private float _initalDuration;

    private int _rotationCounter = 0;
    private float _duration = 1.25f;
    private float _power = .1f;
    private float _slowDownAmt = 1.0f;

    // Use this for initialization
    void Start () {
        _superBar = GameObject.Find("SuperBar").GetComponent<Image>();
        _tapManager = FindObjectOfType<TapManager>();
        _camera = Camera.main.transform;
        _initalDuration = _duration;
    }

    private void FixedUpdate()
    {
        if (_tapManager._holdActivated && _superBar.fillAmount >= 1f)
        {
            _superIceActivated = true;
            _shouldShake = true;
            Handheld.Vibrate();
        }

        if (_shouldShake)
        {
            if (_duration > 0)
            {
                var shake = Camera.main.transform.localPosition + Random.insideUnitSphere * _power;
                _camera.localPosition = new Vector3(shake.x, shake.y, _camera.localPosition.z);

                _duration -= Time.deltaTime * _slowDownAmt;
            }
            else
            {
                _shouldShake = false;
                _duration = _initalDuration;
            }
        }

        _topOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        _leftOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
        _rightOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
        _bottomOutterBounds = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        _calculatedDistance = (_topOutterBounds - _distanceToFreezeEnemyFromTop);


        if (_superIceActivated)
        {
            if (_superInstantiated)
            {
                _iceOrb.transform.rotation = Quaternion.Euler(0, 0, _rotationCounter);
                _rotationCounter += 1;

                if (_iceOrb.transform.localScale.x < 2 || _iceOrb.transform.localScale.y < 2)
                {
                    _iceOrb.transform.localScale += new Vector3(_iceOrbSpeed, _iceOrbSpeed, 1);
                }

            }
            else
            { 
                _superInstantiated = true;
                _iceOrb = Instantiate(_iceOrb, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _iceOrb.transform.position.z), _iceOrb.transform.rotation);
                _iceOrb.transform.parent = transform;
            }

           
            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject obj in enemies)
            {
                if (obj.transform.position.y < _calculatedDistance)
                {
                    obj.GetComponent<Renderer>().material.color = Color.blue;
                    obj.GetComponent<EnemyController>().enabled = false;
                    obj.GetComponent<Animator>().enabled = false;
                }
            }

            
        }
    }
}
