using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Jetpack : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody _rb;
    private S_PlayerMovement _pm;
    [SerializeField] private S_BatteryManager ScriptBatteryManager;

    [Header("Jetpack")]
    [SerializeField] private float _jetpackForce;
    [SerializeField] private float _jetpackUpwardForce;
    [SerializeField] private float _dividePer;
    private bool _isGravityDisable;


    private Vector3 saveForceToApplyInAir;
    private Vector3 saveForceToApplyOnGround;

    private bool _isTriggerBoxTrue;
    private bool _isMaxForce;
    
    [SerializeField] private bool _isJetpackAvaible;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        _isJetpackAvaible = true;
    }


    void Update()
    {
        if (Input.GetButtonDown("Jetpack") && _isJetpackAvaible)
            JetpackFunction();

    }

    public void JetpackFunction()
    {
        if(_isTriggerBoxTrue)
        {
            if(!ScriptBatteryManager._overdrive)
            {
                ScriptBatteryManager.UseThreeBattery();
                JetPackUsage();
            }
            else if(ScriptBatteryManager._overdrive)
            {
                ScriptBatteryManager._overdrive = false;
                JetPackUsage();
            }
        }

        if (!_isTriggerBoxTrue && !_isMaxForce && ScriptBatteryManager._nbrBattery >= 1)
            {
                    ScriptBatteryManager.UseOneBattery();
                    JetPackUsage();

            }
            else
            {
                    //Son effect + UI
            }
            if (_isGravityDisable)
            _rb.useGravity = false;
    }

    private void JetPackUsage()
    {
        Transform forwardT;

        forwardT = orientation;


        if (_isTriggerBoxTrue)
        {
            _isMaxForce = true;
            Vector3 forceToApply = forwardT.forward * _jetpackForce + forwardT.up * _jetpackUpwardForce;
            saveForceToApplyInAir = forceToApply;
        }
        else
        {
            Vector3 forceToApply = (forwardT.forward * _jetpackForce)/_dividePer + (forwardT.up * _jetpackUpwardForce)/ _dividePer;
            saveForceToApplyOnGround = forceToApply;
        }

        StartCoroutine(waitForBoolean());
        Invoke(nameof(DelayedJetpackForce), 0.025f);
    }


    private void DelayedJetpackForce() 
    {
        if(_isMaxForce)
        _rb.AddForce(saveForceToApplyInAir, ForceMode.Impulse);
        else
        _rb.AddForce(saveForceToApplyOnGround, ForceMode.Impulse);
        _isJetpackAvaible = false;

        ResetJetpack();
    }

    public void BooleanTriggerBoxEnter()
    {
        if(ScriptBatteryManager._nbrBattery >= 3 || ScriptBatteryManager._overdrive)
        {
            _isTriggerBoxTrue = true;
            JetpackFunction();
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
        Debug.Log("Enum");
        yield return new WaitForSeconds(5f);
        _isJetpackAvaible = true;
    }
}
