using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private Queue<RoadModel> _roadModels;
    private Queue<RoadView> _roadViews;

    private RoadModel _currentRoadModel;
    private RoadView _currentRoadView;
    private SmoothFollow _smoothFollow;

    private LevelModel _levelModel;

    public void Init(RoadModel roadModel, RoadView roadView, LevelModel levelModel, SmoothFollow smoothFollow)
    {
        _roadModels = new Queue<RoadModel>();
        _roadViews = new Queue<RoadView>();
        _currentRoadModel = roadModel;
        _currentRoadView = roadView;
        _levelModel = levelModel;
        _smoothFollow = smoothFollow;
        _levelModel.OnRoadRemove += DisableRoadByTrigger;
    }

    private void OnDestroy()
    {
        _levelModel.OnRoadRemove -= DisableRoadByTrigger;
    }

    private void Update()
    {
        var currentPosition = _smoothFollow.gameObject.transform.position.z;
        if (_levelModel.RoadPositionZ < _levelModel.PlayerVisibilityRadius + currentPosition)
        {
            AddRoad();
        }
    }

    private void AddRoad()
    {
        // Check if Queue is not empty
        if (_roadViews.Count != 0)
        {
            RoadView viewItem = _roadViews.Dequeue();
            var roadTransform = viewItem.transform;
            roadTransform.position = new Vector3(roadTransform.position.x, roadTransform.position.y,
                _levelModel.RoadPositionZ);
            _levelModel.ChangeBaseRoadPosition(roadTransform.localScale.z);
            viewItem.gameObject.SetActive(true);
        }
        // If Queue is empty, instantiate a new prebab copy
        else
        {
            var roadTransform = _currentRoadView.transform;
            var viewPosition = new Vector3(roadTransform.position.x, roadTransform.position.y,
                _levelModel.RoadPositionZ);
            _levelModel.ChangeBaseRoadPosition(roadTransform.localScale.z);
            Instantiate(_currentRoadView, viewPosition, roadTransform.rotation);
        }
    }

    private void DisableRoadByTrigger(RoadView asteroid)
    {
        asteroid.gameObject.SetActive(false);
        _roadViews.Enqueue(asteroid);
    }
}