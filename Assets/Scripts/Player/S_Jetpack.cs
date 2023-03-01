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
    [SerializeField] private S_BatteryManager ScriptBatteryManager;

    [Header("Jetpack")]
    [SerializeField] private float _jetpackForce;
    [SerializeField] private float _jetpackUpwardForce;
    [SerializeField] private float _dividePer;
    [SerializeField] private bool _jetPackSave;
    private bool _isGravityDisable;

    [Header("Time Value")]
    [SerializeField] private float _timerCd;
    [SerializeField] private float _timerMaxCd;
    private Vector3 saveForceToApplyInAir;
    private Vector3 saveForceToApplyOnGround;

    private bool _isTriggerBoxTrue;
    private bool _isMaxForce;
    
    [SerializeField] private bool _isJetpackAvaible;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        ScriptDash = GetComponent<S_Dash>();
        _isJetpackAvaible = true;
    }


    void Update()
    {
        /*if (Input.GetButtonDown("Jetpack") && _isJetpackAvaible)
            JetpackFunction();*/

        if (S_InputManager._playerInputAction.Player.Jetpack.triggered && _isJetpackAvaible)
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
            _timerCd = 0;
            Time.timeScale = 1f;
        }
    }
    public void JetpackFunction()
    {
        if(_isTriggerBoxTrue && ScriptBatteryManager._nbrBattery >= 1)
        {
                ScriptBatteryManager.UseOneBattery();
                JetPackUsage();
        }


        if (!_isTriggerBoxTrue && !_isMaxForce && ScriptBatteryManager._nbrBattery >= 1)
            {
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


            if (Mathf.Abs(_rb.velocity.y) >= 10 && Mathf.Abs(_rb.velocity.y) <= 20)
                {
                    i = 25;
                }
            else if (Mathf.Abs(_rb.velocity.y) <= 2)
                {
                    i = 25;
                }
            else if(Mathf.Abs(_rb.velocity.y) >= 2 && Mathf.Abs(_rb.velocity.y) <= 10)
                {
                    i = 15;
                }
        /*else if (Mathf.Abs(_rb.velocity.y) <= 40 && (Mathf.Abs(_rb.velocity.y) >= 20))
            {
                i = 60;
            }

        else if (Mathf.Abs(_rb.velocity.y) <= 60 && (Mathf.Abs(_rb.velocity.y) >= 40))
            {
                i = 100;
            }*/
        else
                {
                    i = Mathf.Abs(_rb.velocity.y) * 1.2f;
                }

            Vector3 forceToApply = (forwardT.forward * _jetpackForce) / _dividePer + (forwardT.up * _jetpackUpwardForce) / _dividePer * i / 20f;
            saveForceToApplyOnGround = forceToApply;

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

           /* if (Input.GetButtonDown("Jetpack"))
                JetpackFunction();*/
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
}
