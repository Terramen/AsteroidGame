using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private LevelView _levelView;
    private LevelModel _levelModel;
    private PrefabPooling _prefabPooling;
    private SpaceshipModel _spaceshipModel;

    public void Init(LevelModel levelModel, SpaceshipModel spaceshipModel)
    {
        _levelModel = levelModel;
        _spaceshipModel = spaceshipModel;
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    public void InitEnvironment()
    {
        _levelModel.AddAsteroidsInPool(_levelModel.AsteroidCount);
        _levelModel.AddRoadsInPool(_levelModel.RoadCount);
    }

    private void Start()
    {
        // Start the coroutine to spawn asteroids with time
        StartCoroutine(AsteroidRespawn());
    }

    private void OnDisable()
    {
        //  If behavior was disabled, coroutine will be stopped
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    
    private IEnumerator AsteroidRespawn()
    {
        while (true)
        {
            _levelModel.AddAsteroidsInPool(1);
            yield return new WaitForSeconds(_levelModel.AsteroidFrequency / _spaceshipModel.Speed);
        }
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}