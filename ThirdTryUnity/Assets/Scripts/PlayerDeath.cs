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
    public Text _gameOverRecordBounceCount;
    public Text _gameOverRecordHighScore;
    public Text _gameOverEnemiesKilled;
    public int _enemyDeathCounter = 0;

    private Score _score;
    private PlayerHealth _playerHealth;
    private int _playerDeathCountToDate;
    private int _recordHighScore;
    private int _recordBounceCount;
    private string _currentTotalScore;
    public GameObject _selfRezPanel;
    private MustardSelfRez _selfRez;

    void Awake()
    {
        _selfRez = FindObjectOfType<MustardSelfRez>();
        _gameOverPanel.SetActive(false);
        _selfRezPanel.SetActive(false);
        _playerHealth = GetComponent<PlayerHealth>();
        _score = FindObjectOfType<Score>();
        _playerDeathCountToDate = PlayerPrefs.GetInt("Deaths");
        _recordHighScore = PlayerPrefs.GetInt("RecordHighScore");
        _recordBounceCount = PlayerPrefs.GetInt("RecordBounceCount");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        FindObjectOfType<AdManager>()._shouldShowAd = true;
    }

    public void Die()
    {
        _currentTotalScore = _score._totalScoreText.text.Split(' ')[2];
        PlayerPreferences();
        _gameOverEnemiesKilled.text += _enemyDeathCounter.ToString();
        transform.Find("IceSpikes").gameObject.SetActive(false);
        //Time.timeScale = 0;
        
        _gameOverPanel.SetActive(true);

        StartCoroutine("Wait");

        if (GetComponent<PlayerControl>()._superState == SuperEnums.Mustard)
        {
            _selfRezPanel.SetActive(true);
            _selfRez._countDownTimer = 5.9f;
            _selfRez._startTimer = true;
        }
    }

    private void PlayerPreferences()
    {
        _playerDeathCountToDate += 1;
        PlayerPrefs.SetInt("Deaths", _playerDeathCountToDate);
        PlayerPrefs.Save();

        _gameOverBounceCount.text = _score._bounceCountText.text;
        _gameOverDeathsToDate.text = "Deaths to Date: " + _playerDeathCountToDate.ToString();

        if (Convert.ToInt32(_currentTotalScore) > _recordHighScore || _recordHighScore == 0)
        {
            PlayerPrefs.SetInt("RecordHighScore", Convert.ToInt32(_currentTotalScore));
            PlayerPrefs.Save();
            _gameOverTotalScore.text = "NEW HIGH SCORE!!!!     " + _currentTotalScore;
            _gameOverRecordHighScore.text += _currentTotalScore.ToString();
        }
        else
        {
            _gameOverTotalScore.text = _score._totalScoreText.text;
            _gameOverRecordHighScore.text += _recordHighScore.ToString();
        }


        if(_score._bounceCount > _recordBounceCount || _recordBounceCount == 0)
        {
            PlayerPrefs.SetInt("RecordBounceCount", _score._bounceCount);
            PlayerPrefs.Save();
            _gameOverRecordBounceCount.text += _score._bounceCount.ToString();
        }
        else
        {
            _gameOverRecordBounceCount.text += _recordBounceCount.ToString();
        }


    }
}
