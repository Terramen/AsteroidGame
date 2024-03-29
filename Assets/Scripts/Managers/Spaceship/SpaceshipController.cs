using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipController : MonoBehaviour
{
    private SpaceshipView _spaceshipView;
    private SpaceshipModel _spaceshipModel;
    private SmoothFollow _mainCameraFollow;
    private RoadModel _roadModel;

    public void Init(SpaceshipModel spaceshipModel, SpaceshipView spaceshipView, SmoothFollow smoothFollow,
        RoadModel roadModel)
    {
        _spaceshipModel = spaceshipModel;
        _spaceshipView = spaceshipView;
        _mainCameraFollow = smoothFollow;
        _roadModel = roadModel;
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
            _spaceshipView.SpaceshipLeftSideMoving(_roadModel.RoadWidth, _spaceshipModel.ZShipMovementAngle,
                _spaceshipModel.SideShiftSpeed, _spaceshipModel.SpaceshipTiltTime);
        }

        if (InputManager.Instance.IsMovingRight)
        {
            _spaceshipView.SpaceshipRightSideMoving(_roadModel.RoadWidth, _spaceshipModel.ZShipMovementAngle,
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
            _spaceshipModel.CheckSpeedBoostState(_spaceshipModel.SpeedBoosted);
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