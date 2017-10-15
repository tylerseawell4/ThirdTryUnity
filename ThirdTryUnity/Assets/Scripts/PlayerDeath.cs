using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //var player = GetComponent<Rigidbody2D>();
            //player.velocity = Vector3.zero;
            //StartCoroutine("DeathSequence");
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator DeathSequence()
    {
        var anim = GetComponent<Animator>();
        anim.SetInteger("State", 1);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
