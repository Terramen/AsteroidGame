using UnityEngine;

public class RootController : MonoBehaviour
{
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _scoreControllerPrefab;
    [SerializeField] private GameObject _scoreViewPrefab;

    [SerializeField] private GameObject _spaceshipPrefab;
    [SerializeField] private GameObject _asteroidMissCollisionPrefab;
    [SerializeField] private GameObject _levelGeneratorPrefab;
    private ScoreController _scoreController;
    private AsteroidMissCollision _asteroidMissCollision;
    private PlayerMovement _playerMovement;
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
        
        var asteroidMissCollisionPrefab = Instantiate(_asteroidMissCollisionPrefab, _spaceshipPrefab.transform, true);
        asteroidMissCollisionPrefab.transform.localPosition = Vector3.zero;
        _asteroidMissCollision = asteroidMissCollisionPrefab.GetComponent<AsteroidMissCollision>();
        _asteroidMissCollision.Init(scoreModel);

        _playerMovement = _spaceshipPrefab.GetComponent<PlayerMovement>();
        _playerMovement.Init(scoreModel);

        _spaceshipCollision = _spaceshipPrefab.GetComponent<SpaceshipCollision>();
        _spaceshipCollision.Init(scoreModel);

        _levelGenerator = _levelGeneratorPrefab.GetComponent<LevelGenerator>();
        _levelGenerator.Init(scoreModel);

        _uiManager = _canvas.GetComponent<UIManager>();
        _uiManager.Init(scoreModel);
    }
}