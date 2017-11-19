using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChange : MonoBehaviour {
    public SpriteRenderer _background;
    public float _backgroundChangeYValue = 115; //I just defaulted it to 110 for testing it after the second bounce
    public float _colorChangeSpeed = .1f;
    private Color _originalBackgroundColor;
    private PlayerControl _playerControl;

    // Use this for initialization
    void Start () {
        _originalBackgroundColor = _background.color;
        _playerControl = FindObjectOfType<PlayerControl>();
    }
    
    void Update()
    {
        if (transform.parent.position.y >= _backgroundChangeYValue)
        {
            if (_playerControl._player != null)
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y > 0)
                    {
                        _background.color = Color.Lerp(_background.color, Color.black, Time.deltaTime * _colorChangeSpeed);
                    }
                }
            }
        }
        else if (transform.parent.position.y < _backgroundChangeYValue)
        {
           if (_background.color != Color.white)
            {
                _background.color = Color.Lerp(_background.color, Color.white, Time.deltaTime * _colorChangeSpeed);
            }
        }
    }
}
