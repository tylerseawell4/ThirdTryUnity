using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMoveManager : MonoBehaviour
{
    public Image _superBar;
    private SuperKetchup _SuperKetchup;
    private Color _originalColor;
    private SuperIce _superIce;

    private void Start()
    {
        _SuperKetchup = GameObject.Find("Player").GetComponent<SuperKetchup>();
        _superIce = GameObject.Find("Player").GetComponent<SuperIce>();
        _originalColor = _superBar.color;
    }

    private void FixedUpdate()
    {
        if (_SuperKetchup._superActivated || _superIce._superIceActivated)
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
