using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private bool _isActive;
    private bool _isGameOver;

    public bool IsGameOver
    {
        set => _isGameOver = value;
    }
    private void Update()
    {
        // Block input after start key pressed
        if (!_isActive)
        {
            if (Input.anyKeyDown)
            {
                _isActive = true;
                ChangeState();
            }
        }
        // Game going pause after game is over
        if (_isGameOver)
        {
            _isGameOver = false;
            ChangeState();
        }
    }

    private void ChangeState()
    {
        GameState currentGameState = GameStateManager.Instance.CurrentGameState;
        GameState newGameState = currentGameState == GameState.Gameplay
            ? GameState.Paused
            : GameState.Gameplay;
        GameStateManager.Instance.SetState(newGameState);
    }
}
