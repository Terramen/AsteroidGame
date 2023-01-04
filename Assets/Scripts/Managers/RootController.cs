using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _scoreControllerPrefab;
    [SerializeField] private GameObject _scoreViewPrefab;

    [SerializeField] private GameObject _spaceshipControllerPrefab;
    [SerializeField] private GameObject _spaceshipPrefab;
    
    [SerializeField] private GameObject _asteroidControllerPrefab;
    [SerializeField] private GameObject _asteroidMissCollisionPrefab;
    [SerializeField] private GameObject _levelControllerPrefab;
    [SerializeField] private GameObject _cameraPrefab;

    [SerializeField] private GameObject _roadControllerPrefab;

    //[SerializeField] private LevelView _levelViewPrefab;
    [SerializeField] private AsteroidView _asteroidViewPrefab;
    [SerializeField] private RoadView _roadViewPrefab;
    [SerializeField] private LevelView _levelViewPrefab;

    [SerializeField] private ObjectRemoveTrigger _objectRemoveTrigger;

    private ScoreController _scoreController;
    private SpaceshipController _spaceshipController;
    private AsteroidController _asteroidController;
    private RoadController _roadController;
    private LevelController _levelController;
    private AsteroidMissCollision _asteroidMissCollision;
    private SpaceshipCollision _spaceshipCollision;

    private void Awake()
    {
        PauseController pauseController = GetComponent<PauseController>();
        
        SmoothFollow smoothFollow = _cameraPrefab.GetComponent<SmoothFollow>();
        SpaceshipModel spaceshipModel = new SpaceshipModel(7, 2, 30, 0.2f, 3);
        var spaceshipViewPrefab = Instantiate(_spaceshipPrefab);
        SpaceshipView spaceshipView = spaceshipViewPrefab.GetComponent<SpaceshipView>();
        smoothFollow.SetTarget(spaceshipView.transform);
        
        LevelModel levelModel = new LevelModel(7, 20, 16, 2);
        LevelView levelView = Instantiate(_levelViewPrefab, _canvas.transform, false);
        var levelControllerPrefab = Instantiate(_levelControllerPrefab);
        _levelController = levelControllerPrefab.GetComponent<LevelController>();
        _levelController.Init(levelModel, levelView, spaceshipModel, pauseController);

        AsteroidModel asteroidModel = new AsteroidModel(60);
        AsteroidView asteroidView = _asteroidViewPrefab;
        _asteroidController = _asteroidControllerPrefab.GetComponent<AsteroidController>();
        _asteroidController.Init(levelModel, asteroidView, asteroidModel);
        
        RoadModel roadModel = new RoadModel(_roadViewPrefab.transform.localScale.x / 2);
        RoadView roadView = _roadViewPrefab;
        var roadControllerPrefab = Instantiate(_roadControllerPrefab);
        _roadController = roadControllerPrefab.GetComponent<RoadController>();
        _roadController.Init(roadModel, roadView, levelModel);
         
        // TODO Possible to replace?
        _levelController.InitEnvironment();

        _objectRemoveTrigger.Init(levelModel);
        
        ScoreModel scoreModel = new ScoreModel(30, 5, 1, 2);
        var scoreViewPrefab = Instantiate(_scoreViewPrefab, _canvas.transform, false);
        ScoreView scoreView = scoreViewPrefab.GetComponent<ScoreView>();
        var scoreControllerPrefab = Instantiate(_scoreControllerPrefab);
        _scoreController = scoreControllerPrefab.GetComponent<ScoreController>();
        _scoreController.Init(scoreModel, scoreView, spaceshipModel, levelModel);

        var spaceshipController = Instantiate(_spaceshipControllerPrefab);
        _spaceshipController = spaceshipController.GetComponent<SpaceshipController>();
        _spaceshipController.Init(spaceshipModel, spaceshipView, smoothFollow, roadModel);
        
        var asteroidMissCollisionPrefab = Instantiate(_asteroidMissCollisionPrefab, spaceshipViewPrefab.transform, true);
        asteroidMissCollisionPrefab.transform.localPosition = Vector3.zero;
        _asteroidMissCollision = asteroidMissCollisionPrefab.GetComponent<AsteroidMissCollision>();
        _asteroidMissCollision.Init(scoreModel);

        _spaceshipCollision = spaceshipViewPrefab.GetComponent<SpaceshipCollision>();
        _spaceshipCollision.Init(levelModel);
    }
}