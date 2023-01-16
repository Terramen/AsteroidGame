using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunController : MonoBehaviour
{
    private EnvironmentModel _environmentModel;
    private LaserGunModel _laserGunModel;
    private LaserGunView _laserLeftGunView;
    private LaserGunView _laserRightGunView;
    private LaserProjectileView _laserProjectileView;

    public void Init(LaserGunModel laserGunModel, LaserGunView laserLeftGunView, LaserGunView laserRightGunView,
        LaserProjectileView laserProjectileView, EnvironmentModel environmentModel)
    {
        _environmentModel = environmentModel;
        _laserGunModel = laserGunModel;
        _laserLeftGunView = laserLeftGunView;
        _laserRightGunView = laserRightGunView;
        _laserProjectileView = laserProjectileView;
    }

    private void Shoot()
    {
        //_environmentModel.OnShoot.Invoke();
        var leftGun = Instantiate(_laserProjectileView, _laserLeftGunView.transform.position, Quaternion.identity);
        leftGun.Launch(_laserGunModel.ProjectileLifeTime, _laserGunModel.ProjectileSpeed);
        var rightGun = Instantiate(_laserProjectileView, _laserRightGunView.transform.position, Quaternion.identity);
        rightGun.Launch(_laserGunModel.ProjectileLifeTime, _laserGunModel.ProjectileSpeed);
    }

    private void Update()
    {
        if (InputManager.Instance.IsShooting && _laserGunModel.ShotsLeft > 0)
        {
            Shoot();
            _laserGunModel.RemoveLaserCharge();
        }
    }
}