using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject _gameOverPanel;
    public Text _gameOverBounceCount;
    public Text _gameOverTotalScore;
    public Text _gameOverDeathsToDate;
    private Score _score;
    private PlayerHealth _playerHealth;
    private int _playerDeathCountToDate;
    private int _playerHighScore;
    private string _currentTotalScore;
    // Use this for initialization
    void Start()
    {
        _gameOverPanel.SetActive(false);
        _playerHealth = GetComponent<PlayerHealth>();
        _score = FindObjectOfType<Score>();
        _playerDeathCountToDate = PlayerPrefs.GetInt("Deaths");
        _playerHighScore = PlayerPrefs.GetInt("HighScore");
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //12 is enemy layer
    //    if (collision.gameObject.layer == 12 && _playerHealth.GetPlayerHealth() <= 0)
    //    {
            
    //    }
    //}

    public void Die()
    {
        _currentTotalScore = _score._totalScoreText.text.Split(' ')[2];
        PlayerPreferences();

        _gameOverBounceCount.text = _score._bounceCountText.text;      
        _gameOverDeathsToDate.text = "Deaths to Date: " + _playerDeathCountToDate.ToString();

        gameObject.SetActive(false);
        _gameOverPanel.SetActive(true);
    }

    private void PlayerPreferences()
    {
        _playerDeathCountToDate += 1;
        PlayerPrefs.SetInt("Deaths", _playerDeathCountToDate);
        PlayerPrefs.Save();
       
        if (Convert.ToInt32(_currentTotalScore) > _playerHighScore || _playerHighScore == 0)
        {
            PlayerPrefs.SetInt("HighScore", Convert.ToInt32(_currentTotalScore));
            PlayerPrefs.Save();
            _gameOverTotalScore.text = "NEW HIGH SCORE!!!! " + _currentTotalScore;
        }
        else
        {
            _gameOverTotalScore.text = _score._totalScoreText.text;
        }
    }
    IEnumerator DeathSequence()
    {
        var anim = GetComponent<Animator>();
        anim.SetInteger("State", 1);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
