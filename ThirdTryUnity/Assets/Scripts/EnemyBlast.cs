using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlast : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + .5f)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Super")
            FindObjectOfType<SuperKetchup>().SpawnLightning(transform);

        Destroy(gameObject);
    }
}
