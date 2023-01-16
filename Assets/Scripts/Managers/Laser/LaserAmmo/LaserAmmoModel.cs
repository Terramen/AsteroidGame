using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmoModel : EnvironmentModel
{
    private float _rotateSpeed;

    public float RotateSpeed => _rotateSpeed;

    public LaserAmmoModel(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }
}
