using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingScore : MonoBehaviour {
    public Animator _animator;
    public Text _text;

    private string _beetleScore = "+15";
    private string _gadFlyScore = "+20";
    private string _flyScore = "+50";
    private string _waspScore = "+10";
    private string _beeScore = "+15";

    private Score _score;
    private AnimatorClipInfo[] _animatorClip;

    void Start()
    {
        _score = FindObjectOfType<Score>();
        _animatorClip = _animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, _animatorClip[0].clip.length);
        if (_score != null)
        {
            string newString = _text.text.Replace("+", "");
            _score._floatingScoreText = Convert.ToInt32(newString);
        }
    }    

    public void SetText(string enemyName)
    {
        string scoreText = "+10";

        if (enemyName.Contains("Bee"))
        {
            scoreText = _beeScore;
        }
        else if(enemyName.Contains("GadFly"))
        {
            scoreText = _gadFlyScore;
        }
        else if (enemyName.Contains("fly"))
        {
            scoreText = _flyScore;
        }
        else if (enemyName.Contains("Wasp"))
        {
            scoreText = _waspScore;
        }
        else if (enemyName.Contains("Bee"))
        {
            scoreText = _beeScore;
        }        
        
        _text.text = scoreText;
    }
}
