using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private LevelView _levelView;
    private LevelModel _levelModel;
    private PrefabPooling _prefabPooling;
    private SmoothFollow _smoothFollow;

    // TODO Remove later
    private PauseController _pauseController;

    public void Init(LevelModel levelModel, LevelView levelView, PauseController pauseController)
    {
        _pauseController = pauseController;
        _levelModel = levelModel;
        _levelView = levelView;
        _levelView.InitView(levelModel);
        _levelModel.OnGameOver += PlayAgain;
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        _levelModel.OnGameOver -= PlayAgain;
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
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