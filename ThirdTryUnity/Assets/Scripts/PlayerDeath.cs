using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject _gameOverPanel;
    // Use this for initialization
    void Start()
    {
        _gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            gameObject.SetActive(false);
            _gameOverPanel.SetActive(true);
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
