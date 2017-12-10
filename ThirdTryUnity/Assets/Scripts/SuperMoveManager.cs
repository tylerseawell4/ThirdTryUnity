using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMoveManager : MonoBehaviour
{
    public Image _superBar;
    private SuperKetchup _superKetchup;
    private Color _originalColor;
    private SuperIce _superIce;
    private SuperMustard _superMustard;
    public bool _mustardActivated;
    public bool _ketchupActivated;
    public bool _iceActivated;
    private void Start()
    {
        _superKetchup = GameObject.Find("Player").GetComponent<SuperKetchup>();
        _superIce = GameObject.Find("Player").GetComponent<SuperIce>();
        _superMustard = GameObject.Find("Player").GetComponent<SuperMustard>();
        _originalColor = _superBar.color;
    }

    private void FixedUpdate()
    {
        _mustardActivated = _superMustard._superActivated;
        _iceActivated = _superIce._superIceActivated;
        _ketchupActivated = _superKetchup._superActivated;

        if (_superKetchup._superActivated || _superIce._superIceActivated || _superMustard._superActivated)
            _superBar.fillAmount -= .002f;

        if (_superBar.fillAmount < 0)
            _superBar.fillAmount = 0;

        if (_superBar.fillAmount == 0)
            _superBar.color = _originalColor;

    }

    public void IncreaseSuperBar(float valueToIncreaseBy)
    {
        if (_superBar.fillAmount >= 1) return;

        var morePreciseValue = (float)Math.Round((valueToIncreaseBy * 100f), 1);
        _superBar.fillAmount += morePreciseValue / 100f;

        if (_superBar.fillAmount == 1)
            _superBar.color = Color.yellow;
    }
}
