using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadModel : EnvironmentModel
{
    private float _roadWidth;

    public float RoadWidth => _roadWidth;

    public RoadModel(float roadWidth)
    {
        _roadWidth = roadWidth;
    }
}