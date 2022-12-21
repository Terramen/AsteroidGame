using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _scoreControllerPrefab;
    [SerializeField] private GameObject _scoreViewPrefab;

    [SerializeField] private GameObject _spaceshipControllerPrefab;
    [SerializeField] private GameObject _spaceshipPrefab;
    
    [SerializeField] private GameObject _asteroidMissCollisionPrefab;
    [SerializeField] private GameObject _levelGeneratorPrefab;
    [SerializeField] private GameObject _roadPrefab;
    [SerializeField] private GameObject _cameraPrefab;

    private ScoreController _scoreController;
    private SpaceshipController _spaceshipController;
    private AsteroidMissCollision _asteroidMissCollision;
    private SpaceshipCollision _spaceshipCollision;
    private LevelGenerator _levelGenerator;
    private UIManager _uiManager;

    private void Awake()
    {
        ScoreModel scoreModel = new ScoreModel(30, 5, 20, 1, 2);
        var scoreViewPrefab = Instantiate(_scoreViewPrefab, _canvas.transform, true);
        scoreViewPrefab.transform.localPosition = Vector3.zero;
        ScoreView scoreView = scoreViewPrefab.GetComponent<ScoreView>();
        var scoreControllerPrefab = Instantiate(_scoreControllerPrefab);
        _scoreController = scoreControllerPrefab.GetComponent<ScoreController>();
        _scoreController.Init(scoreModel, scoreView);
        

        SmoothFollow smoothFollow = _cameraPrefab.GetComponent<SmoothFollow>();
        SpaceshipModel spaceshipModel = new SpaceshipModel(7, 2, 30, 0.2f, 3, _roadPrefab.transform.localScale.x / 2);
        var spaceshipViewPrefab = Instantiate(_spaceshipPrefab);
        SpaceshipView spaceshipView = spaceshipViewPrefab.GetComponent<SpaceshipView>();
        smoothFollow.SetTarget(spaceshipView.transform);
        
        var spaceshipController = Instantiate(_spaceshipControllerPrefab);
        _spaceshipController = spaceshipController.GetComponent<SpaceshipController>();
        _spaceshipController.Init(spaceshipModel, spaceshipView, scoreModel, smoothFollow);
        
        var asteroidMissCollisionPrefab = Instantiate(_asteroidMissCollisionPrefab, spaceshipViewPrefab.transform, true);
        asteroidMissCollisionPrefab.transform.localPosition = Vector3.zero;
        _asteroidMissCollision = asteroidMissCollisionPrefab.GetComponent<AsteroidMissCollision>();
        _asteroidMissCollision.Init(scoreModel);
        
        _uiManager = _canvas.GetComponent<UIManager>();
        _uiManager.Init(scoreModel);
        
        _spaceshipCollision = spaceshipViewPrefab.GetComponent<SpaceshipCollision>();
        _spaceshipCollision.Init(scoreModel, _uiManager);

        _levelGenerator = _levelGeneratorPrefab.GetComponent<LevelGenerator>();
        _levelGenerator.Init(scoreModel, spaceshipModel);


    }
}