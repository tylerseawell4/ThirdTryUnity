using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoojeeSuit : MonoBehaviour {
    private Sprite _savedSprite;
    private SpriteRenderer _currentSprite;

    private string _playerSuit;
	// Use this for initialization
	void Start () {
        _playerSuit = PlayerPrefs.GetString("DoojeeSuit");
        _savedSprite = Resources.Load<Sprite>(_playerSuit);

        _currentSprite = gameObject.GetComponent<SpriteRenderer>();
        _currentSprite.sprite = _savedSprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = _savedSprite;
    }
}
