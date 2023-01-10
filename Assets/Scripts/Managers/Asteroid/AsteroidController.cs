using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    private Queue<AsteroidModel> _asteroidModels;

    private Queue<AsteroidView> _asteroidViews;

    // TODO can be dynamic?
    private AsteroidView _currentAsteroidView;
    private AsteroidModel _currentAsteroidModel;

    private LevelModel _levelModel;
    private SmoothFollow _smoothFollow;

    private PrefabPooling _prefabPooling;

    public void Init(LevelModel levelModel, AsteroidView asteroidView, AsteroidModel asteroidModel,
        SmoothFollow smoothFollow)
    {
        _currentAsteroidView = asteroidView;
        _currentAsteroidModel = asteroidModel;
        _asteroidModels = new Queue<AsteroidModel>();
        _asteroidViews = new Queue<AsteroidView>();
        _levelModel = levelModel;
        _smoothFollow = smoothFollow;
        _levelModel.OnAsteroidRemove += DisableAsteroidByTrigger;
    }

    private void OnDestroy()
    {
        _levelModel.OnAsteroidRemove -= DisableAsteroidByTrigger;
    }

    private void Update()
    {
        var currentPosition = _smoothFollow.gameObject.transform.position.z;
        if (_levelModel.AsteroidPositionZ < _levelModel.PlayerVisibilityRadius + currentPosition)
        {
            AddAsteroid();
        }
    }

    private void AddAsteroid()
    {
        _levelModel.ChangeBaseAsteroidPosition();

        // Check if Queue is not empty
        if (_asteroidViews.Count != 0)
        {
            AsteroidModel modelItem = _asteroidModels.Dequeue();
            AsteroidView viewItem = _asteroidViews.Dequeue();
            viewItem.transform.position = new Vector3(Random.Range(-_levelModel.RoadBorder, _levelModel.RoadBorder),
                viewItem.transform.position.y, _levelModel.AsteroidPositionZ);
            viewItem.gameObject.SetActive(true);
            viewItem.RotateObject(modelItem.RotateSpeed);
            viewItem.RotateObject(60);
        }
        // If Queue is empty, instantiate a new prebab copy
        else
        {
            var model = _currentAsteroidModel;
            var viewPosition = new Vector3(Random.Range(-_levelModel.RoadBorder, _levelModel.RoadBorder),
                _currentAsteroidView.transform.position.y, _levelModel.AsteroidPositionZ);
            var view = Instantiate(_currentAsteroidView, viewPosition, _currentAsteroidView.transform.rotation);
            view.RotateObject(model.RotateSpeed);
        }
    }

    private void DisableAsteroidByTrigger(AsteroidView asteroid)
    {
        asteroid.gameObject.SetActive(false);
        _asteroidViews.Enqueue(asteroid);
        _asteroidModels.Enqueue(new AsteroidModel(60));
    }
}