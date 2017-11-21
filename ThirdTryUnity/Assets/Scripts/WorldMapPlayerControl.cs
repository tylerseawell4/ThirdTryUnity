using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapPlayerControl : MonoBehaviour
{

    private Rigidbody2D _player;
    // Use this for initialization
    void Start()
    {
        _player = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
#if UNITY_ANDROID
        if (Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Ended)
        {
            if (Input.acceleration.x > .035f)
                _player.velocity = new Vector3(2f, _player.velocity.y, 0f);
            else if (Input.acceleration.x < -.035f)
                _player.velocity = new Vector3(-2f, _player.velocity.y, 0f);
            else
                _player.velocity = new Vector3(0f, _player.velocity.y, 0f);

            if (Input.acceleration.z > -.67f)
                _player.velocity = new Vector3(_player.velocity.x, -2f, 0f);
            else if (Input.acceleration.z < -.75)
                _player.velocity = new Vector3(_player.velocity.x, 2f, 0f);
            else
                _player.velocity = new Vector3(_player.velocity.x, 0f, 0f);
        }
        else
            _player.velocity = Vector3.zero;
#endif

#if UNITY_EDITOR
        if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            _player.velocity = new Vector3(5, _player.velocity.y, 0f);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            // transform.localScale = new Vector3(-1f, 1f, 1f);
            _player.velocity = new Vector3(-5, _player.velocity.y, 0f);
        }
        else if (Input.GetAxisRaw("Vertical") > 0f)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            _player.velocity = new Vector3(_player.velocity.x, 5, 0f);
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            // transform.localScale = new Vector3(-1f, 1f, 1f);
            _player.velocity = new Vector3(_player.velocity.x, -5, 0f);
        }
        else
            _player.velocity = new Vector3(0f, 0f, 0f);
#endif

        // transform.Translate(Input.acceleration.x * Time.deltaTime * 10f, -Input.acceleration.z * Time.deltaTime * 10f, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Finish")
            SceneManager.LoadScene("MainMenu");
    }
}
