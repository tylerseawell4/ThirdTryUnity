using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject _player;       //Public variable to store a reference to the player game object
    public Renderer _renderer;
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - _player.transform.position;
    }

    void FixedUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.

        transform.position = _player.transform.position + offset;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.25f, 3.25f), Mathf.Clamp(transform.position.y, -2.25f, 2.25f), transform.position.z);


        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -_renderer.bounds.size.x/4, _renderer.bounds.size.x/4), Mathf.Clamp(transform.position.y, -_renderer.bounds.size.y/4, _renderer.bounds.size.y/4), transform.position.z);
    }
}
