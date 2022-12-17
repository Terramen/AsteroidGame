using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Spaceship")]
    [SerializeField] private float _speed;
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _zShipMovementAngle;
    [SerializeField] private float _spaceshipTiltTime;
    [SerializeField] private float _xSpeed;

    private bool _speedBoosted;
    private bool _moveButtonPressed;
    private bool _isMoving;
    
    [Header("Utility")]
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private SmoothFollow _mainCameraFollow;

    [Header("Road")]
    [SerializeField] private Transform _roadTransform;

    public float Speed => _speed;
    
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);


        if (InputManager._instance.IsMovingLeft)
        {
            if (transform.position.x >= -_roadTransform.localScale.x / 2)
            {
                SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, _zShipMovementAngle));
                SpaceshipMoving(-_xSpeed);
            }
        }
        if (InputManager._instance.IsMovingRight)
        {
            if (transform.position.x <= _roadTransform.localScale.x / 2)
            {
                SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, -_zShipMovementAngle));
                SpaceshipMoving(_xSpeed);
            }
        }

        if (!InputManager._instance.IsMovingLeft && !InputManager._instance.IsMovingRight)
        {
            SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, 0));
        }

        if (InputManager._instance.IsShipBoosted)
        {
            if (!_speedBoosted)
            {
                _scoreManager.ChangeScoreCalculation(_scoreManager.BoostFlyingScore);
                _speed *= _speedMultiplier;
                _speedBoosted = true;
            }
            _mainCameraFollow.DistanceChangeByShipBoost(_mainCameraFollow.BoostedDistance);
        }
        else
        {
            if (_speedBoosted)
            {
                _scoreManager.ChangeScoreCalculation(_scoreManager.RegularFlyingScore);
                _speed /= _speedMultiplier;
                _speedBoosted = false; 
            }
            _mainCameraFollow.DistanceChangeByShipBoost(_mainCameraFollow.DefaultDistance);
        }

    }

    private void SpaceshipTilt(Quaternion start, Quaternion end)
    {
        transform.rotation = Quaternion.Slerp(start, end, _spaceshipTiltTime);
    }

    private void SpaceshipMoving(float speed)
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0,0 );
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
