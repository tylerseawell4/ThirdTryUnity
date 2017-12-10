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

    private GameObject _super;
    public GameObject _superPrefab;
    public GameObject _superActivationPrefab;
    private GameObject _superActivationTop;
    private GameObject _superActivationBottom;
    private GameObject _superActivationLeft;
    private GameObject _superActivationRight;
    private bool _lightningCreated;
    public GameObject _lightningPrefab;
    private GameObject _superLightningAttack;
    private Image _superBar;
    private bool _shouldShake;
    private float _duration = 1.25f;
    private float _power = .1f;
    private float _initalDuration;
    private float _slowDownAmt = 1.0f;
    private Transform _camera;
    private float _rotationAmountClockwise;
    private bool _makeSmaller;
    private float _rotationAmountCounter;

    // Use this for initialization
    void Start()
    {
        _superAcumTime = 0f;
        _tapManager = FindObjectOfType<TapManager>();
        _superBar = GameObject.Find("SuperBar").GetComponent<Image>();

        _camera = Camera.main.transform;
        _initalDuration = _duration;

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
        //if (_tapManager._holdActivated && _superBar.fillAmount >= 1f)
        //{
        //    _superActivated = true;
        //    _shouldShake = true;
        //    Handheld.Vibrate();
        //}

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

            var currentScaleFire = _fireArray[0].transform.localScale.x;
            if (!_makeSmaller)
            {
                currentScaleFire += .05f;
                if (currentScaleFire >= 4f)
                    _makeSmaller = true;
            }
            else if (currentScaleFire >= .75f)
            {
                currentScaleFire -= .035f;
            }

            for (int i = 0; i <_fireArray.Length; i++)
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

            if(_makeSmaller)
            {
                var currentScaleX = _fireWings[0].transform.localScale.x;
                var currentScaleY = _fireWings[0].transform.localScale.y;
                foreach (var firewing in _fireWings)
                {
                    Color color = firewing.GetComponent<SpriteRenderer>().color;

                    if (color.a < .6f)
                    {
                        color.a += .01f;
                        firewing.GetComponent<SpriteRenderer>().color = color;
                    }

                    if (currentScaleX <= 1)
                        currentScaleX += .01f;

                    if (currentScaleY <= 1)
                        currentScaleY += .02f;

                    firewing.transform.localScale = new Vector3(currentScaleX, currentScaleY, 1f);
                }
            }



            //if (!_lightningCreated)
            //{
            //    var topPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            //    _superActivationTop = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, -180));
            //    _superActivationTop.transform.parent = transform;

            //    var bottomPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
            //    _superActivationBottom = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.identity);
            //    _superActivationBottom.transform.parent = transform;

            //    var leftPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
            //    _superActivationLeft = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, -90));
            //    _superActivationLeft.transform.parent = transform;

            //    var rightPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            //    _superActivationRight = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, 90));
            //    _superActivationRight.transform.parent = transform;

            //    _lightningCreated = true;
            //}

            //if (_lightningCreated)
            //{
            //    if (!_superInstantiated)
            //    {
            //        _superInstantiated = true;
            //        if (_super == null)
            //            _super = new GameObject();
            //        _super = Instantiate(_superPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superPrefab.transform.position.z), _superPrefab.transform.rotation);
            //        _super.transform.parent = transform;
            //    }

            //    Color color = _super.GetComponent<SpriteRenderer>().color;

            //    if (color.a < 1)
            //    {
            //        color.a += .0175f;
            //        _super.GetComponent<SpriteRenderer>().color = color;
            //    }

            //    if (_superActivationTop.transform.localScale.y > 0)
            //    {
            //        var yScale = _superActivationTop.transform.localScale.y;
            //        yScale -= .085f;
            //        _superActivationTop.transform.localScale = new Vector2(_superActivationTop.transform.localScale.x, yScale);
            //    }

            //    if (_superActivationBottom.transform.localScale.y > 0)
            //    {
            //        var yScale = _superActivationBottom.transform.localScale.y;
            //        yScale -= .085f;
            //        _superActivationBottom.transform.localScale = new Vector2(_superActivationBottom.transform.localScale.x, yScale);
            //    }

            //    if (_superActivationLeft.transform.localScale.y > 0)
            //    {
            //        var yScale = _superActivationLeft.transform.localScale.y;
            //        yScale -= .085f;
            //        _superActivationLeft.transform.localScale = new Vector2(_superActivationLeft.transform.localScale.x, yScale);
            //    }

            //    if (_superActivationRight.transform.localScale.y > 0)
            //    {
            //        var yScale = _superActivationRight.transform.localScale.y;
            //        yScale -= .085f;
            //        _superActivationRight.transform.localScale = new Vector2(_superActivationRight.transform.localScale.x, yScale);
            //    }


            //    _superAcumTime += 1 * Time.deltaTime;

            //    if (_superAcumTime >= 10)
            //    {
            //        Color color1 = _super.GetComponent<SpriteRenderer>().material.color;
            //        color1.a -= .025f;
            //        _super.GetComponent<SpriteRenderer>().material.color = color1;

            //        var yScale = _super.transform.localScale.y;
            //        var xScale = _super.transform.localScale.x;
            //        yScale -= .085f;
            //        xScale -= .085f;

            //        _super.transform.localScale = new Vector2(xScale, yScale);
            //        if (color1.a <= 0f)
            //        {
            //            _tapManager._holdActivated = false;
            //            _superInstantiated = false;
            //            _superActivated = false;
            //            _lightningCreated = false;
            //            _superAcumTime = 0f;
            //            Destroy(_superActivationTop);
            //            Destroy(_superActivationBottom);
            //            Destroy(_superActivationLeft);
            //            Destroy(_superActivationRight);
            //            Destroy(_super);
            //        }
            //    }
            // }

        }
    }
    //public void SpawnLightning(Transform enemy)
    //{
    //    _superLightningAttack = Instantiate(_lightningPrefab, transform.position, Quaternion.identity);
    //    _superLightningAttack.transform.parent = transform;
    //    _superLightningAttack.GetComponent<LightningAttackTime>().SpawnLightning(enemy);

    //}
}
