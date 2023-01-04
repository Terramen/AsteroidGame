using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [Header("Counters")] 
    [SerializeField] private GameObject _counterPanel;
    
    [SerializeField] private TextMeshProUGUI _scoreCounter;
    [SerializeField] private TextMeshProUGUI _asteroidCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _highScore;

    [Header("GameOver")]
    [SerializeField] private GameObject _gameOverPanel;
    
    [SerializeField] private TextMeshProUGUI _resultScoreCount;
    [SerializeField] private TextMeshProUGUI _resultAsteroidCount;
    [SerializeField] private TextMeshProUGUI _resultTimeCount;
    [SerializeField] private TextMeshProUGUI _highScoreCongratulation;

    private void OnValidate()
    {
        
    }

    public void SetGameCounterValues(int asteroidCount, int score, float timeFromStart, int highScore)
    {
        _scoreCounter.text = "Score \n" + score;
        _asteroidCounter.text = "Asteroids passed \n" + asteroidCount;
        _timer.text = "Time played \n" + timeFromStart;
        _highScore.text = "HighScore \n" + highScore;
    }

    public void SetResultCounterValues(bool isHighScoreBeated, int asteroidCount, int score, float timeFromStart)
    {
        if (isHighScoreBeated)
        {
            _highScoreCongratulation.gameObject.SetActive(true);
        }
        _resultScoreCount.text = score.ToString();
        _resultTimeCount.text = timeFromStart.ToString();
        _resultAsteroidCount.text = asteroidCount.ToString();
    }

    public void SwitchPanelState(bool state)
    {
        _counterPanel.SetActive(state);
        _gameOverPanel.SetActive(!state);
    }
}
