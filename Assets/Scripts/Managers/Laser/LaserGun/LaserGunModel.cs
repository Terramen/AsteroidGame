using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunModel
{
    private float _projectileSpeed;
    private float _projectileLifeTime;
    private int _shotsLeft;

    public LaserGunModel(float projectileSpeed, float projectileLifeTime, int shotsLeft = default)
    {
        _projectileSpeed = projectileSpeed;
        _projectileLifeTime = projectileLifeTime;
        _shotsLeft = shotsLeft;
    }

    public float ProjectileSpeed => _projectileSpeed;

    public float ProjectileLifeTime => _projectileLifeTime;

    public int ShotsLeft => _shotsLeft;

    public void AddLaserCharge()
    {
        _shotsLeft++;
    }

    public void RemoveLaserCharge()
    {
        _shotsLeft--;
    }
}