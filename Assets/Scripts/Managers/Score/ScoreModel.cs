using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel
{
    private int _score;
    private int _highScore;
    private int _scoreToAdd;
    private int _scoreIncreaseSum;
    private int _scoreToIncreaseRespawn;

    private TimerCounter _timerCounter;
    private AsteroidCounter _asteroidCounter;
    private FlyingCounter _flyingCounter;

    public int Score => _score;

    public int HighScore => _highScore;

    public int ScoreToAdd => _scoreToAdd;

    public int ScoreIncreaseSum => _scoreIncreaseSum;

    public int ScoreToIncreaseRespawn => _scoreToIncreaseRespawn;

    public TimerCounter TimerCounter => _timerCounter;

    public AsteroidCounter AsteroidCounter => _asteroidCounter;

    public FlyingCounter FlyingCounter => _flyingCounter;

    public bool IsHighScoreBeated => _score > _highScore;

    public event Action OnActualScoreUpdate;

    // TODO Rename? and move this to Spaceship Model
    public delegate void GameOverEventHandler();
    
    public event GameOverEventHandler OnGameOver;


    public ScoreModel(int scoreToIncreaseRespawn, int asteroidPassScore, int asteroidFrequency,
        int regularFlyingScore, int boostFlyingScore)
    {
        // TODO Remove PlayerPrefs
        _highScore = PlayerPrefs.GetInt("highScore");
        _scoreToAdd = regularFlyingScore;
        _scoreToIncreaseRespawn = scoreToIncreaseRespawn;
        _scoreIncreaseSum = _scoreToIncreaseRespawn;
        _timerCounter = new TimerCounter();
        _asteroidCounter = new AsteroidCounter(asteroidPassScore, asteroidFrequency);
        _flyingCounter = new FlyingCounter(regularFlyingScore, boostFlyingScore);
    }

    public void AddScore(int points)
    {
        _score += points;
    }
    
    public void ChangeScoreCalculation(bool isBoosted)
    {
        _scoreToAdd = _flyingCounter.ChangeFlyingScore(isBoosted);
    }


    public void AddScoreByTime(float time)
    {
        if (_timerCounter.IsTimePassed(time))
        {
            AddScore(_scoreToAdd);
        };
    }

    public void BeatHighScore()
    {
        _highScore = _score;
    }

    public void CheckScoreIncreasing()
    {
        if (_score > _scoreIncreaseSum)
        {
            _scoreIncreaseSum += _scoreToIncreaseRespawn;
            _asteroidCounter.AsteroidRespawnIncreasing();
        }
    }

    public void SpaceshipCrush()
    {
        OnGameOver?.Invoke();
    }
}

public class TimerCounter
{
    private float _timeCounter;
    private float _timeFromStart;
    
    private const float Seconds = 1;

    public float TimeCounter => _timeCounter;

    public float TimeFromStart => _timeFromStart;

    public bool IsTimePassed(float time)
    {
        _timeFromStart += time;
        bool isTimePassed = _timeFromStart - _timeCounter > Seconds;
        if (isTimePassed)
        {
            _timeCounter++;
            return true;
        }

        return false;
    }
}

public class AsteroidCounter
{
    private int _asteroidCount;
    private int _asteroidPassScore;
    private int _asteroidFrequency;

    public int AsteroidCount => _asteroidCount;

    public int AsteroidPassScore => _asteroidPassScore;

    public int AsteroidFrequency => _asteroidFrequency;

    public AsteroidCounter(int asteroidPassScore, int asteroidFrequency, int asteroidCount = default)
    {
        _asteroidCount = asteroidCount;
        _asteroidPassScore = asteroidPassScore;
        _asteroidFrequency = asteroidFrequency;
    }

    public void AddAsteroid()
    {
        _asteroidCount += 1;
    }

    public void AsteroidRespawnIncreasing()
    {
        if (_asteroidFrequency > 0)
        {
            _asteroidFrequency--;
        }
    }
}

public class FlyingCounter
{
    private int _regularFlyingScore;
    private int _boostFlyingScore;

    public int RegularFlyingScore => _regularFlyingScore;

    public int BoostFlyingScore => _boostFlyingScore;

    public FlyingCounter(int regularFlyingScore, int boostFlyingScore)
    {
        _regularFlyingScore = regularFlyingScore;
        _boostFlyingScore = boostFlyingScore;
    }

    public int ChangeFlyingScore(bool isBoosted)
    {
        if (isBoosted)
        {
            return _boostFlyingScore;
        }

        return _regularFlyingScore;
    }
}