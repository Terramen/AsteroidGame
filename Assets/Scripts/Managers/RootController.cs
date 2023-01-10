using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private ScoreController _scoreControllerPrefab;

    [SerializeField] private SpaceshipController _spaceshipControllerPrefab;
    [SerializeField] private SpaceshipView _spaceshipPrefab;
    
    [SerializeField] private AsteroidController _asteroidControllerPrefab;
    [SerializeField] private AsteroidMissCollision _asteroidMissCollisionPrefab;
    [SerializeField] private LevelController _levelControllerPrefab;
    [SerializeField] private GameObject _cameraPrefab;

    [SerializeField] private RoadController _roadControllerPrefab;
    
    [SerializeField] private AsteroidView _asteroidViewPrefab;
    [SerializeField] private RoadView _roadViewPrefab;
    [SerializeField] private LevelView _levelViewPrefab;
    [SerializeField] private ScoreView _scoreViewPrefab;

    [SerializeField] private ObjectRemoveTrigger _objectRemoveTrigger;

    private void Awake()
    {
        PauseController pauseController = GetComponent<PauseController>();
        
        SmoothFollow smoothFollow = _cameraPrefab.GetComponent<SmoothFollow>();

        LevelModel levelModel = new LevelModel(20, 16, 2.5f, 70);
        LevelView levelView = Instantiate(_levelViewPrefab, _canvas.transform, false);
        var levelController = Instantiate(_levelControllerPrefab);
        levelController.Init(levelModel, levelView, pauseController);

        AsteroidModel asteroidModel = new AsteroidModel(60);
        AsteroidView asteroidView = _asteroidViewPrefab;
        var asteroidController = Instantiate(_asteroidControllerPrefab);
        asteroidController.Init(asteroidModel, asteroidView, levelModel, smoothFollow);
        
        RoadModel roadModel = new RoadModel(_roadViewPrefab.transform.localScale.x / 2);
        RoadView roadView = _roadViewPrefab;
        var roadController = Instantiate(_roadControllerPrefab);
        roadController.Init(roadModel, roadView, levelModel, smoothFollow);

        _objectRemoveTrigger.Init(levelModel);
        
        SpaceshipModel spaceshipModel = new SpaceshipModel(7, 2, 30, 0.2f, 3);
        var spaceshipView = Instantiate(_spaceshipPrefab);
        var spaceshipController = Instantiate(_spaceshipControllerPrefab);
        spaceshipController.Init(spaceshipModel, spaceshipView, smoothFollow, roadModel);
        smoothFollow.SetTarget(spaceshipView.transform);
        
        ScoreModel scoreModel = new ScoreModel(30, 5, 1, 2);
        var scoreView = Instantiate(_scoreViewPrefab, _canvas.transform, false);
        var scoreController = Instantiate(_scoreControllerPrefab);
        scoreController.Init(scoreModel, scoreView, spaceshipModel, levelModel);

        var asteroidMissCollision = Instantiate(_asteroidMissCollisionPrefab, spaceshipView.transform, false);
        asteroidMissCollision.Init(scoreModel);

        var spaceshipCollision = spaceshipView.GetComponent<SpaceshipCollision>();
        spaceshipCollision.Init(levelModel);
    }
}