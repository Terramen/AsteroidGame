using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidView : MonoBehaviour
{
    public void RotateObject(float rotationSpeed)
    {
        StartCoroutine(StartRotate(rotationSpeed));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator StartRotate(float rotationSpeed)
    {
        while (true)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
