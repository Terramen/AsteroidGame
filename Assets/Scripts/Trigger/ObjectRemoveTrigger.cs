using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRemoveTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask _roadLayer;
    [SerializeField] private LayerMask _asteroidLayer;

    private LevelModel _levelModel;

    public void Init(LevelModel levelModel)
    {
        _levelModel = levelModel;
    }
    // If trigger behind camera collide with asteroid or road - disable them
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_roadLayer, other.gameObject.layer))
        {
            other.gameObject.TryGetComponent(out RoadView roadView);
            _levelModel.RemoveRoadsFromPool(roadView);
        }
        if (ExistLayerByLayerMask(_asteroidLayer, other.gameObject.layer))
        {
            other.gameObject.TryGetComponent(out AsteroidView asteroidView);
            _levelModel.RemoveAsteroidsFromPool(asteroidView);
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
