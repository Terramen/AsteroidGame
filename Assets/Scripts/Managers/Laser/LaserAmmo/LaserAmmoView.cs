using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAmmoView : EnvironmentView
{
    public override void ActivateView(EnvironmentModel model)
    {
        base.ActivateView(model);
        var laserAmmoModel = model as LaserAmmoModel;
        var speed = laserAmmoModel?.RotateSpeed ?? default;
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
