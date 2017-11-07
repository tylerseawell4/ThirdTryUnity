using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public Text _bounceCountText;
    public Text _totalScoreText;

    private PlayerControl _player;
    private VelocityBounce2 _playerVelocity;
    private int _bounceCount;
    private float _calculatePlayerPosition;
    private float _calculatedDistanceScore;
    private float _totalScore;

    void Start () {
        _player = FindObjectOfType<PlayerControl>();
        _playerVelocity = FindObjectOfType<VelocityBounce2>();
        _calculatePlayerPosition = Mathf.Round(_player.transform.position.y);
        _bounceCount = 1;
        _bounceCountText.text = "Bounce Count: 1";
        _totalScoreText.text = "Total Score: 0";
    }

    void FixedUpdate()
    {
        _calculatedDistanceScore = Mathf.Round(Mathf.Abs(_calculatePlayerPosition - _player.transform.position.y));
        _totalScoreText.text = "Total Score: " + _calculatedDistanceScore;
    }

    public void BounceCount()
    {
        _bounceCount += 1;
        _bounceCountText.text = "Bounce Count: " + _bounceCount;
    }

    public void ChangeCalculatingPoint(float newCalculatingHeight)
    {
        _calculatePlayerPosition = Mathf.Round(newCalculatingHeight);
    }
  
}
