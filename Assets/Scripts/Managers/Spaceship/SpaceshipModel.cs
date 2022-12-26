public class SpaceshipModel
{
    private float _speed;
    private float _speedMultiplier;
    private float _zShipMovementAngle;
    private float _spaceshipTiltTime;
    private float _sideShiftSpeed;
    private bool _speedBoosted;

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

    public delegate void SpeedBoostedHandler(bool isBoosted);
    public event SpeedBoostedHandler OnSpeedBoosted;

    public SpaceshipModel(float speed, float speedMultiplier, float zShipMovementAngle, float spaceshipTiltTime,
        float sideShiftSpeed)
    {
        _speed = speed;
        _speedMultiplier = speedMultiplier;
        _zShipMovementAngle = zShipMovementAngle;
        _spaceshipTiltTime = spaceshipTiltTime;
        _sideShiftSpeed = sideShiftSpeed;
    }

    public void CheckSpeedBoostState(bool isBoosted)
    {
        OnSpeedBoosted?.Invoke(isBoosted);
    }
}