using System.Collections;
using UnityEngine;

public class S_GrappinV2 : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private S_PlayerMovement _pm;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _grappingTransform;
    [SerializeField] private LineRenderer lr;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("HUD")]
    [SerializeField] private GameObject _HUDCrossHairLock;
    [SerializeField] private Animator _aniamCrossHair;
    private bool _isAnimPlaying = true;

    [Header("Layer")]
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private LayerMask Everything;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("Grappling Hook Ref")]
    [SerializeField] private float _maxGrappleDistance;
    [SerializeField] private float _grappleDelayTime;
    [SerializeField] private float _overshootYAxis;

    private Vector3 grapplePoint;
    private Vector3 previousGrapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float _grapplingCd;
    private float _grapplingCdTimer;

    [Header("Boolean")]
    public bool _isGrappling;
    public bool isIncreaseFOV;
    public bool _isDecreaseRbDrag;

    public bool _isHUD = false;
    public bool _isHookingHUD = false;
    public bool _isAimForgivenessActive;
    public System.Action updateAction;
    public int IncrementValue;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
        _isHUD = false;
    }

    private void Update()
    {
        HUDCrosshair();
        /*if (Input.GetKeyDown(KeyCode.A) && !_isGrappling)
            StartGrapple();*/
        if (S_InputManager._playerInputAction.Player.Grappin.triggered && !_isGrappling)
            StartGrapple();

        RaycastHit DebugHit;

        if (Physics.Raycast(_camera.position, _camera.forward, out DebugHit, _maxGrappleDistance, Everything))
        {
            int whatIsTarget = LayerMask.NameToLayer("WhatIsTarget");

            if (DebugHit.collider.gameObject.layer == whatIsTarget)
            {
                Debug.DrawRay(_camera.position, _camera.forward * _maxGrappleDistance, Color.blue);
                grapplePoint = DebugHit.transform.position;
                _isHUD = true;
                _isAimForgivenessActive = true;
                if (IncrementValue <= 0)
                {
                    IncrementValue++;
                    previousGrapplePoint = grapplePoint;
                }
                StopAllCoroutines();
            }
            else
            {
                Debug.DrawRay(_camera.position, _camera.forward * _maxGrappleDistance, Color.red);
                StartCoroutine(TimerGrapplingHook());

            }
        }
        else
        {
            StartCoroutine(TimerGrapplingHook());
        }



        if (_grapplingCdTimer > 0)
            _grapplingCdTimer -= Time.deltaTime;

        //updateAction?.Invoke();

    }

    private void LateUpdate()
    {
        if (_isGrappling)
            //lr.SetPosition(1000, _grappingTransform.position);
            lr.SetPosition(0, _grappingTransform.position);
    }
    private void StartGrapple()
    {
        if (_grapplingCdTimer > 0) return;

        /*RaycastHit noTarget;
        if (Physics.Raycast(_camera.position, _camera.forward, out noTarget, _maxGrappleDistance, ~_whatIsTarget))
        {
        }*/

        PlayerSoundScript.RopeSound();
        _isGrappling = true;
        _pm._isFreezing = true;
        RaycastHit hit;
        if (_isAimForgivenessActive)
        {
            Debug.Log("????????????");
            _isDecreaseRbDrag = true;
            _pm.Jump();
            grapplePoint = previousGrapplePoint;
            PlayerSoundScript.ImpactHookSound();
            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        //if(Physics.Raycast(_camera.position, _camera.forward, out hit, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsTarget")))
        else if (Physics.Raycast(_camera.position, _camera.forward, out hit, _maxGrappleDistance, Everything))
        {
            int whatIsTarget = LayerMask.NameToLayer("WhatIsTarget");
            if (hit.collider.gameObject.layer == whatIsTarget)
            {
                _isDecreaseRbDrag = true;
                _pm.Jump();
                grapplePoint = hit.transform.position;
                PlayerSoundScript.ImpactHookSound();
                Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
            }
            else
                MissGrapple();
        }
        else
            MissGrapple(); 


        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
        var finalPosition = grapplePoint;
        /*SetGraplin(finalPosition);
        updateAction = () => SetGraplin(finalPosition);*/
    }

    private void HUDCrosshair()
    {
        if (!_isHookingHUD)
        {
            if (_isHUD)
            {
                _HUDCrossHairLock.SetActive(true);
                if (_isAnimPlaying && _grapplingCdTimer <= 0)
                {
                    _aniamCrossHair.Rebind();
                    _aniamCrossHair.Play("A_CrossHairOpen");
                    _isAnimPlaying = false;
                }
            }
            else if (!_isAnimPlaying)
            {
                _aniamCrossHair.Rebind();
                _aniamCrossHair.Play("A_CrossHairClose");
                _isAnimPlaying = true;
                //_HUDCrossHairLock.SetActive(false);
            }  
       }
       else if (!_isAnimPlaying && _grapplingCdTimer >= 0)
       {
            _aniamCrossHair.Rebind();
            _aniamCrossHair.Play("A_CrossHairClose");
            _isAnimPlaying = true;
            //_HUDCrossHairLock.SetActive(false);
       }     
    }

    IEnumerator TimerGrapplingHook()
    {
        yield return new WaitForSeconds(0.3f);
        _isHUD = false;
        _isAimForgivenessActive = false;
        grapplePoint = grapplePoint;
    }

    private void MissGrapple()
    {
        grapplePoint = _camera.position + _camera.forward * _maxGrappleDistance;
        PlayerSoundScript.RewindSound();
        Invoke(nameof(StopGrapple), _grapplingCd);
    }


    /*private void SetGraplin(Vector3 finalPos)
    {

        float i = 0f;
        lr.positionCount = 100;
        var tempTime = Time.time;
        for (int y = 0; y < lr.positionCount; y++)
        {
            //var tempPosition = Vector3.Lerp(_grappingTransform.position, finalPos, i);
            var tempPosition = Vector3.Lerp(finalPos, _grappingTransform.position, i);
            tempPosition = new Vector3(tempPosition.x, tempPosition.y + Mathf.Cos(5 * Time.time * i) * 0.1f, tempPosition.z);
            lr.SetPosition(y, tempPosition);
            i = (float)y / (float)lr.positionCount;
        }

        lr.SetPosition(lr.positionCount - 1, _grappingTransform.position);
   
    }*/
    private void ExecuteGrapple()
    {
        _isHookingHUD = true;
        _pm._isFreezing = true;
        isIncreaseFOV = true;
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z); //point de d part du personnage
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + _overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = _overshootYAxis;

        _pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
        Invoke(nameof(HUDManager), _grappleDelayTime);
    }

    public void StopGrapple()
    {
        IncrementValue = 0;

        _pm._readyToJump = true;

        _pm._isFreezing = false;

        _isGrappling = false;

        _grapplingCdTimer = _grapplingCd;

        lr.enabled = false;

        isIncreaseFOV = false;

        _isDecreaseRbDrag = false;
    }

    public void HUDManager()
    {
        _isHookingHUD = false;
    }
}


