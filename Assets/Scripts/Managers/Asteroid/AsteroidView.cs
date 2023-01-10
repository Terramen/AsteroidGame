using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidView : EnvironmentView
{
    public override void ActivateView(EnvironmentModel model)
    {
        base.ActivateView(model);
        var asteroidModel = model as AsteroidModel;
        var speed = asteroidModel?.RotateSpeed ?? default;
        StartCoroutine(StartRotate(speed));
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
