using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChange : MonoBehaviour {
    public SpriteRenderer _background;
    public float _backgroundChangeYValue = 115; //I just defaulted it to 110 for testing it after the second bounce
    public float _colorChangeSpeed = 1;
    private Color _originalBackgroundColor;

	// Use this for initialization
	void Start () {
        _originalBackgroundColor = _background.color;
    }
    
    void Update()
    {
        if (transform.parent.position.y >= _backgroundChangeYValue)
        {
            _background.color = Color.Lerp(_background.color, Color.black, Time.deltaTime * _colorChangeSpeed);

        }
        else if (transform.parent.position.y < _backgroundChangeYValue)
        {
            if (_background.color == Color.black)
            {
                _background.color = Color.Lerp(Color.black, Color.white, Time.deltaTime * _colorChangeSpeed);
            }
            else if (_background.color != Color.white)
            {
                _background.color = Color.Lerp(_background.color, Color.white, Time.deltaTime * _colorChangeSpeed);
            }
        }
    }
}
