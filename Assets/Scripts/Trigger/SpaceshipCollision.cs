using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipCollision : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _laserGunMask;
    
    private LevelModel _levelModel;
    private LaserGunModel _laserGunModel;
    
    public void Init(LevelModel levelModel, LaserGunModel laserGunModel)
    {
        _levelModel = levelModel;
        _laserGunModel = laserGunModel;
    }
    // if spaceship collide with asteroid, it will call a spaceship crush function
    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_layerMask, other.gameObject.layer))
        {
            _levelModel.SpaceshipCrush();
            gameObject.SetActive(false);
        }
        
        // TODO Remove later
        if (ExistLayerByLayerMask(_laserGunMask, other.gameObject.layer))
        {
            _laserGunModel.AddLaserCharge();
            other.gameObject.SetActive(false);
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
