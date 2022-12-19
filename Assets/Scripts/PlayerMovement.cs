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
    [SerializeField] private SmoothFollow _mainCameraFollow;

    [Header("Road")]
    [SerializeField] private Transform _roadTransform;

    private ScoreModel _scoreModel;

    public float Speed => _speed;

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

    // Update is called once per frame
    private void Update()
    {
        transform.position += new Vector3(0, 0, _speed * Time.deltaTime);


        if (InputManager.Instance.IsMovingLeft)
        {
            if (transform.position.x >= -_roadTransform.localScale.x / 2)
            {
                SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, _zShipMovementAngle));
                SpaceshipMoving(-_xSpeed);
            }
        }

        if (InputManager.Instance.IsMovingRight)
        {
            if (transform.position.x <= _roadTransform.localScale.x / 2)
            {
                SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, -_zShipMovementAngle));
                SpaceshipMoving(_xSpeed);
            }
        }

        if (!InputManager.Instance.IsMovingLeft && !InputManager.Instance.IsMovingRight)
        {
            SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, 0));
        }

        bool isBoosted = InputManager.Instance.IsShipBoosted;
        
        if (isBoosted != _speedBoosted)
        {
            _speedBoosted = isBoosted;
            _speed = _speedBoosted ? _speed * _speedMultiplier : _speed / _speedMultiplier;
            _scoreModel.ChangeScoreCalculation(_speedBoosted);
        }
        
        float distance = _speedBoosted ? _mainCameraFollow.BoostedDistance : _mainCameraFollow.DefaultDistance;
        _mainCameraFollow.DistanceChangeByShipBoost(distance);
    }

    private void SpaceshipTilt(Quaternion start, Quaternion end)
    {
        transform.rotation = Quaternion.Slerp(start, end, _spaceshipTiltTime);
    }

    private void SpaceshipMoving(float speed)
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}