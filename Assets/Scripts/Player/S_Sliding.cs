using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Sliding : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    private Transform _orientation;
    [SerializeField]
    private Transform _playerObj;

    private Rigidbody rb;
    private S_PlayerMovement _pm;

    [Header("Sliding")]
    [SerializeField] private float _maxSlideTime;
    [SerializeField] private float _SlideValue;

    [SerializeField] public float _slideForce;
    private float _slideTimer;

    [SerializeField] private float _slideYScale;

    private float _startYScale;

    [Header("Input")]
    public KeyCode _slideKey = KeyCode.LeftControl;
    private float _horizontalInput;
    private float _verticalInput;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();

        _startYScale = _playerObj.localScale.y;
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_slideKey) && (_horizontalInput != 0 || _verticalInput != 0))
        {
            StartSlide();
        }

        if(Input.GetKeyDown(_slideKey) && (_horizontalInput != 0 || _verticalInput != 0) && !_pm._isGrounded)
        {
            StartCoroutine(waitForGround());
        }

        if(Input.GetKeyUp(_slideKey) && _pm._isSliding)
        {
            StopSlide();
            _pm._ReachUpgradeBool = false;
        }
    }

    private void FixedUpdate()
    {
        if (_pm._isSliding)
        {
            SlidingMovement();
        }
    }

        private void StartSlide()
    {
        if (_pm._isGrounded == true)
        {
            _pm._isSliding = true;
            _playerObj.localScale = new Vector3(_playerObj.localScale.x, _slideYScale, _playerObj.localScale.z);
            rb.AddForce(Vector3.down * _SlideValue, ForceMode.Impulse);

            _slideTimer = _maxSlideTime;
        }
        else
        {
            _pm._isSliding = false;
        }

        
    }

    private void SlidingMovement()
    {
        Vector3 _inputDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        //sliding normal 
        if (!_pm.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(_inputDirection.normalized * _slideForce, ForceMode.Force);
            _slideTimer -= Time.deltaTime;
        }
        
        //sliding slope 
        else
        {
            rb.AddForce(_pm.GetSlopeMoveDirection(_inputDirection) * _slideForce, ForceMode.Force);
        }

        if (_slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        _pm._isSliding = false;

        _playerObj.localScale = new Vector3(_playerObj.localScale.x, _startYScale, _playerObj.localScale.z);
    }

    public void EnterSlideForRamp()
    {
        _maxSlideTime = 10f;
        _slideTimer = _maxSlideTime;
    }
    public void EndSlideForRamp()
    {
        _maxSlideTime = 0.75f;
        _slideTimer = _maxSlideTime;
    }



    IEnumerator waitForGround()
    {
        yield return new WaitUntil(() => _pm._isGrounded);
        StartSlide();
    }


}
