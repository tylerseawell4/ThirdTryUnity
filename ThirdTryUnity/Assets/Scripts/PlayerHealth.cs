using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public SpriteRenderer _renderer;
    private int _healthCount;
    private Color _originalColor;
    private PlayerDeath _playerDeath;
    private SuperKetchup _SuperKetchup;
    private SuperMustard _superMustard;
    private SuperIce _superIce;
    public GameObject _super;
    private Animator _anim;
    private TapManager _tapManager;
    private GameObject _flightSuit;
    // Use this for initialization
    private void Awake()
    {
        _flightSuit = GameObject.Find("Suit");
        _tapManager = FindObjectOfType<TapManager>();
        _anim = GetComponent<Animator>();
        _healthCount = 1;
        _originalColor = _renderer.color;
        _playerDeath = GetComponent<PlayerDeath>();
        _SuperKetchup = GetComponent<SuperKetchup>();
        _superIce = GetComponent<SuperIce>();
        _superMustard = GetComponent<SuperMustard>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_superMustard._superActivated || _superIce._superIceActivated || _SuperKetchup._superActivated) return;

        if (collision.gameObject.tag == "WormTail" || collision.gameObject.tag == "WormMouth") return;

        //12 is enemy layer
        if (collision.gameObject.layer == 12 || collision.gameObject.tag == "BounceBack")
        {
            var inWormColor = new Color(.25f, .25f, .25f, .6f);
            if (gameObject.GetComponent<SpriteRenderer>().color != inWormColor)
                DecreasePlayerHeath();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_superMustard._superActivated || _superIce._superIceActivated || _SuperKetchup._superActivated) return;

        //12 is enemy layer
        if (collision.gameObject.layer == 12)
            DecreasePlayerHeath();
    }
    public void IncreasePlayerHealth()
    {
        _healthCount++;
        if (_healthCount == 2)
        {
            _renderer.color = Color.green;
        }
        else if (_healthCount == 3)
        {
            _renderer.color = Color.yellow;
        }
        else if (_healthCount == 4)
        {
            _renderer.color = Color.blue;
        }
    }
    private void DecreasePlayerHeath()
    {
        if (_SuperKetchup._superActivated || _superIce._superIceActivated) return;

        _healthCount--;

        if (_healthCount == 0)
        {
            StartCoroutine("DeathSequence");
            return;
        }
        if (_healthCount == 1)
        {
            _renderer.color = _originalColor;
        }
        else if (_healthCount == 2)
        {
            _renderer.color = Color.green;
        }
        else if (_healthCount == 3)
        {
            _renderer.color = Color.yellow;
        }
        else if (_healthCount >= 4)
        {
            _renderer.color = Color.blue;
        }
    }
    public int GetPlayerHealth()
    {
        return _healthCount;
    }

    IEnumerator DeathSequence()
    {
        _anim.SetInteger("State", 1);
        if (_flightSuit != null)
        {
            _flightSuit.transform.position = new Vector3(0, 0, 0);
        }

        yield return new WaitForSeconds(.6f);

        Color color = gameObject.GetComponent<SpriteRenderer>().color;

        color.a = 0f;
        gameObject.GetComponent<SpriteRenderer>().color = color;

        _anim.SetInteger("State", 0);

        yield return new WaitForSeconds(.6f);

        _playerDeath.Die();

        _healthCount = 1;
    }
}