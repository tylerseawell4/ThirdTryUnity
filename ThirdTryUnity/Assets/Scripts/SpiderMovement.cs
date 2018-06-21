using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    private float _rotateSpeed = 1.5f;
    private float _radius = 2.1f;
    private Vector2 _centre;
    private float _angle;
    public bool _changeMovement;
   // private bool _shouldDieFromBeingOffSceen;

    private void Start()
    {
        _centre = transform.position;
    }

    private void Update()
    {
        var screenHeightTop = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
        var screenHeightBottom = Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y;
        if (_changeMovement)
        {
            var check1 = gameObject.transform.position.y + 35;
            var check2 = gameObject.transform.position.y - 35;
            if (check1 < screenHeightTop || check2 > screenHeightBottom)
                Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!_changeMovement)
        {
            _angle += _rotateSpeed * Time.deltaTime;

            var offset = new Vector2(Mathf.Sin(_angle), Mathf.Cos(_angle)) * _radius;
            var centerOffsetVec = _centre + offset;
            transform.position = new Vector3(centerOffsetVec.x, centerOffsetVec.y, transform.position.z);
        }
        else
        {
            var up = transform.position.y + .25f;
            transform.position = new Vector3(transform.position.x, up, transform.position.z);
        }
    }
}
