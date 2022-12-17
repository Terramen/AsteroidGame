using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPooling : MonoBehaviour
{
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
    }
    // Add a choosed object to a disabledQueue
    public void AddObjectToPool(GameObject prefab, Queue<GameObject> disabledPrefabQueue,
        float positionX, float positionZ)
    {
        // Check if Queue is not empty
        if (disabledPrefabQueue.Count != 0)
        {
            GameObject item = disabledPrefabQueue.Dequeue();
            item.transform.position = new Vector3(positionX, item.transform.position.y,
                positionZ);
            item.SetActive(true);
        }
        // If Queue is empty, instantiate a new prebab copy
        else
        {
            Instantiate(prefab, new Vector3(positionX, prefab.transform.position.y, positionZ),
                prefab.transform.rotation);
        }
    }

    // Disable choosed Gameobject
    public void DisableObject(GameObject item, Queue<GameObject> disabledPrefabQueue)
    {
        item.SetActive(false);
        disabledPrefabQueue.Enqueue(item);
    }
    
    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }
}
