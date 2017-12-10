using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMustard : MonoBehaviour
{
    private TapManager _tapManager;
    public bool _superActivated;
    private bool _superInstantiated;
    private float _superAcumTime;
    public GameObject[] _fireArray;
    public GameObject[] _fireWings;
    public GameObject[] _fireBursts;
    public GameObject _superActivationPrefab;
    private Image _superBar;
    public bool _shouldShake;
    private float _duration = 1.5f;
    private float _power = .1f;
    private float _initalDuration;
    private float _slowDownAmt = 1.0f;
    private Transform _camera;
    private float _rotationAmountClockwise;
    private bool _makeSmallerFireArray;
    private float _rotationAmountCounter;
    private bool _makeSmallerFireBursts;

    // Use this for initialization
    void Awake()
    {
        _superAcumTime = 0f;
        _tapManager = FindObjectOfType<TapManager>();
        _superBar = GameObject.Find("SuperBar").GetComponent<Image>();

        _camera = Camera.main.transform;
        _initalDuration = _duration;
        ResetSuper();
    }

    private void ResetSuper()
    {
        foreach (var fire in _fireBursts)
        {
            fire.transform.localScale = new Vector3(1f, 1f, 1f);

            Color color = fire.GetComponent<SpriteRenderer>().color;

            color.a = 0;
            fire.GetComponent<SpriteRenderer>().color = color;
        }

        foreach (var fire in _fireArray)
        {
            fire.transform.localScale = new Vector3(0f, 0f, 1f);

            Color color = fire.GetComponent<SpriteRenderer>().color;

            color.a = 0;
            fire.GetComponent<SpriteRenderer>().color = color;
        }

        foreach (var firewing in _fireWings)
        {
            firewing.transform.localScale = new Vector3(0, .1f, 1f);
            Color color = firewing.GetComponent<SpriteRenderer>().color;

            color.a = 0;
            firewing.GetComponent<SpriteRenderer>().color = color;
        }
    }

    void FixedUpdate()
    {
        if (_tapManager._holdActivated)
        {
            _tapManager._holdActivated = false;
            _superActivated = true;
            _shouldShake = true;
            Handheld.Vibrate();
            _superActivationPrefab.SetActive(true);
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

        if (_superActivated)
        {
            if (_superAcumTime < 10f)
            {
                var currentScaleFire = _fireArray[0].transform.localScale.x;
                if (!_makeSmallerFireArray)
                {
                    currentScaleFire += .05f;
                    if (currentScaleFire >= 5f)
                        _makeSmallerFireArray = true;
                }
                else if (currentScaleFire >= .75f)
                {
                    currentScaleFire -= .05f;
                }

                for (int i = 0; i < _fireArray.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        _rotationAmountClockwise += .5f;
                        _rotationAmountCounter -= .5f;
                        _fireArray[i].transform.rotation = Quaternion.Euler(0, 0, _rotationAmountClockwise);
                        _fireArray[i].transform.localScale = new Vector3(currentScaleFire, currentScaleFire, 1f);
                    }
                    else
                    {
                        _rotationAmountCounter -= .5f;
                        _fireArray[i].transform.rotation = Quaternion.Euler(0, 0, _rotationAmountCounter);
                        _fireArray[i].transform.localScale = new Vector3(currentScaleFire, currentScaleFire, 1f);
                    }
                    Color color = _fireArray[i].GetComponent<SpriteRenderer>().color;
                    if (color.a < 1)
                    {
                        color.a += .15f;
                        _fireArray[i].GetComponent<SpriteRenderer>().color = color;
                    }
                }

                var currentScaleFire2 = _fireBursts[0].transform.localScale.x;
                if (!_makeSmallerFireBursts)
                {
                    currentScaleFire2 += .05f;
                    if (currentScaleFire2 >= 3f)
                        _makeSmallerFireBursts = true;
                }
                else if (currentScaleFire2 >= 1.5f)
                {
                    currentScaleFire2 -= .035f;
                }

                foreach (var fireBursts in _fireBursts)
                {
                    fireBursts.transform.localScale = new Vector3(currentScaleFire2, currentScaleFire2, 1f);

                    Color color = fireBursts.GetComponent<SpriteRenderer>().color;
                    if (color.a < .5f)
                    {
                        color.a += .15f;
                        fireBursts.GetComponent<SpriteRenderer>().color = color;
                    }
                }

                if (_makeSmallerFireBursts)
                {
                    var currentScaleX = _fireWings[0].transform.localScale.x;
                    var currentScaleY = _fireWings[0].transform.localScale.y;
                    foreach (var firewing in _fireWings)
                    {
                        Color color = firewing.GetComponent<SpriteRenderer>().color;

                        if (color.a < .75f)
                        {
                            color.a += .01f;
                            firewing.GetComponent<SpriteRenderer>().color = color;
                        }

                        if (currentScaleX <= .75)
                            currentScaleX += .01f;

                        if (currentScaleY <= .75)
                            currentScaleY += .02f;

                        firewing.transform.localScale = new Vector3(currentScaleX, currentScaleY, 1f);
                    }

                }
            }

            _superAcumTime += 1 * Time.deltaTime;

            if (_superAcumTime >= 10)
            {
                var currentScaleFire3 = _fireBursts[0].transform.localScale.x;
                currentScaleFire3 -= .04f;

                foreach (var fireBursts in _fireBursts)
                {
                    fireBursts.transform.localScale = new Vector3(currentScaleFire3, currentScaleFire3, 1f);

                    Color color = fireBursts.GetComponent<SpriteRenderer>().color;
                    if (color.a >= 0f)
                    {
                        color.a -= .04f;
                        fireBursts.GetComponent<SpriteRenderer>().color = color;
                    }
                }

                var currentScaleX = _fireWings[0].transform.localScale.x;
                var currentScaleY = _fireWings[0].transform.localScale.y;
                foreach (var firewing in _fireWings)
                {
                    Color color = firewing.GetComponent<SpriteRenderer>().color;

                    if (color.a >= 0)
                    {
                        color.a -= .01f;
                        firewing.GetComponent<SpriteRenderer>().color = color;
                    }

                    if (currentScaleX >= 0)
                        currentScaleX -= .03f;

                    if (currentScaleY >= 0)
                        currentScaleY -= .05f;

                    firewing.transform.localScale = new Vector3(currentScaleX, currentScaleY, 1f);
                }

                if (currentScaleY <= 0f)
                {
                    _makeSmallerFireArray = false;
                    _makeSmallerFireBursts = false;
                    ResetSuper();
                    _superActivationPrefab.SetActive(false);
                    _tapManager._holdActivated = false;
                    _superActivated = false;
                    _superAcumTime = 0f;
                }
            }
        }
    }
}
