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
    [Header("StartGamePanel")]
    [SerializeField] private GameObject _startGamePanel;
    
    [Header("GameOverPanel")]
    [SerializeField] private GameObject _gameOverGamePanel;

    [Header("Utility")]
    [SerializeField] private PauseController _pauseController;

    // TODO Remove this later
    private ScoreModel _scoreModel;

    public void Init(ScoreModel scoreModel)
    {
        _scoreModel = scoreModel;
    }

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Trigger when spaceship collide with asteroid
    public void SpaceshipCrush()
    {
        _pauseController.IsGameOver = true;
        _gameOverGamePanel.SetActive(true);
    }
    
    // Save highscore and reload a game scene
    public void PlayAgain()
    {
        if (_scoreModel.IsHighScoreBeated)
        {
            PlayerPrefs.SetInt("highScore",_scoreModel.HighScore);
            PlayerPrefs.Save();   
        }
        
        // TODO Possible remove LoadScene and change it with Reset params
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
        //PlayerPrefs.SetInt("highScore",_scoreModel.HighScore);
    }
}
