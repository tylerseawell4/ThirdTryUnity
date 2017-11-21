using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChange : MonoBehaviour {
    public SpriteRenderer _background;
    public float _backgroundChangeYValue = 115; //I just defaulted it to 110 for testing it after the second bounce
    public float _colorChangeSpeed = .1f;
    public Color _spaceBackgroundColor = new Color(0.15f, 0.15f, 0.15f, 1);
    private float _transitionDownYValue = 0;

    private Color _originalBackgroundColor;
    private PlayerControl _playerControl;
    private string _backgroundColorString;
    private string _spaceBackgroundColorString;

    // Use this for initialization
    void Start () {
        _originalBackgroundColor = _background.color;
        _playerControl = FindObjectOfType<PlayerControl>();
    }  


    void Update()
    {        
         _backgroundColorString = _background.color.ToString();
         _spaceBackgroundColorString = _spaceBackgroundColor.ToString();

        if (_backgroundColorString == _spaceBackgroundColorString)
        {
            if (_transitionDownYValue == 0)
            {
                _transitionDownYValue = transform.position.y;
            }
        }
        

        if (transform.parent.position.y >= _backgroundChangeYValue)
        {
            if (_playerControl._player != null)
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y > 0)
                    {
                        _background.color = Color.Lerp(_background.color, _spaceBackgroundColor, Time.deltaTime * _colorChangeSpeed);
                    }
                }
            }
        }


        if (_background.color != Color.white)
        {
            if (_playerControl._player != null)
            {
                if (_playerControl._player.gameObject.activeSelf)
                {
                    if (_playerControl._player.velocity.y < 0)
                    {
                        if (_transitionDownYValue == 0)
                        {
                            _background.color = Color.Lerp(_background.color, Color.white, Time.deltaTime * _colorChangeSpeed);
                        }
                        else if (transform.parent.position.y <= _transitionDownYValue)
                        {
                            _background.color = Color.Lerp(_background.color, Color.white, Time.deltaTime * _colorChangeSpeed);
                        }

                    }
                }
            }
        }
    }
}
