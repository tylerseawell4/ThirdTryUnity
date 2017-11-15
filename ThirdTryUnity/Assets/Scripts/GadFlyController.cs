using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadFlyController : MonoBehaviour
{
    private PlayerControl _playerControl;
    public GameObject _objectToShoot;
    private float _shootTime;
    private int _shootCount;
    public Transform _shootPoint;
    private Animator _anim;
    public GameObject _enemyDroplingToSpawn;
    private Transform target;
    private float smoothSpeed = 0.125f;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _shootCount = 0;
        _shootTime = 1f;
        _playerControl = FindObjectOfType<PlayerControl>();
        target = _playerControl.transform;
        // transform.position = new Vector3(_playerControl.transform.position.x, _playerControl.transform.position.y - 5f, transform.position.z);
        transform.right = _playerControl.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    void FixedUpdate()
    {
        transform.right = _playerControl.transform.position - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, -90);

        target = _playerControl.transform;


        Vector3 desiredPosition = new Vector3(target.position.x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + .75f, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = new Vector3(smoothedPosition.x, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + .75f, transform.position.z);


        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - .5f || transform.position.y > Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y + .5f)
            _shootTime -= 1f * Time.deltaTime;

        if (_shootTime <= 0)
        {
            _shootTime = 2.5f;
            _shootCount++;

            //do shoot code
            Instantiate(_objectToShoot, _shootPoint.position, _objectToShoot.transform.rotation);
        }

        if (_shootCount == 3)
        {
            StartCoroutine("DeathSequence");
        }

    }

    IEnumerator DeathSequence()
    {
        _anim.SetInteger("State", 1);
        yield return new WaitForSeconds(.5f);

        if (gameObject.transform.localScale.x > 2f)
            _enemyDroplingToSpawn.transform.localScale = new Vector2(2.5f, 2.5f);
        else
            _enemyDroplingToSpawn.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.x);
        Instantiate(_enemyDroplingToSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _enemyDroplingToSpawn.gameObject.transform.position.z), Quaternion.identity);

        Destroy(gameObject);
    }
}
