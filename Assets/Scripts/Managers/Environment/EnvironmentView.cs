using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentView : MonoBehaviour
{
    public virtual void ActivateView(EnvironmentModel model)
    {
        //model.OnViewActivated?.Invoke();
    }
}
