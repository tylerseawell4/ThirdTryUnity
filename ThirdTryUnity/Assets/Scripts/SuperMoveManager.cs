using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperMoveManager : MonoBehaviour
{
    public Image _superBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyDropling")
        {
            var value = Math.Abs((collision.gameObject.transform.localScale.x / 100) * 2);
            IncreaseSuperBar(value);
        }
    }
    private void IncreaseSuperBar(float valueToIncreaseBy)
    {
        if (_superBar.fillAmount >= 1) return;

        var morePreciseValue = (float)Math.Round((valueToIncreaseBy * 100f), 1);
        _superBar.fillAmount += morePreciseValue / 100f;

        if (_superBar.fillAmount == 1)
            _superBar.color = Color.yellow;
    }
}
