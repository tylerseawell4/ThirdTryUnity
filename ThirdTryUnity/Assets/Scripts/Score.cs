using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text _bounceCountText;
    public Text _totalScoreText;
    public float _totalScoreNumber;

    private PlayerControl _player;
    private VelocityBounce2 _playerVelocity;
    private int _bounceCount;
    private float _calculatePlayerPosition;
    private float _calculatedDistanceScore;
    private float _calculatedDistanceScoreDown;
    private float _totalScore;

    void Start()
    {
        _player = FindObjectOfType<PlayerControl>();
        _playerVelocity = FindObjectOfType<VelocityBounce2>();
        _calculatePlayerPosition = Mathf.Round(_player.transform.position.y);
        _bounceCount = 0;
        _bounceCountText.text = "Bounce Count: 0";
        _totalScoreText.text = "Total Score: 0";
    }

    void FixedUpdate()
    {
        if (_player._player.velocity.y > 0)
        {
            _calculatedDistanceScore = Mathf.Round(Mathf.Abs(_calculatePlayerPosition - _player.transform.position.y));
            var newScore = _calculatedDistanceScoreDown + _calculatedDistanceScore;
            _calculatedDistanceScore = newScore;
            _totalScoreText.text = "Total Score: " + newScore;
            _totalScoreNumber = newScore;
        }
        else  if(_player._player.velocity.y < 0)
        {
            _calculatedDistanceScoreDown = Mathf.Round(Mathf.Abs(_playerVelocity._playersExactHeight - _player.transform.position.y));
            var newScore = _calculatedDistanceScoreDown + _calculatedDistanceScore;
            _calculatedDistanceScoreDown = newScore;
            _totalScoreText.text = "Total Score: " + newScore;
            _totalScoreNumber = newScore;
        }
  
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
