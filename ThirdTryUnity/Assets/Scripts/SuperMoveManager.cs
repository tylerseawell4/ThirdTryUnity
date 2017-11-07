using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMoveManager : MonoBehaviour
{

    private float _barDisplay = 0;
    private Vector2 _pos = new Vector2(0, Screen.height - (Screen.height * .05f));
    private Vector2 _size = new Vector2(Screen.width, (Screen.height * .05f));
    public string _percentageString = "0%";
    public Texture2D _progressBarFull;
    private string _fullString;
    GUIStyle _style = new GUIStyle();

    private void Start()
    {
        _style.fontSize = (int)(Screen.height * .05);
        _style.alignment = TextAnchor.MiddleCenter;
    }
    void OnGUI()
    {

        // draw the background:
        GUI.color = Color.red;
        GUI.BeginGroup(new Rect(_pos.x, _pos.y, _size.x, _size.y), _percentageString, _style);
        GUI.Box(new Rect(0, 0, _size.x, _size.y), _progressBarFull);

        // draw the filled-in part:
        GUI.BeginGroup(new Rect(0, 0, _size.x * _barDisplay, _size.y), _fullString, _style);
        GUI.Box(new Rect(0, 0, _size.x, _size.y), _progressBarFull);
        GUI.EndGroup();

        GUI.EndGroup();

    }

    void Update()
    {
        // for this example, the bar display is linked to the current time,
        // however you would set this value based on your desired display
        // eg, the loading progress, the player's health, or whatever.
       // _barDisplay = Time.time * 0.05f;
    }
    public void IncreaseSuperBar(float valueToIncreaseBy)
    {
        if (_barDisplay >= 1) return;

        _barDisplay += valueToIncreaseBy;
        _percentageString = _barDisplay * 100 + "%";
        if (_barDisplay >= 1)
        {
            _percentageString = "";
            _fullString = "SUPER FULL";
        }
    }
}
