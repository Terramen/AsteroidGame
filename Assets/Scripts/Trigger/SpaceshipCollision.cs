﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollision : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private LayerMask _layerMask;
    // if spaceship collide with asteroid, it will call a spaceship crush function
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_layerMask, other.gameObject.layer))
        {
            _uiManager.SpaceshipCrush();
            gameObject.SetActive(false);
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
