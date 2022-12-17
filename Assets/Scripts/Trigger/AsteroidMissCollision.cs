using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMissCollision : MonoBehaviour
{
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private LayerMask _layerMask;

    // If trigger collide with asteroid it will add 5 points
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_layerMask, other.gameObject.layer))
        {
            _scoreManager.AddScore(_scoreManager.AsteroidPassScore);
            _scoreManager.AddAsteroid();
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
