using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectileView : MonoBehaviour
{
    [SerializeField] private LayerMask _asteroidLayerMask;
    public void Launch(float time, float speed)
    {
        StartCoroutine(ProjectileMovement(time, speed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ProjectileMovement(float time, float speed)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            transform.position += new Vector3(0, 0, speed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (ExistLayerByLayerMask(_asteroidLayerMask, other.gameObject.layer))
        {
            other.gameObject.SetActive(false);
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
