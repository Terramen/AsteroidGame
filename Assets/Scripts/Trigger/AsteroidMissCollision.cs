﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AsteroidMissCollision : MonoBehaviour
{
    private ScoreModel _scoreModel;

    //[SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private LayerMask _layerMask;

    public void Init(ScoreModel scoreModel)
    {
        _scoreModel = scoreModel;
    }

    // If trigger collide with asteroid it will add 5 points
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_layerMask, other.gameObject.layer))
        {
            _scoreModel.AddScore(_scoreModel.AsteroidCounter.AsteroidPassScore);
            _scoreModel.AsteroidCounter.AddAsteroid();
        }
    }

    private bool ExistLayerByLayerMask(LayerMask layerMask, int layer)
    {
        if ((layerMask.value & 1 << layer) > 0)
        {
            return true;
        }

        return false;
    }
}