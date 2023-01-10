using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnvironmentController : MonoBehaviour
{
    private Queue<EnvironmentModel> _models;

    private Dictionary<EnvironmentType, Queue<EnvironmentView>> _views;

    private EnvironmentModel _currentModel;
    private EnvironmentView _currentView;

    private LevelModel _levelModel;

    public virtual void Init(EnvironmentModel model, EnvironmentView view, LevelModel levelModel,
        SmoothFollow smoothFollow)
    {
        _models = new Queue<EnvironmentModel>();
        _views = new Dictionary<EnvironmentType, Queue<EnvironmentView>>();
        _currentModel = model;
        _currentView = view;
        _levelModel = levelModel;
        _levelModel.OnEnvironmentRemove += DisableEnvironmentByTrigger;
    }

    private void OnDestroy()
    {
        _levelModel.OnEnvironmentRemove -= DisableEnvironmentByTrigger;
    }

    protected float AddEnvironment(EnvironmentType environmentType, float positionZ, float positionX = default)
    {
        // Check if Queue is not empty
        if (_views.TryGetValue(environmentType, out Queue<EnvironmentView> queue) && queue.Count != 0)
        {
            var viewItem = queue.Dequeue();
            var envTransform = viewItem.transform;
            envTransform.position = new Vector3(positionX, envTransform.position.y,
                positionZ);
            viewItem.gameObject.SetActive(true);
            viewItem.ActivateView(_currentModel);
            return envTransform.localScale.z;
        }
        // If Queue is empty, instantiate a new prebab copy
        else
        {
            var envTransform = _currentView.transform;
            var viewPosition = new Vector3(positionX, envTransform.position.y,
                positionZ);
            var view = Instantiate(_currentView, viewPosition, envTransform.rotation);
            view.ActivateView(_currentModel);
            return envTransform.localScale.z;
        }
    }

    private void DisableEnvironmentByTrigger(EnvironmentType type, EnvironmentView view)
    {
        view.gameObject.SetActive(false);
        if (_views.TryGetValue(type, out Queue<EnvironmentView> queue))
        {
            queue.Enqueue(view);
        }
        else
        {
            var envViewQueue = new Queue<EnvironmentView>();
            envViewQueue.Enqueue(view);
            _views.Add(type, envViewQueue);
        }
    }
}