using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool _isShipBoosted;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private bool _isShooting;
    private static InputManager _instance = null;
    
    [SerializeField] private string _moveRight;
    [SerializeField] private string _moveLeft;
    [SerializeField] private string _shipBoostKey;
    [SerializeField] private string _shootKey;

    public bool IsShipBoosted => _isShipBoosted;

    public bool IsMovingLeft => _isMovingLeft;

    public bool IsMovingRight => _isMovingRight;

    public bool IsShooting => _isShooting;

    public static InputManager Instance => _instance;

    // Singleton for inputs
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
         
        _instance = this;

        DontDestroyOnLoad(gameObject);
    }
    

    private void Update()
    {
        _isShipBoosted = Input.GetButton(_shipBoostKey);
        _isMovingLeft = Input.GetButton(_moveLeft);
        _isMovingRight = Input.GetButton(_moveRight);
        _isShooting = Input.GetButtonDown(_shootKey);
    }
}
