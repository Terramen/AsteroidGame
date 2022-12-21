using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipView : MonoBehaviour
{
    public void SpaceshipLeftSideMoving(float roadLengthX, float zShipMovementAngle, float sideShiftSpeed,
        float spaceshipTiltTime)
    {
        if (transform.position.x >= -roadLengthX)
        {
            SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, zShipMovementAngle), spaceshipTiltTime);
            SpaceshipSideMoving(-sideShiftSpeed);
        }
    }

    public void SpaceshipRightSideMoving(float roadLengthX, float zShipMovementAngle, float sideShiftSpeed,
        float spaceshipTiltTime)
    {
        if (transform.position.x <= roadLengthX)
        {
            SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, -zShipMovementAngle), spaceshipTiltTime);
            SpaceshipSideMoving(sideShiftSpeed);
        }
    }

    public void SpaceshipRotateTiltBack(float spaceshipTiltTime)
    {
        SpaceshipTilt(transform.rotation, Quaternion.Euler(0, 0, 0), spaceshipTiltTime);
    }

    private void SpaceshipTilt(Quaternion start, Quaternion end, float spaceshipTiltTime)
    {
        transform.rotation = Quaternion.Slerp(start, end, spaceshipTiltTime);
    }

    private void SpaceshipSideMoving(float speed)
    {
        transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
    }

    public void SpaceshipForwardMoving(float speed)
    {
        transform.position += new Vector3(0, 0, speed * Time.deltaTime);
    }
}