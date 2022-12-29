using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [Header("StartGamePanel")]
    [SerializeField] private GameObject _startGamePanel;
    
    [Header("GameOverPanel")]
    [SerializeField] private GameObject _gameOverGamePanel;
    
    [SerializeField] private Button _playAgainButton;
    
    // TODO Possible remove :(
    private LevelModel _levelModel;

    public void InitView(LevelModel levelModel)
    {
        _levelModel = levelModel;
    }

    private void Awake()
    {
        _playAgainButton.onClick.AddListener(() => PlayAgain(_levelModel));
    }

    private void OnDestroy()
    {
        _playAgainButton.onClick.RemoveListener(() => PlayAgain(_levelModel));
    }
    
    public void SpaceshipCrush()
    {
        _gameOverGamePanel.SetActive(true);
    }

    public void RestartGame(GameState newGameState)
    {
        _startGamePanel.SetActive(newGameState == GameState.Paused);
    }

    private void PlayAgain(LevelModel levelModel)
    {
        levelModel.PlayAgain();
    }
}
