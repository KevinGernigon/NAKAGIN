using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerMovement : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Movement")]
    public float _moveSpeed;
    //[SerializeField] private float _sprintSpeed;
    [SerializeField] public float _walkSpeed;
    [SerializeField] private float _timerMaxSpeed;
    [SerializeField] private float _maxSpeedReachCooldown;
    [SerializeField] private float _slideSpeed;
    [SerializeField] private float _wallRunSpeed;
    [SerializeField] private float _climbUpSpeed;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _dashSpeedChangeFactor;
    [SerializeField] private float _AerialSpeed;
    private float _desiredMoveSpeed;
    private float _lastDesiredMoveSpeed;
    private float _speedChangeFactor;
    [SerializeField] private float _maxWalkSpeed;
    [SerializeField] private float _incrementValMaxSpeed;
    public float _fallMultiplier;
    public bool _isMaxSpeed;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    public MovementState _lastState;
    private bool _isKeepingMomentum;

    private float _speedIncreaseMultiplier;
    private float _slopeIncreaseMultiplier;
    [SerializeField] private float _groundDrag;
    [SerializeField] private float _airSpeed;


    [Header("Jumping")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float valJump;
    [SerializeField] private int _jumpCount;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    public bool _readyToJump;

    private float _startYScale;


    [Header("Ground Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask Everything;
    public bool _isGrounded;

    [Header("Slope Handling")]
    [SerializeField] private float _maxSlopeAngle;
    private RaycastHit _slopeHit;
    private bool _exitingSlope;
    [SerializeField] private float _slopeVectorDownValue;
    public float _actualSlopeAngle;

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
    [SerializeField] private S_GrappinV2 GrapplingScript;
    [SerializeField] private S_Accelaration AccelerationScript;
    [SerializeField] private S_Sliding SlideScript;
    [SerializeField] private S_PlayerCam PlayerCamScript;
    [SerializeField] private Transform _orientation;
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _arms_AC;

    [Header("Raycast")]
    [SerializeField] private float _valueRaycast;

    [Header("AirTime")]
    private float _timeInAir = 0f;
    private float _minRequirementTimeInAir = 0.5f;

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
    public bool _isButtonEnabled;
    public bool _isSlopePositive;
    public bool _isMoving;
    public bool _isHigherThan;

    public bool _isAccelerating;
    public bool _isDecelerating;
    public bool _whatIsWallOnGround;

    int i = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        AccelerationScript = GetComponent<S_Accelaration>();
        SlideScript = GetComponent<S_Sliding>();
        rb.freezeRotation = true;
        _readyToJump = true;
        _saveWalkSpeed = _walkSpeed;
        _startYScale = transform.localScale.y;
        _upgradeSpeedValue = 1;
        _isButtonEnabled = true;
        _isDecelerating = false;
    }

    private void Update()
    {
        if (GetSlopeMoveDirection(_moveDirection).y >= 0f && OnSlope())
        {
            _isSlopePositive = true;
            _moveSpeed = _walkSpeed;
        }
        else
            _isSlopePositive = false;

        //Ground Check
        //_isGrounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + _valueRaycast, _whatIsGround);

        if (Physics.CheckSphere(transform.position, 1.1f, _whatIsGround) || Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + _valueRaycast, _whatIsWall)){
            _isGrounded = true;
        }
        else
            _isGrounded = false;
        
        if(Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + _valueRaycast, _whatIsWall))
        {
            _whatIsWallOnGround = true;
        }
        else
        {
            _whatIsWallOnGround = false;
        }

        if (!_isGrounded && _jumpCount >= 0)
        {
            _jumpCount = 0;
            _jumpForce = 0;
        }
        else _jumpForce = valJump;
        //_isGrounded = Physics.BoxCast(transform.position, Vector3.one, Vector3.down, Quaternion.identity, _playerHeight * 0.5f + _valueRaycast, _whatIsGround);

        InputCommand();
        SpeedControl();
        StateHandler();


        //handle drag
        //if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && state == MovementState.walking && !Input.GetButton("Jump") && !GrapplingScript._isDecreaseRbDrag)
        if (_horizontalInput == 0 && _verticalInput == 0 && state == MovementState.walking && S_InputManager._playerInputAction.Player.Jump.ReadValue<float>() == 0 && !GrapplingScript._isDecreaseRbDrag)
        {
            _isAccelerating = false;
            _isDecelerating = true;
            AccelerationScript.VarianceVitesse();
            PlayerSoundScript.EndSoundWalk();

            if (_arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Arms_Jump_Down") 
            {
                if (_arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    _arms_AC.Play("A_Arms_Idle_Placeholder");
                }
            }
            else
            {
                _arms_AC.Play("A_Arms_Idle_Placeholder");
            }

            rb.drag = _groundDrag + 10;
            _isMoving = false;
        }
        else if (state == MovementState.walking && !GrapplingScript._isDecreaseRbDrag)
        {
            if (S_InputManager._playerInputAction.Player.Jump.ReadValue<float>() == 1) return;
            _isAccelerating = true;
            _isDecelerating = false;
            AccelerationScript.VarianceVitesse();
            PlayerSoundScript.WalkSound();
            if (_arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Arms_Jump_Down")
            {
                if (_arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    _arms_AC.Play("A_Arms_Running");
                }
            }
            else
            {
                _arms_AC.Play("A_Arms_Running");
            }
            rb.drag = _groundDrag;
            _isMoving = true;
        }
        else
        {
            PlayerSoundScript.EndSoundWalk();
            rb.drag = 0;
            _isMoving = false;
        }

        if (state == MovementState.air)
        {
            PlayerSoundScript.EndSoundWalk();
            if(_arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Arms_Jump_Impulse")
            {
                if (_arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    _arms_AC.Play("A_Arms_Jump_Idle");
                }
            }
            else if(!_arms_AC.IsInTransition(0))
            {
                _arms_AC.Play("A_Arms_Jump_Idle");
            }
            _desiredMoveSpeed = _airSpeed;

        }

        /*if (state != MovementState.air && state != MovementState.dashing) //limit dash in air
        {
            ScriptDash._limitDash = 3;
        }*/

        if (OnSlope())
            _player.GetComponent<CapsuleCollider>().material.dynamicFriction = 2f;
        else
            _player.GetComponent<CapsuleCollider>().material.dynamicFriction = 0f;
    }

    private void FixedUpdate()
    {
        MovingPlayer();


        if (state == MovementState.air || _isHigherThan)
        {
            _timeInAir += Time.fixedDeltaTime;
            if (_timeInAir >= _minRequirementTimeInAir)
            {
                _isHigherThan = true;
                if (Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.7f + _valueRaycast, _whatIsGround) && _isHigherThan)
                {
                    i++;
                    if (i >= 2)
                    {
                        //Debug.Log("GroundContact");
                        _isHigherThan = false;
                        PlayerSoundScript.LandingSound();
                        _arms_AC.Play("A_Arms_Jump_Down");
                        i = 0;
                    }
                   
                }
            }
        }
        else
        {
            _timeInAir = 0f;
            _isHigherThan = false;
            i = 0;
        }

        if (_lastState != MovementState.dashing)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * _fallMultiplier * Time.deltaTime;
        }

        if (!_isGrounded && _jumpCount == 0)
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

        //if (Input.GetButton("Vertical"))
        if (_verticalInput != 0 )
        {
            _timerMaxSpeed += Time.deltaTime;
            if (_timerMaxSpeed >= _maxSpeedReachCooldown)
            {
                _isMaxSpeed = true;
            }
            else _isMaxSpeed = false;
        }
        else _timerMaxSpeed = 0f;
    }

    private void InputCommand()
    {

        _horizontalInput = S_InputManager._mouvementInput.x;
        _verticalInput = S_InputManager._mouvementInput.y;

        //_horizontalInput = Input.GetAxisRaw("Horizontal");
        //_verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        //if (Input.GetButtonDown("Jump") && _readyToJump && _isGrounded || (Input.GetButtonDown("Jump") && canJumpLedge))
            if (S_InputManager._playerInputAction.Player.Jump.triggered && _readyToJump && _isGrounded || (S_InputManager._playerInputAction.Player.Jump.triggered && canJumpLedge))
            {
                _readyToJump = false;
                Jump();
                canJumpLedge = false;
                Invoke(nameof(ResetJump), _jumpCooldown);
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
            if (!_arms_AC.IsInTransition(0))
            {
                _arms_AC.Play("A_Arms_Wall_Destruction_01");
            }
            state = MovementState.dashing;
            _desiredMoveSpeed = _dashSpeed;
            _speedChangeFactor = _dashSpeedChangeFactor;
        }
        //Mode - Climbing
        else if (_isClimbing)
        {
            _arms_AC.Play("A_Arms_Climb");
            state = MovementState.climbing;
            _desiredMoveSpeed = _climbUpSpeed;
        }

        //Mode - WallRunning
        else if (_isWallRunning)
        {
            if (ScriptWallRun._isWallLeft) _arms_AC.Play("A_Left_Arm_Wall_Grab");
            else if (ScriptWallRun._isWallRight) _arms_AC.Play("A_Right_Arm_Wall_Grab");

            state = MovementState.wallrunning;
            _desiredMoveSpeed = _wallRunSpeed;
        }

        //Mode - Slide
        else if (_isSliding)
        {
            state = MovementState.sliding;
            //if(OnSlope() && rb.velocity.y > 0f)
            _desiredMoveSpeed = _walkSpeed * GetComponent<S_Sliding>()._slideForce;
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

        if (_isClimbing)
        {
            _moveDirection = Vector3.up;
            return;
        }
        else
        {
            _moveDirection = _orientation.forward * _verticalInput + _orientation.right * _horizontalInput;
        }

        if (_isMaxSpeed)
        {
            if (_walkSpeed <= _maxWalkSpeed)
                _walkSpeed += _incrementValMaxSpeed;
        }

        //calculate movement direction 
        


        //on slope
        if (OnSlope() && !_exitingSlope)
        {
            rb.AddForce(Vector3.down * _slopeVectorDownValue, ForceMode.Force);
            if (rb.velocity.y > 0 && _isSliding)
            {
                rb.AddForce(GetSlopeMoveDirection(_moveDirection) * _moveSpeed * 12.5f, ForceMode.Force);
            }
            else if(_isSliding)
                rb.AddForce(GetSlopeMoveDirection(_moveDirection) * _moveSpeed * 20f, ForceMode.Force);
            else    
                rb.AddForce(GetSlopeMoveDirection(_moveDirection) * _moveSpeed * 15f, ForceMode.Force);
                //rb.AddForce(_moveDirection.normalized * _moveSpeed * 20f * _upgradeSpeedValue, ForceMode.Force);
        }

        else if (_isGrounded)
        {
            rb.AddForce(_moveDirection.normalized * _moveSpeed * 10f * _upgradeSpeedValue, ForceMode.Force);
        }

        else if (!_isGrounded || (!_isGrounded && _isGrappleActive))
        {
            rb.AddForce(_moveDirection.normalized * _moveSpeed * _AerialSpeed * _airMultiplier, ForceMode.Force);
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
        if (OnSlope() && !_exitingSlope && !_isGrounded)
        {
                if (rb.velocity.magnitude > _moveSpeed)
                {
                //rb.velocity = rb.velocity.normalized * _moveSpeed;
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
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
    public void Jump()
    {
        if (!canJumpLedge)
        {
            if (state == MovementState.air) return;
        }

        _jumpCount++;

        _arms_AC.Play("A_Arms_Jump_Impulse");
        PlayerSoundScript.JumpSound();
        if (!OnSlope() && _whatIsWallOnGround)
        {
            rb.AddForce(transform.up * _jumpForce * 1.5f, ForceMode.Impulse);
        }
        else if(_isSliding && OnSlope())
        {
            rb.AddForce(transform.up * _jumpForce * 1.2f, ForceMode.Impulse);
        }
        else if (OnSlope() && !_isSliding)
        {
            rb.AddForce(transform.up * _jumpForce * 1.15f, ForceMode.Impulse);
        }
        else if (_isSliding && !OnSlope())
        {
            rb.AddForce(transform.up * _jumpForce * 1f, ForceMode.Impulse);
        }
        else if (_isDashing)
        {
            rb.AddForce(transform.up * _jumpForce * (0.8f-ScriptDash._dashDuration), ForceMode.Impulse);
        }
        else if (GetSlopeMoveDirection(_moveDirection).y != 0 || _isGrounded)
        {
            rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
        else if (!canJumpLedge)
        {
            _exitingSlope = true;
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            //rb.velocity = new Vector3(rb.velocity.x, ??, rb.velocity.z);
            rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private void ResetJump()
    {
        _readyToJump = true;

        _exitingSlope = false;

        canJumpLedge = true;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        _isGrappleActive = true;
        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        //rb.velocity = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 1f);
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
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float _angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            _actualSlopeAngle = _angle;
            return _angle < _maxSlopeAngle && (_angle >= 5 || _angle <= -5);
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



        if (Mathf.Abs(displacementX) >= 40 || Mathf.Abs(displacementZ) >= 40)
        {
            _wantedSpeedGrappling = 2f;
            _wantedHeightGrappling = 1.80f;
        }
        else if (Mathf.Abs(displacementX) >= 30 || Mathf.Abs(displacementZ) >= 30)
        {
            _wantedSpeedGrappling = 2.25f;
            _wantedHeightGrappling = 1.75f;
        }
        else if (Mathf.Abs(displacementX) >= 20 || Mathf.Abs(displacementZ) >= 20)
        {
            _wantedSpeedGrappling = 2.50f;
            _wantedHeightGrappling = 2f;
        }
        else if (Mathf.Abs(displacementX) >= 10 || Mathf.Abs(displacementZ) >= 10)
        {
            _wantedSpeedGrappling = 2.75f;
            _wantedHeightGrappling = 2f;
        }
        else if (Mathf.Abs(displacementX) >= 0 || Mathf.Abs(displacementZ) >= 0)
        {
            _wantedSpeedGrappling = 4f;
            _wantedHeightGrappling = 2f;
        }



        /*if (Mathf.Abs(displacementX) < 20 && Mathf.Abs(displacementZ) < 20)
        {
            _wantedSpeedGrappling = 4f;
            _wantedHeightGrappling = 2f;
        }
        else
        {
            _wantedSpeedGrappling = 3f;
            _wantedHeightGrappling = 1.5f;
        }*/


        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * trajectoryHeight * gravity) * _wantedHeightGrappling;
        Vector3 velocityXZ = (displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity))) * _wantedSpeedGrappling;

        return velocityXZ + velocityY;
    }  
}
