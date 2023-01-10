using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : EnvironmentController
{
    private LevelModel _levelModel;
    private SmoothFollow _smoothFollow;

    public override void Init(EnvironmentModel model, EnvironmentView view, LevelModel levelModel,
        SmoothFollow smoothFollow)
    {
        base.Init(model, view, levelModel, smoothFollow);
        _levelModel = levelModel;
        _smoothFollow = smoothFollow;
    }

    private void Update()
    {
        var currentPosition = _smoothFollow.gameObject.transform.position.z;
        if (_levelModel.AsteroidPositionZ < _levelModel.PlayerVisibilityRadius + currentPosition)
        {
            _levelModel.ChangeBaseAsteroidPosition();
            AddEnvironment(EnvironmentType.Asteroid, _levelModel.AsteroidPositionZ,
                Random.Range(-_levelModel.RoadBorder, _levelModel.RoadBorder));
            
        }
    }
}