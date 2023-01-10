using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelModel
{
    private int _asteroidCount;
    private int _asteroidFrequency;
    private float _asteroidPositionZ;

    private int _roadCount;
    private float _roadBorder;
    private float _roadPositionZ;

    private float _playerVisibilityRadius;

    public int AsteroidCount => _asteroidCount;

    public int AsteroidFrequency => _asteroidFrequency;

    public float AsteroidPositionZ => _asteroidPositionZ;

    public int RoadCount => _roadCount; 

    public float RoadBorder => _roadBorder;

    public float PlayerVisibilityRadius => _playerVisibilityRadius;

    public float RoadPositionZ => _roadPositionZ;

    public delegate void AsteroidRemovingHandle(AsteroidView asteroidView);

    public event AsteroidRemovingHandle OnAsteroidRemove;

    public delegate void RoadRemovingHandle(RoadView roadView);

    public event RoadRemovingHandle OnRoadRemove;

    public event Action OnGameOver;

    public event Action OnRestartGame;

    // TODO Event to not create SpaceshipModel in LevelController

    public LevelModel(int asteroidCount, int asteroidFrequency, int roadCount, float roadBorder,
        float playerVisibilityRadius)
    {
        _asteroidCount = asteroidCount;
        _asteroidFrequency = asteroidFrequency;
        _roadCount = roadCount;
        _roadBorder = roadBorder;
        _playerVisibilityRadius = playerVisibilityRadius;
    }

    public void ChangeBaseAsteroidPosition()
    {
        _asteroidPositionZ += _asteroidFrequency;
    }
    
    public void ChangeBaseRoadPosition(float position)
    {
        _roadPositionZ += position;
    }

    public void AsteroidRespawnIncreasing()
    {
        if (_asteroidFrequency > 0)
        {
            _asteroidFrequency--;
        }
    }

    public void RemoveAsteroidsFromPool(AsteroidView asteroidView)
    {
        OnAsteroidRemove?.Invoke(asteroidView);
    }

    public void RemoveRoadsFromPool(RoadView roadView)
    {
        OnRoadRemove?.Invoke(roadView);
    }

    public void SpaceshipCrush()
    {
        OnGameOver?.Invoke();
    }

    public void PlayAgain()
    {
        OnRestartGame?.Invoke();
    }
}