using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private ScoreModel _scoreModel;
    private ScoreView _scoreView;

    public void Init(ScoreModel scoreModel, ScoreView scoreView)
    {
        _scoreModel = scoreModel;
        _scoreView = scoreView;
        _scoreModel.OnGameOver += GameEnding;
    }

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        _scoreModel.OnGameOver -= GameEnding;
    }

    private void Update()
    {
        _scoreModel.AddScoreByTime(Time.deltaTime);
        if (_scoreModel.IsHighScoreBeated)
        {
            _scoreModel.BeatHighScore();
        }

        _scoreModel.CheckScoreIncreasing();

        //From UIManager
        _scoreView.SetGameCounterValues(_scoreModel.AsteroidCounter.AsteroidCount, _scoreModel.Score,
            _scoreModel.TimerCounter.TimeFromStart, _scoreModel.HighScore);
    }

    public void GameEnding()
    {
        _scoreView.SetResultCounterValues(_scoreModel.IsHighScoreBeated, _scoreModel.AsteroidCounter.AsteroidCount,
            _scoreModel.Score, _scoreModel.TimerCounter.TimeFromStart);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
        _scoreView.enabled = newGameState == GameState.Gameplay;
    }
}