using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelModel
{
    private int _asteroidFrequency;
    private float _asteroidPositionZ;

    private int _laserAmmoMaxFrequency;
    private int _laserAmmoMinFrequency;
    private int _laserAmmoFrequency;
    private float _laserAmmoPositionZ;

    private float _roadBorder;
    private float _roadPositionZ;

    private float _playerVisibilityRadius;

    public float AsteroidPositionZ => _asteroidPositionZ;

    public float RoadBorder => _roadBorder;

    public float PlayerVisibilityRadius => _playerVisibilityRadius;

    public float RoadPositionZ => _roadPositionZ;

    public float LaserAmmoPositionZ => _laserAmmoPositionZ;

    public delegate void EnvironmentRemovingHandle(EnvironmentType type, EnvironmentView view);

    public event EnvironmentRemovingHandle OnEnvironmentRemove;

    public event Action OnGameOver;

    public event Action OnRestartGame;

    public LevelModel(int asteroidFrequency, float roadBorder, float playerVisibilityRadius, int laserAmmoMaxFrequency,
        int laserAmmoMinFrequency)
    {
        _asteroidFrequency = asteroidFrequency;
        _roadBorder = roadBorder;
        _playerVisibilityRadius = playerVisibilityRadius;
        _laserAmmoMaxFrequency = laserAmmoMaxFrequency;
        _laserAmmoMinFrequency = laserAmmoMinFrequency;
        LaserAmmoRespawnChange();
        ChangeBaseLaserAmmoPosition();
    }

    public void ChangeBaseAsteroidPosition()
    {
        _asteroidPositionZ += _asteroidFrequency;
    }

    public void ChangeBaseRoadPosition(float position)
    {
        _roadPositionZ += position;
    }

    public void ChangeBaseLaserAmmoPosition()
    {
        _laserAmmoPositionZ += _laserAmmoFrequency;
    }

    public void AsteroidRespawnIncreasing()
    {
        if (_asteroidFrequency > 0)
        {
            _asteroidFrequency--;
        }
    }

    public void LaserAmmoRespawnChange()
    {
        _laserAmmoFrequency = Random.Range(_laserAmmoMinFrequency, _laserAmmoMaxFrequency);
    }

    public void RemoveEnvironmentFromPool(EnvironmentType type, EnvironmentView view)
    {
        OnEnvironmentRemove?.Invoke(type, view);
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