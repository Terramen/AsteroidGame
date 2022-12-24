using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidModel
{
    private float _rotateSpeed;

    public float RotateSpeed => _rotateSpeed;

    public AsteroidModel(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }
}
