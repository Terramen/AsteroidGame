using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : EnvironmentController
{
    private SmoothFollow _smoothFollow;

    private LevelModel _levelModel;

    public override void Init(EnvironmentModel model, EnvironmentView view, LevelModel levelModel,
        SmoothFollow smoothFollow)
    {
        base.Init(model, view, levelModel, smoothFollow);
        _smoothFollow = smoothFollow;
        _levelModel = levelModel;
    }

    private void Update()
    {
        var currentPosition = _smoothFollow.gameObject.transform.position.z;
        if (_levelModel.RoadPositionZ < _levelModel.PlayerVisibilityRadius + currentPosition)
        {
            var shift = AddEnvironment(EnvironmentType.Road, _levelModel.RoadPositionZ);
            _levelModel.ChangeBaseRoadPosition(shift);
        }
    }
}