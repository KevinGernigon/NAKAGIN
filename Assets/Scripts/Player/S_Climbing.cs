using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Climbing : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;
    

    [Header("References")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private S_Dash ScriptDash;
    [SerializeField] private S_Accelaration AccelerationScript;
    [SerializeField] private Animator _arms_AC;
    [SerializeField] private S_PlayerSound PlayerSoundScript;

    public S_PlayerMovement pm;
    [SerializeField] private LayerMask _whatIsClimbable;


    [Header("Climbing")]
    [SerializeField] private float _climbSpeed;
    [SerializeField] private float _maxClimbTime;
    private float _climbTimer;
    private bool _isClimbing;

    /*[Header("ClimbJumping")]
    [SerializeField] private float _climbJumpUpForce;
    [SerializeField] private float _climbJumpBackForce;*/


    //public KeyCode jumpKey = KeyCode.Space;


    [SerializeField] private int _climbJumps;
    [SerializeField] private int _climbJumpsLeft;
    [SerializeField] private int _counterClimbPropulsion;

    [Header("Detection")]
    [SerializeField] private float _detectionLength;
    [SerializeField] private float _sphereCastRadius;
    [SerializeField] public float _maxWallLookAngle;
    public float _wallLookAngle;

    private RaycastHit _frontWallHit;
    public bool _isWallFront;
    public bool _isWallWasFront;

    private Transform _lastWall;
    private Vector3 _lastWallNormal;
    [SerializeField] private float _minWallNormalAngleChange;

    [Header("Exiting")]
    public bool _isExitingWall;
    [SerializeField] private float _exitWallTime;
    private float _exitingWallTimer;
    public bool _isAchievedClimb;
    [SerializeField] private float _AnimationClimbTime;

    private void Start()
    {
        AccelerationScript = GetComponent<S_Accelaration>();
    }
    private void Update()
    {
        WallCheck();
        StateMachine();

        /*S_Debugger.UpdatableLog("_isClimbing", _isClimbing);
        S_Debugger.UpdatableLog("_isExitingWall", _isExitingWall);
        S_Debugger.UpdatableLog("_exitingWallTimer", _exitingWallTimer);
        S_Debugger.UpdatableLog("_wallLookAngle", _wallLookAngle);
        S_Debugger.UpdatableLog("_maxWallLookAngle", _maxWallLookAngle);*/

        if (_isClimbing && !_isExitingWall)
        {
            ClimbingMovement();
        }

    }

    private void StateMachine()
    {
        //State 1 - Climbing
        //if (_isWallFront && Input.GetKey(KeyCode.Z) && _wallLookAngle < _maxWallLookAngle)

        //if ((Input.GetButton("Vertical") || Input.GetButton("Jump")) && (_isWallFront  && _wallLookAngle < _maxWallLookAngle))  
        if ((S_InputManager._mouvementInput.y > 0 || S_InputManager._jumpInput) && (_isWallFront  && _wallLookAngle < _maxWallLookAngle))   
        //if (_isWallFront  && _wallLookAngle < _maxWallLookAngle)   
        {
            if (!_isClimbing && _climbTimer > 0)
            {
                StartClimbing();       
            }

            if (_climbTimer > 0)
            {
                _climbTimer -= Time.deltaTime;
            }
            if (_climbTimer <= 0)
            {
                StopClimbingByTime();

            }
        }
        //State 2 - Exiting
        else if (_isExitingWall)
        {
            if (_isClimbing)
            {
                StopClimbingByReachPoint();
            }

            if (_exitingWallTimer > 0) _exitingWallTimer -= Time.deltaTime;
            if (_exitingWallTimer < 0) _isExitingWall = false;
        }
        //State 3 - None
        else
        {
            if (_isClimbing)
            {
                StopClimbingByReachPoint();
            }
        }


        /*if (_isWallFront && Input.GetKeyDown(jumpKey) && _climbJumpsLeft > 0)
        {
            ClimbJump();
        }*/
    }
    private void WallCheck()
    {
        _isWallFront = Physics.SphereCast(transform.position, _sphereCastRadius, _orientation.forward, out _frontWallHit, _detectionLength, _whatIsClimbable);
        _wallLookAngle = Vector3.Angle(_orientation.forward, -_frontWallHit.normal);

        bool newWall = _frontWallHit.transform != _lastWall || Mathf.Abs(Vector3.Angle(_lastWallNormal, _frontWallHit.normal)) > _minWallNormalAngleChange;

        if (_isWallFront && newWall || pm._isGrounded)
        {
            _climbTimer = _maxClimbTime;
            _climbJumpsLeft = _climbJumps;
        }
    }

    private void StartClimbing()
    {
        //_arms_AC.Play("A_Arms_Climb");
        _arms_AC.SetBool("climbing", true);
        if(PlayerSoundScript.isPlayingClimb == false) StartCoroutine(PlayerSoundScript.ClimbSounds());
        pm._walkSpeed = 40;
        _isClimbing = true;
        pm._isClimbing = true;

        _lastWall = _frontWallHit.transform;
        _lastWallNormal = _frontWallHit.normal;
    }

    private void ClimbingMovement()
    {
        //rb.velocity = new Vector3(rb.velocity.x, _climbSpeed, rb.velocity.z);
        rb.velocity = new Vector3(rb.velocity.x, _climbSpeed, rb.velocity.z);
    }

    private void StopClimbingByReachPoint()
    {

        _isClimbing = false;
        pm._isClimbing = false;
        StartCoroutine(counterJumpAdjustment());
        _isAchievedClimb = true;  
        StartCoroutine(EndClimbingAnimation());
    }
    private void StopClimbingByTime()
    {
        _isClimbing = false;
        pm._isClimbing = false;
    }

    /*private void ClimbJump()
    {
        _isExitingWall = true;
        _exitingWallTimer = _exitWallTime;
        Vector3 forceToApply = transform.up * _climbJumpUpForce + _frontWallHit.normal * _climbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);

        _climbJumpsLeft--;
    }*/

    IEnumerator counterJumpAdjustment()
    {
        //Vector3 forceToApply = _orientation.forward;
        //yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => !_isWallFront);
        yield return new WaitForSeconds(0.05f); 
        rb.AddForce(Vector3.down * _counterClimbPropulsion, ForceMode.Impulse);   
    }

    IEnumerator EndClimbingAnimation()
    {
        yield return new WaitForSeconds(_AnimationClimbTime);
        _isAchievedClimb = false;
    }
}
