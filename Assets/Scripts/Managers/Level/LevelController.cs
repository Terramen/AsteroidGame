using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private LevelView _levelView;
    private LevelModel _levelModel;
    private PrefabPooling _prefabPooling;
    private SpaceshipModel _spaceshipModel;

    // TODO Remove later
    private PauseController _pauseController;

    public void Init(LevelModel levelModel, LevelView levelView, SpaceshipModel spaceshipModel,
        PauseController pauseController)
    {
        _pauseController = pauseController;
        _levelModel = levelModel;
        _levelView = levelView;
        _levelView.InitView(levelModel);
        _spaceshipModel = spaceshipModel;
        _levelModel.OnGameOver += PlayAgain;
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
        _levelModel.OnGameOver -= PlayAgain;
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

    private void PlayAgain()
    {
        _pauseController.IsGameOver = true;
        _levelView.SpaceshipCrush();
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        _levelView.RestartGame(newGameState);
        enabled = newGameState == GameState.Gameplay;
    }
}