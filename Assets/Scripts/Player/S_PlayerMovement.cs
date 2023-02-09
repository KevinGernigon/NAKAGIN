using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float _moveSpeed;
    //[SerializeField] private float _sprintSpeed;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _wallRunSpeed;
    [SerializeField] private float _climbUpSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashSpeedChangeFactor;
    [SerializeField] private float _AerialSpeed;
    private float _desiredMoveSpeed;
    private float _lastDesiredMoveSpeed;
    private float _speedChangeFactor;
    public float _fallMultiplier;

    public MovementState _lastState;
    private bool _isKeepingMomentum;

    private float _speedIncreaseMultiplier;
    private float _slopeIncreaseMultiplier;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _airSpeed;


    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    public bool _readyToJump;

    [Header("Crouching")]
    [SerializeField] private float _crouchSpeed;
    [SerializeField] private float _crouchYScale;
    private float _startYScale;

    [Header("Keybinds")]
    //public KeyCode _jumpKey = KeyCode.Space;
    public KeyCode _sprintKey = KeyCode.LeftShift;
    public KeyCode _crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    public bool _isGrounded;

    [Header("Slope Handling")]
    [SerializeField] private float _maxSlopeAngle;
    private RaycastHit _slopeHit;
    private bool _exitingSlope;

    [Header("Grappling")]
    [SerializeField] public float _wantedSpeedGrappling = 2;
    [SerializeField] public float _wantedHeightGrappling = 2;
    [Header("Upgrade values")]
    private float _upgradeSpeedValue;
    private float _upgradeDashSpeed;
    public bool _ReachUpgradeBool;

    [Header("References")]
    [SerializeField] private S_Climbing ClimbingScript;
    [SerializeField] private S_Dash ScriptDash;
    [SerializeField] private S_WallRunning ScriptWallRun;
    [SerializeField] private Transform _orientation;


    [Header("Raycast")]
    [SerializeField] private float _valueRaycast;
    float _horizontalInput;
    float _verticalInput;
    

    Vector3 _moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        freeze,
        walking,
        crouching,
        sprinting,
        sliding,
        wallrunning,
        climbing,
        dashing,
        air
    }

    public bool _isSliding;
    public bool _isWallRunning;
    public bool _isClimbing;
    public bool _isDashing;
    public bool _isReachUpgrade;
    public bool _isGrappling;
    public bool _isFreezing;
    public bool _isGrappleActive;
    public bool _ResetDashSpeed;
    private bool _isEnableMovementOnNextTouch;
    private float _saveWalkSpeed;
    private bool canJumpLedge;
    private float _timerJump;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        _readyToJump = true;
        _saveWalkSpeed = _walkSpeed;
        _startYScale = transform.localScale.y;
        _upgradeSpeedValue = 1;
    }

    private void Update()
    {
        //Ground Check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + _valueRaycast, _whatIsGround);
        //_isGrounded = Physics.BoxCast(transform.position, Vector3.one, Vector3.down, Quaternion.identity, _playerHeight * 0.5f + _valueRaycast, _whatIsGround);

        InputCommand();
        SpeedControl();
        StateHandler();

        if (_isReachUpgrade)
        {
            SpeedValueUpgrade();
        }

        //handle drag
        if (state == MovementState.walking || state == MovementState.sprinting || state == MovementState.crouching && !_isGrappleActive)
        {
            rb.drag = _groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (_ReachUpgradeBool == false)
        {
            _dashSpeed = 25;
            _upgradeSpeedValue = 1;
        }

        if (state == MovementState.air)
        {
            _desiredMoveSpeed = _airSpeed;
        }

        if(state != MovementState.air && state != MovementState.dashing) //limit dash in air
        {
            ScriptDash._limitDash = 1;
        }

    }

    private void FixedUpdate()
    {
        MovingPlayer();
        
        if (_lastState != MovementState.dashing)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        }

        if (!_isGrounded)
        {
            _timerJump += Time.deltaTime;
            if (_timerJump < _jumpCooldown - 0.01f && _readyToJump)
            {
                canJumpLedge = true;
            }
            else
                canJumpLedge = false;

        }
        else _timerJump = 0f;
    }

    private void InputCommand()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetButtonDown("Jump") && _readyToJump && _isGrounded || (Input.GetButtonDown("Jump") && canJumpLedge))
        {
            _readyToJump = false;
            Jump();
            canJumpLedge = false;
            Invoke(nameof(ResetJump), _jumpCooldown);

            
        }
            //When crouch

        if (Input.GetKeyDown(_crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, _crouchYScale, transform.localScale.z);
            //add force cuz floating
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(_crouchKey))
        {
             transform.localScale = new Vector3(transform.localScale.x, _startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        //Mode - Grappin
        /*if (_isFreezing)
        {
            state = MovementState.freeze;
            //_moveSpeed = 0;
            //rb.velocity = Vector3.zero;
        }*/
        //Mode - Dashing
        if (_isDashing)
        {
            state = MovementState.dashing;
            _desiredMoveSpeed = _dashSpeed;
            _speedChangeFactor = _dashSpeedChangeFactor;
        }
        //Mode - Climbing
        else if (_isClimbing)
        {
            state = MovementState.climbing;
            _desiredMoveSpeed = _climbUpSpeed;
        }

        //Mode - WallRunning
        else if (_isWallRunning)
        {
            state = MovementState.wallrunning;
            _desiredMoveSpeed = _wallRunSpeed;
        }

        //Mode - Slide
        else if (_isSliding)
        {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f)
            {
                _desiredMoveSpeed = _slideSpeed;
            }
            else
            {
                _desiredMoveSpeed = _walkSpeed * GetComponent<S_Sliding>()._slideForce;
            }
        }
        //Mode - Crouch 
        else if(Input.GetKey(_crouchKey))
        {
            state = MovementState.crouching;
            _desiredMoveSpeed = _crouchSpeed;
        }
        //Mode - Sprint
        /*else if (_isGrounded && Input.GetKey(_sprintKey) && !_isWallRunning)
        {
            state = MovementState.sprinting;
            _desiredMoveSpeed = _sprintSpeed;
        }*/
        //Mode - Walk
        else if (_isGrounded)
        {
            state = MovementState.walking;
            _desiredMoveSpeed = _walkSpeed;
        }
        //Mode - Air
        else
        {
            state = MovementState.air;
        }

        if (Mathf.Abs(_desiredMoveSpeed - _lastDesiredMoveSpeed) > 4f && _moveSpeed != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoothlyLerpMoveSpeed());
        }
        else
        {
            _moveSpeed = _desiredMoveSpeed;
        }

        bool desiredMoveSpeedChanged = _desiredMoveSpeed != _lastDesiredMoveSpeed;
        if (_lastState == MovementState.dashing) _isKeepingMomentum = true;

        if (desiredMoveSpeedChanged)
        {
            if (_isKeepingMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                _moveSpeed = _desiredMoveSpeed;
            }
        }

        //_moveSpeed = _desiredMoveSpeed;
        _lastDesiredMoveSpeed = _desiredMoveSpeed;
        _lastState = state;
    }

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        // smoothly lerp movementSpeed to desired value
        float _time = 0;
        float _difference = Mathf.Abs(_desiredMoveSpeed - _moveSpeed);
        float _startValue = _moveSpeed;
        float boostFactor = _speedChangeFactor;
        while (_time < _difference)
        {
            _moveSpeed = Mathf.Lerp(_startValue, _desiredMoveSpeed, _time / _difference);

            if (OnSlope())
            {
                float _slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                float _slopeAngleIncrease = 1 + (_slopeAngle / 90f);

                _time += Time.deltaTime * _speedIncreaseMultiplier * _slopeIncreaseMultiplier * _slopeAngleIncrease;
            }
            else
                _time += Time.deltaTime * boostFactor;

                yield return null;
        }

        _moveSpeed = _desiredMoveSpeed;
        _speedChangeFactor = 1f;
        _isKeepingMomentum = false;
    }

    private void MovingPlayer()
    {
        if (_isGrappleActive) return;

        if (state == MovementState.dashing) return;
        if (ClimbingScript._isExitingWall) return;
        //calculate movement direction 
        _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;

        //on slope
        if (OnSlope() && !_exitingSlope)
        {
            rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            if (rb.velocity.y > 0 && _isSliding)
            {
                rb.AddForce(GetSlopeMoveDirection(_moveDirection) * _moveSpeed * 20f, ForceMode.Force);
            }
            else
                rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _upgradeSpeedValue, ForceMode.Force);
        }

        else if (_isGrounded) 
        {
            rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _upgradeSpeedValue, ForceMode.Force);
        }

        else if (!_isGrounded)
        {
            rb.AddForce(_moveDirection.normalized * _moveSpeed * _AerialSpeed * _airMultiplier * _upgradeSpeedValue, ForceMode.Force);
        }

        if (!_isWallRunning)
        {
            rb.useGravity = !OnSlope();
        }
    }

    private void SpeedControl()
    {
        if (_isGrappleActive) return;

        //limiting speed on slope
        if (OnSlope() && !_exitingSlope) 
        {
            if(rb.velocity.magnitude > _moveSpeed)
            {
                rb.velocity = rb.velocity.normalized * _moveSpeed;
            }
        }
        else
        {
            
            Vector3 _flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

             // limit velocity if needed
             if (_flatVelocity.magnitude > _moveSpeed)
             {
                    Vector3 _limitedVelocity = _flatVelocity.normalized * _moveSpeed;
                    rb.velocity = new Vector3(_limitedVelocity.x, rb.velocity.y, _limitedVelocity.z);
             }
        }
 
    }
    private void Jump()
    {
        _exitingSlope = true;
        _readyToJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        //rb.velocity = new Vector3(rb.velocity.x, ??, rb.velocity.z);

        rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);

    }

    private void ResetJump()
    {
        _readyToJump = true;

        _exitingSlope = false;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        _isGrappleActive = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        //rb.velocity = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        _isEnableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;
    }
    public void ResetRestrictions()
    {
        _isGrappleActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isEnableMovementOnNextTouch)
        {
            _isEnableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<S_GrappinV2>().StopGrapple();
        }
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f  + 0.3f))
        {
            float _angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return _angle < _maxSlopeAngle && _angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }
    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        float displacementX = endPoint.x - startPoint.x;
        float displacementZ = endPoint.z - startPoint.z;

        Vector3 displacementXZ = new Vector3(displacementX, 0f, displacementZ);


        if(Mathf.Abs(displacementX) < 20 && Mathf.Abs(displacementZ) < 20)
        {
            _wantedSpeedGrappling = 5f;
            _wantedHeightGrappling = 2.5f;
        }
        else
        {
            _wantedSpeedGrappling = 3f;
            _wantedHeightGrappling = 1.75f;
        }


        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * trajectoryHeight * gravity) * _wantedHeightGrappling;
        Vector3 velocityXZ = (displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity))) * _wantedSpeedGrappling;

        return velocityXZ + velocityY;
    }

    public void SpeedValueUpgrade()
    {
        _ReachUpgradeBool = true;

        if(_ReachUpgradeBool)
        {
            _upgradeSpeedValue += 2.5f;
            _dashSpeed += 15;
            StartCoroutine(upgradeSpeed());  
        }       
    }

    IEnumerator upgradeSpeed()
    {
        yield return new WaitForSeconds(2f);
        _ReachUpgradeBool = false;
    }
    
    IEnumerator leaveGround()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
