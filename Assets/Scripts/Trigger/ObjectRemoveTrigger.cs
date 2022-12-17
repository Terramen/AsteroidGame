using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemoveTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _roadLayer;
    [SerializeField] private LayerMask _asteroidLayer;
    [SerializeField] private LevelGenerator _levelGenerator;
    
    // If trigger behind camera collide with asteroid or road - disable them
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_roadLayer, other.gameObject.layer))
        {
            _levelGenerator.DisableRoadByTrigger(other.gameObject);
        }
        if (ExistLayerByLayerMask(_asteroidLayer, other.gameObject.layer))
        {
            _levelGenerator.DisableAsteroidByTrigger(other.gameObject);
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
