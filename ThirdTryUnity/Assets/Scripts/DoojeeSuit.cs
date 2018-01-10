using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoojeeSuit : MonoBehaviour {
    private Sprite _savedSprite;
    private SpriteRenderer _currentSprite;

    private string _playerSuit;
    private string _sceneName;


	void Start()
    {
        _playerSuit = PlayerPrefs.GetString("DoojeeSuit");
        _savedSprite = Resources.Load<Sprite>(_playerSuit);
        _sceneName = SceneManager.GetActiveScene().name;


        if (_sceneName != null)
        {
            if(_sceneName == "TylerScene")
            {
                if(_playerSuit != "Default")
                {
                    _currentSprite = gameObject.GetComponent<SpriteRenderer>();
                    _currentSprite.sprite = _savedSprite;
                    gameObject.GetComponent<SpriteRenderer>().sprite = _savedSprite;
                }
            }
            else
            {
                _currentSprite = gameObject.GetComponent<SpriteRenderer>();
                _currentSprite.sprite = _savedSprite;
                gameObject.GetComponent<SpriteRenderer>().sprite = _savedSprite;
            }
        }
    }
}
