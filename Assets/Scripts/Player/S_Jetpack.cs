using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Jetpack : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody _rb;
    private S_PlayerMovement _pm;
    private S_Dash ScriptDash;
    private S_GrappinV2 GrappinScript;
    [SerializeField] private S_BatteryManager ScriptBatteryManager;
    [SerializeField] private Animator _arms_AC;

    [Header("Jetpack")]
    [SerializeField] private float _jetpackForce;
    [SerializeField] private float _jetpackUpwardForce;
    [SerializeField] private float _dividePer;
    [SerializeField] private bool _jetPackSave;
    [SerializeField] private float _valueJumpJetpack;
    private bool _isGravityDisable;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("HUD")]
    [SerializeField] private GameObject _HUDJetpackWarning;


    [Header("Time Value")]
    [SerializeField] private float _timerCd;
    [SerializeField] private float _timerMaxCd;
    [SerializeField] private float _timerJetpack = 0;
    [SerializeField] private float _maxTimerJetpack = 0.3f;
    private Vector3 saveForceToApplyInAir;
    private Vector3 saveForceToApplyOnGround;

    private bool _isTriggerBoxTrue;
    private bool _isMaxForce;
    private bool _isSoundActive;
    public bool _isPadUsed = false;
    
    [SerializeField] private bool _isJetpackAvaible;
    [SerializeField] private bool _isTimerReach;

    private string _jetpackAnim = "jetpack";

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        GrappinScript = GetComponent<S_GrappinV2>();
        ScriptDash = GetComponent<S_Dash>();

        _isJetpackAvaible = true;
    }


    void Update()
    {
        /*if (Input.GetButtonDown("Jetpack") && _isJetpackAvaible)
            JetpackFunction();*/
        if(S_InputManager._playerInputAction.Player.Jump.triggered){
            StartCoroutine(waitAfterJump());
        }

        if (!_pm._isGrounded && !_pm._isWallRunning)
        {
        
            if(!_pm._isWallRunning){
                _maxTimerJetpack = 0.5f;
            }
            else
                _maxTimerJetpack = 0.3f;

            _timerJetpack += Time.deltaTime;
            if (_timerJetpack < _maxTimerJetpack && _timerJetpack != 0)
            {
                 _isTimerReach = false;
            }
            else
            {
                 _isTimerReach = true;
            }
        }
        else{
            _timerJetpack = 0;
            _isTimerReach = true;
        }

        


        if (S_InputManager._playerInputAction.Player.Jetpack.triggered && _isJetpackAvaible && !_isPadUsed)
        {
            if (S_InputManager._jetpackActive)
            {
                JetpackFunction();
            }
        }
    }
    private void FixedUpdate()
    {
        if (_isTriggerBoxTrue)
        {
            _jetpackAnim = "jetpackSave";
            _isJetpackAvaible = true;
            ScriptDash._limitDash = 0;
            if (ScriptBatteryManager._nbrBattery >= 1)
            {
                _timerCd += Time.deltaTime;
                if (_timerCd < _timerMaxCd)
                {
                    Time.timeScale = 0.2f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }
        else if (!_isTriggerBoxTrue)
        {
            _jetpackAnim = "jetpack";
            _timerCd = 0;
            Time.timeScale = 1f;
        }
    }
    public void JetpackFunction()
    {
        if (!GrappinScript._isJetpackAvaible) return;

        if (ScriptBatteryManager._nbrBattery <= 0 && _isJetpackAvaible && !_isSoundActive)
        {
            PlayerSoundScript.NoBatterySound();
            StartCoroutine(EndSoundCoroutine());
        }

        if (_isTriggerBoxTrue && ScriptBatteryManager._nbrBattery >= 1)
        {
            PlayerSoundScript.JetpackSound();
            _arms_AC.Play("A_Arms_Jetpack");
            //_arms_AC.SetBool(_jetpackAnim, true);
            ScriptBatteryManager.UseOneBattery();
            JetPackUsage();
        }


        if (!_isTriggerBoxTrue && !_isMaxForce && ScriptBatteryManager._nbrBattery >= 1)
        {
            PlayerSoundScript.JetpackSound();
            _arms_AC.Play("A_Arms_Jetpack");
            //_arms_AC.SetBool(_jetpackAnim, true);
            ScriptBatteryManager.UseOneBattery();
            JetPackUsage();

        }

        if (_isGravityDisable)
             _rb.useGravity = false;
    }

    private void JetPackUsage()
    {
        Transform forwardT;

        forwardT = orientation;
        float i;

            if (_pm._isGrounded && _isTimerReach && !_pm._isDashing)
            {
                i = 35;
            }
            else if (_pm._isDashing)
            {
                i = 20;
            }
            else if(!_isTimerReach){
                i = 20;
            }
            else if (Mathf.Abs(_rb.velocity.y) <= 15)
                {
                    i = 25;
                }
            else
                {
                    i = Mathf.Abs(_rb.velocity.y) * 1.5f;
                }

            if(i >= 85)
            {
                i = 85;
            }

            if(_isTriggerBoxTrue){
                i = Mathf.Abs(_rb.velocity.y) * 1.5f;
                if(i > 100){
                    i = 100;
                }
            }

            
            

            //Vector3 forceToApply = (forwardT.forward * _jetpackForce) / _dividePer + (forwardT.up * _jetpackUpwardForce) / _dividePer * i / 20f;
            _rb.AddForce((Vector3.forward * _jetpackForce) + ((Vector3.up * _jetpackUpwardForce) *i), ForceMode.Impulse);
            //saveForceToApplyOnGround = forceToApply;

            StartCoroutine(waitForBoolean());
            Invoke(nameof(DelayedJetpackForce), 0.025f);
    }

    private void DelayedJetpackForce() 
    {
        _rb.AddForce(saveForceToApplyOnGround, ForceMode.Impulse);
        _isJetpackAvaible = false;

        ResetJetpack();
        ScriptDash._limitDash += 1;
        _isTriggerBoxTrue = false;
    }

    public void BooleanTriggerBoxEnter()
    {
        _isTriggerBoxTrue = true;

        if (ScriptBatteryManager._nbrBattery >= 1)
        {
            _HUDJetpackWarning.SetActive(true);
            PlayerSoundScript.StartSauvetageSound();
            _arms_AC.Play("A_Arms_Jetpack_Save");

        }


        if (S_InputManager._playerInputAction.Player.Jetpack.triggered)
        {
            if (S_InputManager._jetpackActive)
            { 
                JetpackFunction(); 
            }
        }
            
    }
   
    public void BooleanTriggerBoxExit()
    {
        _isTriggerBoxTrue = false;

        _HUDJetpackWarning.SetActive(false);

        PlayerSoundScript.EndSauvetageSound();
    }

    private void ResetJetpack()
    {
        StartCoroutine(waitForJetpack());
    }
    IEnumerator waitForBoolean()
    {
        yield return new WaitForSeconds(2f);
        _isMaxForce = false;
    }
    IEnumerator waitForJetpack()
    {
        yield return new WaitForSeconds(5f);
        _isJetpackAvaible = true;
    }

    IEnumerator EndSoundCoroutine()
    {
        _isSoundActive = true;
        yield return new WaitForSeconds(2f);
        _isSoundActive = false;
    }

    IEnumerator waitAfterJump(){
        _isJetpackAvaible = false;
        yield return new WaitForSeconds(0.1f);
        _isJetpackAvaible = true;
    }
}
