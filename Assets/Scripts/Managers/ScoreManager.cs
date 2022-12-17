using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float _timeCounter;
    private int _scoreToAdd;
    [SerializeField] private int _regularFlyingScore;
    [SerializeField] private int _boostFlyingScore;
    [SerializeField] private int _asteroidPassScore;
    [SerializeField] private int _highScore;
    [SerializeField] private int _scoreToIncreaseRespawn;
    [SerializeField] private int _asteroidFrequency;
    private float _timeFromStart;
    private int _scoreIncreaseSum;
    private int _asteroidCount;
    private int _score;
    private bool _isHighScoreBeated;
    public int RegularFlyingScore => _regularFlyingScore;
    public int BoostFlyingScore => _boostFlyingScore;
    public int AsteroidPassScore => _asteroidPassScore;
    public int Score => _score;
    public int AsteroidCount => _asteroidCount;
    public float TimeFromStart => _timeFromStart;
    public int HighScore => _highScore;
    public int AsteroidFrequency => _asteroidFrequency;

    public bool IsHighScoreBeated => _isHighScoreBeated;
    
    // Set score to add with regular flying score
    private void Awake()
    {
        _scoreIncreaseSum = _scoreToIncreaseRespawn;
        _scoreToAdd = _regularFlyingScore;
        _highScore = PlayerPrefs.GetInt("highScore");
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }
    
    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Update()
    {
        _timeFromStart += Time.deltaTime;
        
        // Every second add defined flying score
        if (_timeFromStart - _timeCounter > 1)
        {
            _timeCounter++;
            AddScore(_scoreToAdd);
        }
        
        // If player reach a previous highscore, update highscore with current score
        if (_score > _highScore)
        {
            if (!_isHighScoreBeated)
            {
                _isHighScoreBeated = true;
            }
            _highScore = _score;
            
        }

        // The longer the player plays (in a single session), the more frequently
        // should asteroids be spawned.
        if (_score > _scoreIncreaseSum)
        {
            _scoreIncreaseSum += _scoreToIncreaseRespawn;
            AsteroidRespawnIncreasing();
        }
    }

    public void AddScore(int points)
    {
        _score += points;
    }

    public void ChangeScoreCalculation(int newScore)
    {
        _scoreToAdd = newScore;
    }

    public void AddAsteroid()
    {
        _asteroidCount++;
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    // Set a final result on a game over screen
    public void SetFinalResult(TextMeshProUGUI textElement, string text)
    {
        textElement.text = text;
    }
    
    // Increase a asteroid respawn time when reach a defined score count (Can't be below zero)
    public void AsteroidRespawnIncreasing()
    {
        if (_asteroidFrequency > 0)
        {
            _asteroidFrequency--; 
        }
    }
}
