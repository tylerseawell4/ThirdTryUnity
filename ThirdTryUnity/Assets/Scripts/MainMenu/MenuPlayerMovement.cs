using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerMovement : MonoBehaviour {
    public float _speed = 5;
    private Quaternion _rotation = new Quaternion(0,5,0,0);
    private Camera _camera;
    private float _leftOutterBounds;
    private float _rightOutterBounds;

    // Use this for initialization
    void Start () {
        _camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkTouch(Input.mousePosition);
        }
        if(Input.touchCount > 0 )
        {
            foreach (var touch in Input.touches)
            {                
                checkTouch(touch.position);
            }
        }

        transform.Translate(Vector3.up * Time.deltaTime * _speed);
    }


    void checkTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);      
        var hit = Physics2D.OverlapPoint(touchPos);

        if (hit)
        {
            if(hit.transform.gameObject.name == "Body")
            {
                _speed += 5;
            }              
          
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "MenuCamera")
        {
            _leftOutterBounds = _camera.ScreenToWorldPoint(new Vector2(0, 0)).x;
            _rightOutterBounds = _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            Vector3 v3Position = new Vector3(Random.Range(_leftOutterBounds, _rightOutterBounds), Random.Range(-7.0f, -5.0f),0);
            _speed = Random.Range(2, 7);
            transform.position = v3Position;
        }
    }

}
