using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Sliding : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("References")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private GameObject _anim_bras;
    [SerializeField] private Transform _playerObj;
    [SerializeField] private S_PlayerCam ScriptPlayerCam;

    private Rigidbody rb;
    private S_PlayerMovement _pm;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("Sliding")]
    [SerializeField] private float _maxSlideTime;
    [SerializeField] private float _SlideValue;
    [SerializeField] public float _slideForce;
    [SerializeField] private float _slideYScale;
    private float _slideTimer;
    private float _startYScale;

    [Header("Input")]
    public KeyCode _slideKey = KeyCode.LeftControl;
    private float _horizontalInput;
    private float _verticalInput;

    [Header("Cooldown")]
    private float _slidingCdTimer;
    [SerializeField] private float _slidingCdMax;

    [Header("Bool")]
    public bool _isTrue;
    public int i;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        _startYScale = _playerObj.localScale.y;
        _slidingCdTimer = _slidingCdMax;
    }

    private void Update()
    {

        //if (Input.GetKeyDown(_slideKey))
        if (S_InputManager._playerInputAction.Player.Slide.triggered)
        { 
            _isTrue = true;
        }
        //else if (Input.GetKeyUp(_slideKey))
        else if (S_InputManager._playerInputAction.Player.Slide.ReadValue<float>() == 0)
        { 
            _isTrue = false;
        }

        //if (Input.GetKeyDown(_slideKey) && (_horizontalInput != 0 || _verticalInput != 0))
        if (S_InputManager._playerInputAction.Player.Slide.triggered && (_horizontalInput != 0 || _verticalInput != 0))
        {
            StartSlide();
        }

        //if (Input.GetKeyDown(_slideKey) && (_horizontalInput != 0 || _verticalInput != 0) && !_pm._isGrounded)
        if (S_InputManager._playerInputAction.Player.Slide.triggered && (_horizontalInput != 0 || _verticalInput != 0) && !_pm._isGrounded)
        {
            StartCoroutine(waitForGround());
        }

        if (!_isTrue)
            StopAllCoroutines();

        //if (Input.GetKeyUp(_slideKey) && _pm._isSliding)
        if ((S_InputManager._playerInputAction.Player.Slide.ReadValue<float>() == 0) && _pm._isSliding)
        {
            StopSlide();
            _pm._ReachUpgradeBool = false;
        }

        if (_slidingCdTimer > 0)
            _slidingCdTimer -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _horizontalInput = S_InputManager._mouvementInput.x;
        _verticalInput = S_InputManager._mouvementInput.y;

        //_horizontalInput = Input.GetAxisRaw("Horizontal");
        //_verticalInput = Input.GetAxisRaw("Vertical");


        if (_pm._isSliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        if (_slidingCdTimer > 0) return;

        if (_pm._isGrounded == true)
        {
            PlayerSoundScript.SlideSound();
            _anim_bras.GetComponent<Animator>().SetBool("isExitingSlope", false);
            if (ScriptPlayerCam._RandomCount == 1) _anim_bras.GetComponent<Animator>().Play("A_Left_Arm_Slide_Fall");
            else if (ScriptPlayerCam._RandomCount == 2) _anim_bras.GetComponent<Animator>().Play("A_Right_Arm_Slide_Fall");
            _pm._isSliding = true;
            _playerObj.localScale = new Vector3(_playerObj.localScale.x, _slideYScale, _playerObj.localScale.z);
            _anim_bras.transform.localScale = new Vector3(_anim_bras.transform.localScale.x, _slideYScale, _anim_bras.transform.localScale.z);
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
        if (!_pm.OnSlope() || rb.velocity.y > -0.1f || _pm.GetSlopeMoveDirection(_inputDirection).y <= -10 || _pm.GetSlopeMoveDirection(_inputDirection).y >= 10)
        {
            rb.AddForce(_inputDirection.normalized * _slideForce, ForceMode.Force);
            _slideTimer -= Time.deltaTime;
        }
        //sliding slope 
        else
        {
            rb.AddForce(_pm.GetSlopeMoveDirection(_inputDirection) * _slideForce, ForceMode.Force);
        }
        if (_slideTimer <= 0 || !_pm._isGrounded)
        {
            StopSlide();
        }

    }

    public void StopSlide()
    {
        PlayerSoundScript.EndSoundSlide();

        /*if (ScriptPlayerCam._RandomCount == 1) _anim_bras.GetComponent<Animator>().Play("A_Left_Arm_Slide_Up");
        else if (ScriptPlayerCam._RandomCount == 2) _anim_bras.GetComponent<Animator>().Play("A_Right_Slide_Up");*/
        _anim_bras.GetComponent<Animator>().SetBool("isExitingSlope", true);
        _pm._isSliding = false;
        _slidingCdTimer = _slidingCdMax;
        _playerObj.localScale = new Vector3(_playerObj.localScale.x, _startYScale, _playerObj.localScale.z);
        _anim_bras.transform.localScale = new Vector3(_anim_bras.transform.localScale.x, _startYScale, _anim_bras.transform.localScale.z);
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
