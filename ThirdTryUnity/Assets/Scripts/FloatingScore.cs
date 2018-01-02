using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingScore : MonoBehaviour {
    public Animator _animator;

    private AnimatorClipInfo[] _animatorClip;
    private Text _text;


    void Start()
    {
        _animatorClip = _animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, _animatorClip[0].clip.length);
        _text = _animator.GetComponent<Text>();

    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
