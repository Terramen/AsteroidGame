using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{
    private ScoreModel _scoreModel;
    private ScoreView _scoreView;
    private SpaceshipModel _spaceshipModel;
    private LevelModel _levelModel;

    public void Init(ScoreModel scoreModel, ScoreView scoreView, SpaceshipModel spaceshipModel, LevelModel levelModel)
    {
        _scoreModel = scoreModel;
        _scoreView = scoreView;
        _spaceshipModel = spaceshipModel;
        _levelModel = levelModel;
        _levelModel.OnGameOver += SetFinalResult;
        _levelModel.OnRestartGame += SaveHighScoreAndRestart;
        _spaceshipModel.OnSpeedBoosted += ChangeScoreCalculation;
    }

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        _levelModel.OnGameOver -= SetFinalResult;
        _levelModel.OnRestartGame -= SaveHighScoreAndRestart;
        _spaceshipModel.OnSpeedBoosted -= ChangeScoreCalculation;
    }

    private void Update()
    {
        _scoreModel.AddScoreByTime(Time.deltaTime);
        if (_scoreModel.IsHighScoreBeated)
        {
            _scoreModel.BeatHighScore();
        }

        if (_scoreModel.CheckScoreIncreasing())
        {
            _levelModel.AsteroidRespawnIncreasing();
        }

        //From UIManager
        _scoreView.SetGameCounterValues(_scoreModel.AsteroidCounter.AsteroidCount, _scoreModel.Score,
            _scoreModel.TimerCounter.TimeFromStart, _scoreModel.HighScore);
    }

    private void SetFinalResult()
    {
        _scoreView.SetResultCounterValues(_scoreModel.IsHighScoreBeated, _scoreModel.AsteroidCounter.AsteroidCount,
            _scoreModel.Score, _scoreModel.TimerCounter.TimeFromStart);
    }

    private void SaveHighScoreAndRestart()
    {
        if (_scoreModel.IsHighScoreBeated)
        {
            PlayerPrefs.SetInt("highScore",_scoreModel.HighScore);
            PlayerPrefs.Save();   
        }
        
        // TODO Possible remove LoadScene and change it with Reset params
        SceneManager.LoadSceneAsync("SampleScene");
    }

    private void ChangeScoreCalculation(bool isBoosted)
    {
        _scoreModel.ChangeScoreCalculation(isBoosted);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
        _scoreView.enabled = newGameState == GameState.Gameplay;
    }
}