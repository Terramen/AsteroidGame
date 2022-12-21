using System;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float height;
    [SerializeField] private float heightDamping;
    [SerializeField] private float rotationDamping;
    [SerializeField] private float distanceDamping;
    [SerializeField] private Transform target;

    private float defaultDistance;
    [SerializeField] private float boostedDistance;

    public float DefaultDistance => defaultDistance;

    public float BoostedDistance => boostedDistance;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void Awake()
    {
        defaultDistance = distance;
    }
    
    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
        
        //DistanceChangeByShipBoost();
    }

    public void DistanceChangeByShipBoost(float endDistance)
    {
        distance = Mathf.Lerp(distance, endDistance, distanceDamping * Time.deltaTime);
    }
}