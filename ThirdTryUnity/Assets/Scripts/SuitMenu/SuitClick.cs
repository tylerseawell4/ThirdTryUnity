using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuitClick : MonoBehaviour {
    private GameObject _doojee;
    private SpriteRenderer _doojeeSprite;
    private Text _suitName;
    private Image _gameObjectImage;

    public void AnimateDoojee()
    {
        _suitName = GameObject.Find("SuitName").GetComponent<Text>();
        if (_suitName != null)
        {
            _suitName.text = gameObject.name;
        }


        _doojee = GameObject.Find("Body");
        if (_doojee != null)
        {
            _doojeeSprite = _doojee.GetComponent<SpriteRenderer>();
            _gameObjectImage = gameObject.GetComponent<Image>();
            _doojeeSprite.sprite = _gameObjectImage.sprite;
        }
    }
}
