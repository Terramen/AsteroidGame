﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Counters")]
    [SerializeField] private TextMeshProUGUI _scoreCounter;
    [SerializeField] private TextMeshProUGUI _asteroidCounter;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _highScore;
    
    [Header("StartGamePanel")]
    [SerializeField] private GameObject _startGamePanel;
    
    [Header("GameOverPanel")]
    [SerializeField] private GameObject _gameOverGamePanel;
    [SerializeField] private TextMeshProUGUI _resultScoreCount;
    [SerializeField] private TextMeshProUGUI _resultAsteroidCount;
    [SerializeField] private TextMeshProUGUI _resultTimeCount;
    [SerializeField] private TextMeshProUGUI _highScoreCongratulation;
    
    [Header("Utility")]
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private PauseController _pauseController;
    
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Update a count of all parametrs
    private void Update()
    {
        _asteroidCounter.text = "Asteroids passed \n" + _scoreManager.AsteroidCount;
        _scoreCounter.text = "Score \n" + _scoreManager.Score;
        _timer.text = "Time played \n" + _scoreManager.TimeFromStart;
        _highScore.text = "Highscore \n" + _scoreManager.HighScore;
        
    }

    // Trigger when spaceship collide with asteroid
    public void SpaceshipCrush()
    {
        _pauseController.IsGameOver = true;
        _gameOverGamePanel.SetActive(true);
        if (_scoreManager.IsHighScoreBeated)
        {
            _highScoreCongratulation.gameObject.SetActive(true);
        }
        _scoreManager.SetFinalResult(_resultScoreCount, _scoreManager.Score.ToString());
        _scoreManager.SetFinalResult(_resultTimeCount, _scoreManager.TimeFromStart.ToString());
        _scoreManager.SetFinalResult(_resultAsteroidCount, _scoreManager.AsteroidCount.ToString());
    }
    
    // Save highscore and reload a game scene
    public void PlayAgain()
    {
        PlayerPrefs.SetInt("highScore",_scoreManager.HighScore);
        PlayerPrefs.Save();
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        _startGamePanel.SetActive(newGameState == GameState.Paused);
        enabled = newGameState == GameState.Gameplay;
    }

    // If player quit during the game and get a higher score that will be saved
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("highScore",_scoreManager.HighScore);
    }
}
