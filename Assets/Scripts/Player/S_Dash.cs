using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Dash : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody _rb;
    private S_PlayerMovement _pm;
    private S_GrappinV2 _grappinScript;
    [SerializeField] private Animator _arms_AC;

    [Header("Dashing")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashUpwardForce;
    [SerializeField] public float _dashDuration;
    public float _dashUpgradeForce;
    private float lastPressTime;
    public float _limitDash = 3;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("UI")]
    [SerializeField] private GameObject _hudDashFleche1;
    [SerializeField] private GameObject _hudDashFleche2;
    [SerializeField] private GameObject _hudDashFleche3;
    [Header("")]
    [SerializeField] private GameObject _hudDashJauge1;
    [SerializeField] private GameObject _hudDashJauge2;
    [SerializeField] private GameObject _hudDashJauge3;

    [Header("Settings")]
    [SerializeField] private bool _isUsingCameraForward = true;
    [SerializeField] private bool _isAllowingAllDirections = true;
    [SerializeField] private bool _isDisablingGravity = true;
    [SerializeField] private bool _isResettingVel = true;

    [Header("Cooldown")]
    [SerializeField] private float _dashCd;
    [SerializeField] private float _dashCdTimer;
    [SerializeField] private float _dashGain;
    [SerializeField] private float _dashGainTimer;

    [Header("Input")]
    //public KeyCode dashKey = KeyCode.E;

    public bool AxelIsHere = false;
    private const float DOUBLE_CLICK_TIME = .2f;
    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        _dashUpgradeForce = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        InputManager();

        if (_dashCdTimer > 0)
            _dashCdTimer -= Time.deltaTime;

        if (_limitDash > 3)
            _limitDash = 3;
        else if (_limitDash < 3)
        {
            if (_dashGainTimer > 0)
            {
                _dashGainTimer -= Time.deltaTime;
            }  
            else if(_dashGainTimer <= 0)
            {
                _limitDash++;
                _dashGainTimer = _dashGain;
            }
        }

        if(_limitDash == 0)
        {
            _hudDashFleche3.SetActive(false);
            _hudDashFleche2.SetActive(false);
            _hudDashFleche1.SetActive(false);

            _hudDashJauge3.SetActive(false);
            _hudDashJauge2.SetActive(false);
            _hudDashJauge1.SetActive(false);
        }
        if (_limitDash == 1)
        {
            _hudDashFleche3.SetActive(false);
            _hudDashFleche2.SetActive(false);
            _hudDashFleche1.SetActive(true);

            _hudDashJauge3.SetActive(false);
            _hudDashJauge2.SetActive(false);
            _hudDashJauge1.SetActive(true);

        }
        if (_limitDash == 2)
        {
            _hudDashFleche3.SetActive(false);
            _hudDashFleche2.SetActive(true);
            _hudDashFleche1.SetActive(true);

            _hudDashJauge3.SetActive(false);
            _hudDashJauge2.SetActive(true);
            _hudDashJauge1.SetActive(true);

        }
        if (_limitDash == 3)
        {
            _hudDashFleche3.SetActive(true);
            _hudDashFleche2.SetActive(true);
            _hudDashFleche1.SetActive(true);

            _hudDashJauge3.SetActive(true);
            _hudDashJauge2.SetActive(true);
            _hudDashJauge1.SetActive(true);
        }





    }

    public void ButtonAxel()
    {
        AxelIsHere = !AxelIsHere;
        
    }

    private void InputManager()
    {
        /*if (AxelIsHere)
        {
            if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0 || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) ||
            Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 || (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)) {
            if (S_InputManager._mouvementInput.y != 0 && S_InputManager._mouvementInput.y > 0 || (S_InputManager._mouvementInput.y != 0 && S_InputManager._mouvementInput.y < 0) ||
            S_InputManager._mouvementInput.x != 0 && S_InputManager._mouvementInput.x > 0 || (S_InputManager._mouvementInput.x != 0 && S_InputManager._mouvementInput.x < 0)) 
            {
                float timeSinceLastPress = Time.time - lastPressTime;

                if (timeSinceLastPress <= DOUBLE_CLICK_TIME)
                {
                    Debug.Log("?");
                    DashFunction();
                }
            lastPressTime = Time.time;
            }
        }*/ 
        

        //if (Input.GetButtonDown("Dash"))
        if (S_InputManager._dashInput)
        {
            DashFunction();
        }
    }

    private void DashFunction()
    {
        if (_limitDash <= 0) return;
        if (_dashCdTimer > 0) return;
        if (_pm._isFreezing) return;

        else _dashCdTimer = _dashCd;

        PlayerSoundScript.DashSound();
        _arms_AC.SetBool("dashing", true);
        _arms_AC.SetBool("stoppedMoving", false);
        _limitDash--;
        _pm._isDashing = true;
        //_pm._readyToJump = false;
        Transform forwardT;

        if (_isUsingCameraForward)
            forwardT = playerCam;
        else
            forwardT = orientation;

        if (_pm._isSlopePositive)
        {
               _dashUpwardForce = 140f;
               _dashUpwardForce = _dashUpwardForce - (Mathf.Abs(_pm._actualSlopeAngle * 2));
        }
        else
            _dashUpwardForce = 0f;

        Vector3 direction = GetDirection(forwardT);
        Vector3 forceToApply = direction * _dashForce - orientation.up * _dashUpwardForce;

        if (_isDisablingGravity)
               _rb.useGravity = false;


        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);

        Invoke(nameof(ResetDash), _dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        /*if (_pm.OnSlope())
        {
            StartCoroutine(untilDashIsFalse());
        }*/
        if (_isResettingVel)
        {
            _rb.velocity = Vector3.zero;
        }
            _rb.AddForce(delayedForceToApply, ForceMode.Impulse);

    }

    private void ResetDash()
    {
        _pm._isDashing = false;
        _pm._ReachUpgradeBool = false;
        _dashUpwardForce = 0f;

        if (_isDisablingGravity)
        {
            _rb.useGravity = true;
        }
    }

    public Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = S_InputManager._mouvementInput.x;
        float verticalInput = S_InputManager._mouvementInput.y;
        /*float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");*/

        Vector3 direction = new Vector3();

        if (_isAllowingAllDirections)
        {
            direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        }
        else
        {
            direction = forwardT.forward;
        }
        if (verticalInput == 0 && horizontalInput == 0)
        {
            direction = forwardT.forward;
        }
        return direction.normalized;
    }
    
    /*IEnumerator untilDashIsFalse()
    {
        yield return new WaitUntil(() => !_pm._isDashing);
    }*/

}
