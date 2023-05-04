using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_WallRunning : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("WallRunning")]
    public LayerMask _whatIsWall;
    public LayerMask _whatIsGround;
    [SerializeField] private float _wallRunForce;
    [SerializeField] private float _wallClimbSpeed;
    [SerializeField] private float _wallJumpUpForce;
    [SerializeField] private float _wallJumpSideForce;

    [SerializeField] private float _maxWallRunTime;
    private float _wallRunTimer;
    private int _wallJumpsDone;
    [SerializeField] private Transform _lastWall;


    [Header("Input")]
    //public KeyCode jumpKey = KeyCode.Space;

    /*public KeyCode _upwardsRunKey = KeyCode.LeftShift;
    public KeyCode _downwardsRunKey = KeyCode.LeftControl;
    private bool _isUpwardsRunning;
    private bool _isDownwardsRunning;*/

    private float _horizontalInput;
    private float _verticalInput;

    [Header("Limitations")]
    [SerializeField] private int _allowedWallJumps = 1;

    [Header("Detection")]
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _minJumpHeight;
    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;
    public bool _isWallLeft;
    public bool _isWallRight;
    [SerializeField] private bool _isWallRemembered;
    [SerializeField] private float _angleValue;
    [SerializeField] private float _wallRunTimeClimb;
    [SerializeField] private float _wallRunTimeClimbRef;
    private bool _isWallRunEnding;

    [Header("Exiting")]
    private bool _isExitingWall;
    [SerializeField] private float _exitWallTime;
    private float _exitWallTimer;

    [Header("Gravity")]
    public bool _isUsingGravity;
    [SerializeField] private float _gravityCounterForce;

    [Header("References")]
    public Transform _orientation;
    private S_PlayerMovement pm;
    [SerializeField] private S_Climbing ClimbScript;
    private Rigidbody rb;
    [SerializeField] private Animator _arms_AC;

    private bool _isJumpForgivenessActive;
    private bool canWallJumpLedge;
    private float _timerWallJump;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<S_PlayerMovement>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        _wallRunTimeClimb = _wallRunTimeClimbRef;
        _isJumpForgivenessActive = false;
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
        if (pm._isWallRunning)
            _wallRunTimeClimb -= Time.deltaTime;

        if (_wallRunTimeClimb <= 0)
        {
            _isWallRunEnding = true;
            WallRunningMovement();
        }

        if (_isExitingWall)
            _wallRunTimeClimb = _wallRunTimeClimbRef;

        if (!pm._isWallRunning && _isJumpForgivenessActive)
        {
            _timerWallJump += Time.deltaTime;
            if (_timerWallJump < 0.25f)
            {
                canWallJumpLedge = true;
            }
            else
                canWallJumpLedge = false;
        }
        else _timerWallJump = 0f;
    }

    private void FixedUpdate()
    {
        if (pm._isWallRunning)
        {
            WallRunningMovement();
        }
    }
    private void CheckForWall()
    {
        //Vector3 currentAnglesRight = _orientation.right; //original Right
        //Vector3 currentAnglesLeft = -_orientation.right; //original Left
        //Vector3 currentAnglesRight = -_orientation.forward+_orientation.right; //degueux mais fonctionne à peu près
        Vector3 currentAnglesLeftv2 = Quaternion.AngleAxis(-_angleValue, _orientation.up) * _orientation.forward;
        Vector3 currentAnglesRightv2 = Quaternion.AngleAxis(_angleValue, _orientation.up) * _orientation.forward;

        _isWallRight = Physics.Raycast(transform.position, currentAnglesRightv2, out _rightWallHit, _wallCheckDistance, _whatIsWall);
        _isWallLeft = Physics.Raycast(transform.position, currentAnglesLeftv2, out _leftWallHit, _wallCheckDistance, _whatIsWall);

        if ((_isWallLeft || _isWallRight) && NewWallHit())
        {
            _wallJumpsDone = 0;
            _wallRunTimer = _maxWallRunTime;
        }
    }

    private void RememberLastWall()
    {
        if (_isWallLeft)
        {
            _lastWall = _leftWallHit.transform;
        }

        if (_isWallRight)
        {
            _lastWall = _rightWallHit.transform;
        }
        //add reset _lastWall
    }

    private bool NewWallHit()
    {
        if (_lastWall == null)
        {
            return true;
        }

        if (_isWallLeft && _leftWallHit.transform != _lastWall)
        {
            return true;
        }

        else if (_isWallRight && _rightWallHit.transform != _lastWall)
        {
            return true;
        }

        return false;
    }



    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, _minJumpHeight, _whatIsGround);
    }

    private void StateMachine()
    {
        // Getting inputs
        _horizontalInput = S_InputManager._mouvementInput.x;
        _verticalInput = S_InputManager._mouvementInput.y;

        //_horizontalInput = Input.GetAxisRaw("Horizontal");
        //_verticalInput = Input.GetAxisRaw("Vertical");

        /*_isUpwardsRunning = Input.GetKey(_upwardsRunKey);
        _isDownwardsRunning = Input.GetKey(_downwardsRunKey);*/

        //State 1 - WallRunning
        if ((_isWallLeft || _isWallRight) && _verticalInput > 0 && AboveGround() && !_isExitingWall)
        {
            //start wallrun
            if (!pm._isWallRunning && !pm._isGrounded)
            {
                StartWallRun();

            }

            if (_wallRunTimer > 0)
            {
                _wallRunTimer -= Time.deltaTime;
            }

            if (_wallRunTimer <= 0 && pm._isWallRunning)
            {
                _isExitingWall = true;
                _exitWallTimer = _exitWallTime;
            }


            // walljump
            //if (Input.GetKeyDown(jumpKey))
            if (S_InputManager._playerInputAction.Player.Jump.triggered)
            {
                WallJump();
            }
        }
        //State 2 - Exiting
        else if (_isExitingWall)
        {
            if(canWallJumpLedge && S_InputManager._playerInputAction.Player.Jump.triggered)
            {
                WallJump();
            }

            if (pm._isWallRunning)
            {
                StopWallRun();

            }
            if (_exitWallTimer > 0)
            {
                _exitWallTimer -= Time.deltaTime;
            }

            if (_exitWallTimer <= 0)
            {
                _isExitingWall = false;
            }
        }
        //State 3 - None
        else
        {
            if (canWallJumpLedge && S_InputManager._playerInputAction.Player.Jump.triggered)
            {
                WallJump();
            }

            if (pm._isWallRunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        pm._isWallRunning = true;

        _wallRunTimer = _maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        ClimbScript._maxWallLookAngle = 0;

        _isWallRemembered = false;

        _isJumpForgivenessActive = true;
    }


    private void WallRunningMovement()
    {
        rb.useGravity = _isUsingGravity;
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 _wallNormal = _isWallRight ? _rightWallHit.normal : _leftWallHit.normal;
        Vector3 _wallForward = Vector3.Cross(_wallNormal, transform.up);

        if ((_orientation.forward - _wallForward).magnitude > (_orientation.forward - -_wallForward).magnitude)
        {
            _wallForward = -_wallForward;
        }

        //forward force
        rb.AddForce(_wallForward * _wallRunForce, ForceMode.Force);
        PlayerSoundScript.WallRunSound();
        /*if (_arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Left_Arm_Wall_Grab" || _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Left_Arm_Wall_Run" || _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Right_Arm_Wall_Grab" || _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name == "A_Right_Arm_Wall_Run")
        {
            if (_isWallLeft && _arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) _arms_AC.Play("A_Left_Arm_Wall_Grab");
            else if (_isWallRight && _arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) _arms_AC.Play("A_Right_Arm_Wall_Grab");
        }
        else*/
        /*if(_arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name != "A_Left_Arm_Wall_Grab" && _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name != "A_Left_Arm_Wall_Run" && _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name != "A_Right_Arm_Wall_Grab" && _arms_AC.GetCurrentAnimatorClipInfo(0)[0].clip.name != "A_Right_Arm_Wall_Run")
        {
            if (_isWallLeft) _arms_AC.Play("A_Left_Arm_Wall_Grab");
            else if (_isWallRight) _arms_AC.Play("A_Right_Arm_Wall_Grab");
        }*/
        if (_isWallLeft) _arms_AC.SetBool("leftWall", true);
        else if (_isWallRight) _arms_AC.SetBool("rightWall", true);


        //upwards/downwards force
        if (_isWallRunEnding)
        {
            _wallRunTimeClimb = _wallRunTimeClimbRef;
            rb.velocity = new Vector3(rb.velocity.x, -_wallClimbSpeed, rb.velocity.z);
            _isWallRunEnding = false;
        }

        //push to wall force
        if (!_isExitingWall && !(_isWallLeft && _horizontalInput > 0) && !(_isWallRight && _horizontalInput < 0))
        {
            rb.AddForce(-_wallNormal * 100, ForceMode.Force);
        }

        if (_isUsingGravity)
        {
            rb.AddForce(transform.up * _gravityCounterForce, ForceMode.Force);
        }

        if (!_isWallRemembered)
        {
            RememberLastWall();
            _isWallRemembered = true;
        }
    }

    private void StopWallRun()
    {
        _arms_AC.SetBool("rightWall", false);
        _arms_AC.SetBool("leftWall", false);
        _arms_AC.SetBool("stopWallRun", true);
        PlayerSoundScript.EndWallRunSound();
        pm._isWallRunning = false;
        ClimbScript._maxWallLookAngle = 30f;
    }

    //Reset _lastWall
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            _lastWall = null;
            _wallRunTimeClimb = _wallRunTimeClimbRef;
        }
    }


    private void WallJump()
    {
        PlayerSoundScript.JumpSound();
        /*if (_isWallLeft) _arms_AC.Play("A_Left_Arm_Wall_Jump");
        else _arms_AC.Play("A_Right_Arm_Wall_Jump");*/
        _arms_AC.SetBool("hasWallJumped", true);
        _isJumpForgivenessActive = false;
        canWallJumpLedge = false;
        bool firstJump = true;
        //enter exiting wall state

        _isExitingWall = true;
        _exitWallTimer = _exitWallTime;

        Vector3 wallNormal = _isWallRight ? _rightWallHit.normal : _leftWallHit.normal;

        Vector3 forceToApply = transform.up * _wallJumpUpForce + wallNormal * _wallJumpSideForce;

        firstJump = _wallJumpsDone < _allowedWallJumps;
        _wallJumpsDone++;

        if (!firstJump)
            forceToApply = new Vector3(forceToApply.x, 0f, forceToApply.z);
        //add force

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        RememberLastWall();

        StopWallRun();
    }
}