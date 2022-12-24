public class SpaceshipModel
{
    private float _speed;
    private float _speedMultiplier;
    private float _zShipMovementAngle;
    private float _spaceshipTiltTime;
    private float _sideShiftSpeed;
    private bool _speedBoosted;
    
    // TODO Possible remove later
    private float _roadLengthX;

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float SpeedMultiplier => _speedMultiplier;

    public float ZShipMovementAngle => _zShipMovementAngle;

    public float SpaceshipTiltTime => _spaceshipTiltTime;

    public float SideShiftSpeed => _sideShiftSpeed;

    public bool SpeedBoosted
    {
        get => _speedBoosted;
        set => _speedBoosted = value;
    }

    public float RoadLengthX => _roadLengthX;

    public delegate void SpeedBoostedHandler(bool isBoosted);
    public event SpeedBoostedHandler OnSpeedBoosted;

    public SpaceshipModel(float speed, float speedMultiplier, float zShipMovementAngle, float spaceshipTiltTime,
        float sideShiftSpeed, float roadLengthX)
    {
        _speed = speed;
        _speedMultiplier = speedMultiplier;
        _zShipMovementAngle = zShipMovementAngle;
        _spaceshipTiltTime = spaceshipTiltTime;
        _sideShiftSpeed = sideShiftSpeed;
        _roadLengthX = roadLengthX;
    }

    public void CheckSpeedBoostState(bool isBoosted)
    {
        OnSpeedBoosted?.Invoke(isBoosted);
    }
}