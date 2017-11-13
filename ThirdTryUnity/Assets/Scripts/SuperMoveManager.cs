using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMoveManager : MonoBehaviour
{
    public Image _superBar;
    private SuperMode _superMode;
    private Color _originalColor;

    private void Start()
    {
        _superMode = GameObject.Find("Player").GetComponent<SuperMode>();
        _originalColor = _superBar.color;
    }

    private void FixedUpdate()
    {
        if (_superMode._superActivated)
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
