using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMode : MonoBehaviour
{
    private TapManager _tapManager;
    public bool _superActivated;
    private bool _superInstantiated;
    private float _superAcumTime;
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

    // Use this for initialization
    void Start()
    {
        _superAcumTime = 0f;
        _tapManager = FindObjectOfType<TapManager>();
        _superBar = GameObject.Find("SuperBar").GetComponent<Image>();
    }

    void FixedUpdate()
    {
        if (_tapManager._holdActivated && _superBar.fillAmount >= 1f)
            _superActivated = true;

        if (_superActivated)
        {
            if (!_lightningCreated)
            {
                var topPos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
                _superActivationTop = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, -180));
                _superActivationTop.transform.parent = transform;

                var bottomPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
                _superActivationBottom = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.identity);
                _superActivationBottom.transform.parent = transform;

                var leftPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x;
                _superActivationLeft = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, -90));
                _superActivationLeft.transform.parent = transform;

                var rightPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
                _superActivationRight = Instantiate(_superActivationPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superActivationPrefab.transform.position.z), Quaternion.Euler(0, 0, 90));
                _superActivationRight.transform.parent = transform;

                _lightningCreated = true;
            }

            if (_lightningCreated)
            {
                if (!_superInstantiated)
                {
                    _superInstantiated = true;
                    if (_super == null)
                        _super = new GameObject();
                    _super = Instantiate(_superPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _superPrefab.transform.position.z), _superPrefab.transform.rotation);
                    _super.transform.parent = transform;
                }

                Color color = _super.GetComponent<SpriteRenderer>().color;

                if (color.a < 1)
                {
                    color.a += .0175f;
                    _super.GetComponent<SpriteRenderer>().color = color;
                }

                if (_superActivationTop.transform.localScale.y > 0)
                {
                    var yScale = _superActivationTop.transform.localScale.y;
                    yScale -= .085f;
                    _superActivationTop.transform.localScale = new Vector2(_superActivationTop.transform.localScale.x, yScale);
                }

                if (_superActivationBottom.transform.localScale.y > 0)
                {
                    var yScale = _superActivationBottom.transform.localScale.y;
                    yScale -= .085f;
                    _superActivationBottom.transform.localScale = new Vector2(_superActivationBottom.transform.localScale.x, yScale);
                }

                if (_superActivationLeft.transform.localScale.y > 0)
                {
                    var yScale = _superActivationLeft.transform.localScale.y;
                    yScale -= .085f;
                    _superActivationLeft.transform.localScale = new Vector2(_superActivationLeft.transform.localScale.x, yScale);
                }

                if (_superActivationRight.transform.localScale.y > 0)
                {
                    var yScale = _superActivationRight.transform.localScale.y;
                    yScale -= .085f;
                    _superActivationRight.transform.localScale = new Vector2(_superActivationRight.transform.localScale.x, yScale);
                }


                _superAcumTime += 1 * Time.deltaTime;

                if (_superAcumTime >= 10)
                {
                    Color color1 = _super.GetComponent<SpriteRenderer>().material.color;
                    color1.a -= .025f;
                    _super.GetComponent<SpriteRenderer>().material.color = color1;

                    var yScale = _super.transform.localScale.y;
                    var xScale = _super.transform.localScale.x;
                    yScale -= .085f;
                    xScale -= .085f;

                    _super.transform.localScale = new Vector2(xScale, yScale);
                    if (color1.a <= 0f)
                    {
                        _tapManager._holdActivated = false;
                        _superInstantiated = false;
                        _superActivated = false;
                        _lightningCreated = false;
                        _superAcumTime = 0f;
                        Destroy(_superActivationTop);
                        Destroy(_superActivationBottom);
                        Destroy(_superActivationLeft);
                        Destroy(_superActivationRight);
                        Destroy(_super);
                    }
                }
            }

        }
    }
    public void SpawnLightning(Transform enemy)
    {
        _superLightningAttack = Instantiate(_lightningPrefab,transform.position,Quaternion.identity);
        _superLightningAttack.transform.parent = transform;
        _superLightningAttack.GetComponent<LightningAttackTime>().SpawnLightning(enemy);

    }
}
