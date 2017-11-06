using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameObject _gameOverPanel;
    private PlayerHealth _playerHealth;
    // Use this for initialization
    void Start()
    {
        _gameOverPanel.SetActive(false);
        _playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && _playerHealth.GetPlayerHealth() <= 0)
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
