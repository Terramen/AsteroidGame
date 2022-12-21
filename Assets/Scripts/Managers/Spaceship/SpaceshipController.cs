using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private SpaceshipView _spaceshipView;
    private SpaceshipModel _spaceshipModel;
    private ScoreModel _scoreModel;
    private SmoothFollow _mainCameraFollow;

    public void Init(SpaceshipModel spaceshipModel, SpaceshipView spaceshipView, ScoreModel scoreModel, SmoothFollow smoothFollow)
    {
        _spaceshipModel = spaceshipModel;
        _spaceshipView = spaceshipView;
        _scoreModel = scoreModel;
        _mainCameraFollow = smoothFollow;
    }

    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Update()
    {
        _spaceshipView.SpaceshipForwardMoving(_spaceshipModel.Speed);
        
        if (InputManager.Instance.IsMovingLeft)
        {
            _spaceshipView.SpaceshipLeftSideMoving(_spaceshipModel.RoadLengthX, _spaceshipModel.ZShipMovementAngle,
                _spaceshipModel.SideShiftSpeed, _spaceshipModel.SpaceshipTiltTime);
        }

        if (InputManager.Instance.IsMovingRight)
        {
            _spaceshipView.SpaceshipRightSideMoving(_spaceshipModel.RoadLengthX, _spaceshipModel.ZShipMovementAngle,
                _spaceshipModel.SideShiftSpeed, _spaceshipModel.SpaceshipTiltTime);
        }

        if (!InputManager.Instance.IsMovingLeft && !InputManager.Instance.IsMovingRight)
        {
            _spaceshipView.SpaceshipRotateTiltBack(_spaceshipModel.SpaceshipTiltTime);
        }

        var isBoosted = InputManager.Instance.IsShipBoosted;

        if (isBoosted != _spaceshipModel.SpeedBoosted)
        {
            _spaceshipModel.SpeedBoosted = isBoosted;
            _spaceshipModel.Speed = _spaceshipModel.SpeedBoosted
                ? _spaceshipModel.Speed * _spaceshipModel.SpeedMultiplier
                : _spaceshipModel.Speed / _spaceshipModel.SpeedMultiplier;
            _scoreModel.ChangeScoreCalculation(_spaceshipModel.SpeedBoosted);
        }

        var distance = _spaceshipModel.SpeedBoosted
            ? _mainCameraFollow.BoostedDistance
            : _mainCameraFollow.DefaultDistance;
        _mainCameraFollow.DistanceChangeByShipBoost(distance);
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
        _spaceshipView.enabled = newGameState == GameState.Gameplay;
    }
}