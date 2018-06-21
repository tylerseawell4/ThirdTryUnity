using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingScore : MonoBehaviour {
    public Animator _animator;
    public Text _text;
    public float _multiplyBugScaleSizeNumber = 30;

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

    public void SetText(string enemyName, Transform enemyTransform)
    {       
        float scale = enemyTransform.localScale.x;
        
        int bugCalculatedScore = (int) (scale * _multiplyBugScaleSizeNumber);

        string scoreText = "+" + bugCalculatedScore;        
        _text.text = scoreText;
    }
}
