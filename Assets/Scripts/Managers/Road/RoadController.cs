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

    private LevelModel _levelModel;

    public void Init(RoadModel roadModel, RoadView roadView, LevelModel levelModel)
    {
        _roadModels = new Queue<RoadModel>();
        _roadViews = new Queue<RoadView>();
        _currentRoadModel = roadModel;
        _currentRoadView = roadView;
        _levelModel = levelModel;
        _levelModel.OnRoadAdd += AddMultipleRoads;
        _levelModel.OnRoadRemove += DisableRoadByTrigger;
    }

    private void OnDestroy()
    {
        _levelModel.OnRoadAdd -= AddMultipleRoads;
        _levelModel.OnRoadRemove -= DisableRoadByTrigger;
    }

    private void Update()
    {
        if (_roadViews.Count != 0)
        {
            AddRoad(_levelModel.RoadCount);
            _levelModel.IncreaseRoadCount();
        }
    }

    private void AddMultipleRoads(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddRoad(i);
        }
    }

    private void AddRoad(int iterator)
    {
        // Check if Queue is not empty
        if (_roadViews.Count != 0)
        {
            RoadView viewItem = _roadViews.Dequeue();
            var roadTransform = viewItem.transform;
            roadTransform.position = new Vector3(roadTransform.position.x, roadTransform.position.y,
                roadTransform.localScale.z * iterator);
            viewItem.gameObject.SetActive(true);
        }
        // If Queue is empty, instantiate a new prebab copy
        else
        {
            var roadTransform = _currentRoadView.transform;
            var viewPosition = new Vector3(roadTransform.position.x, roadTransform.position.y,
                roadTransform.localScale.z * iterator);
            Instantiate(_currentRoadView, viewPosition, roadTransform.rotation);
        }
    }

    private void DisableRoadByTrigger(RoadView asteroid)
    {
        asteroid.gameObject.SetActive(false);
        _roadViews.Enqueue(asteroid);
    }
}