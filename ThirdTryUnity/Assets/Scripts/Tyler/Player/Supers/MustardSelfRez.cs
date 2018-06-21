using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MustardSelfRez : MonoBehaviour
{
    public bool _shouldSelfRez;
    public GameObject _player;
    public GameObject _gameoverPanel;
    public GameObject _selfRezPanel;
    public Text _countDownText;
    public float _countDownTimer;
    public bool _startTimer;

    public void SelfRez()
    {
        _player.SetActive(true);
        Color color = _player.GetComponent<SpriteRenderer>().color;
        color.a = .25f;
        _player.GetComponent<SpriteRenderer>().color = color;

        _gameoverPanel.SetActive(false);
        _selfRezPanel.SetActive(false);
        _shouldSelfRez = true;
    }

    public void Update()
    {
        if (_startTimer)
        {
            _countDownTimer -= Time.deltaTime * 1f;
            if (_countDownTimer <= 0)
            {
                _startTimer = false;
                _selfRezPanel.SetActive(false);
            }

            if (_selfRezPanel.activeSelf)
            {

                _countDownText.text = Mathf.Round(_countDownTimer).ToString();
            }
        }
    }
}
