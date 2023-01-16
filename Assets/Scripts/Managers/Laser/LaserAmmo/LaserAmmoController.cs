using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmoController : EnvironmentController
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
        if (_levelModel.LaserAmmoPositionZ < _levelModel.PlayerVisibilityRadius + currentPosition)
        {
            AddEnvironment(EnvironmentType.LaserAmmo, _levelModel.LaserAmmoPositionZ,
                Random.Range(-_levelModel.RoadBorder, _levelModel.RoadBorder));
            _levelModel.ChangeBaseLaserAmmoPosition();
            _levelModel.LaserAmmoRespawnChange();
        }
    }
}