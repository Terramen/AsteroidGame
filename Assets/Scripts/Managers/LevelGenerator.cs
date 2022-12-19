using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    
    [Header("Road")]
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private int _roadCount;
    [SerializeField] private int _zDeadZone;
    [SerializeField] private float _roadBorder;
    private int _roadRemovePointZ;
    private Queue<GameObject> _disabledRoads;

    [Header("Asteroid")]
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private int _asteroidCount;
    private Queue<GameObject> _disabledAsteroids;
    private int _asteroidRemovePointZ;
    private float _asteroidPositionZ;
    
    [Header("Utility")]
    [SerializeField] private PrefabPooling _prefabPooling;
    [SerializeField] private PlayerMovement _playerMovement;

    // TODO Replace with another model because asteroidFrequency not related to score
    private ScoreModel _scoreModel;

    public void Init(ScoreModel scoreModel)
    {
        _scoreModel = scoreModel;
    }

    // Queue initialization and adding some starting objects
    private void Awake()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        _disabledRoads = new Queue<GameObject>();
        _disabledAsteroids = new Queue<GameObject>();
        _asteroidRemovePointZ = 1;

        for (int i = 0; i < _roadCount; i++)
        {
            _prefabPooling.AddObjectToPool(_roadPrefab, _disabledRoads,
                _roadPrefab.transform.position.x, i * _roadPrefab.transform.localScale.z);
        }

        for (int i = 0; i < _asteroidCount; i++)
        {
            _asteroidPositionZ += _scoreModel.AsteroidCounter.AsteroidFrequency;
            _prefabPooling.AddObjectToPool(_asteroidPrefab, _disabledAsteroids,
                Random.Range(-_roadBorder,_roadBorder), _asteroidPositionZ);
        }
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
    
    private void Update()
    {
        // Respawn a deactiaved road
        if (_disabledRoads.Count != 0)
        {
            _prefabPooling.AddObjectToPool(_roadPrefab, _disabledRoads, _roadPrefab.transform.position.x,  _roadCount * _roadPrefab.transform.localScale.z);
            _roadCount++;
        }
    }

    // Coroutine to generate asteroid with time
    private IEnumerator AsteroidRespawn()
    {
        while (true)
        {
            _asteroidPositionZ += _scoreModel.AsteroidCounter.AsteroidFrequency;
            _prefabPooling.AddObjectToPool(_asteroidPrefab, _disabledAsteroids,
                Random.Range(-_roadBorder,_roadBorder), _asteroidPositionZ);
            yield return new WaitForSeconds(_scoreModel.AsteroidCounter.AsteroidFrequency / _playerMovement.Speed);
        }
    }

    private void OnGameStateChanged(GameState newGameState)
    {
        enabled = newGameState == GameState.Gameplay;
    }

    public void DisableAsteroidByTrigger(GameObject asteroid)
    {
        _prefabPooling.DisableObject(asteroid, _disabledAsteroids);
    }
    
    public void DisableRoadByTrigger(GameObject asteroid)
    {
        _prefabPooling.DisableObject(asteroid, _disabledRoads);
    }
}
