using System.Collections;
using UnityEngine;

public class S_GrappinV2 : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private S_PlayerMovement _pm;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _grappingTransform;
    [SerializeField] private LineRenderer lr;

    [Header("Audio")]
    [SerializeField] private AudioSource SoundManager;
    [SerializeField] private AudioClip ImpactHookSoundClip;
    [SerializeField] private AudioClip HookSoundClip;
    [SerializeField] private AudioClip ResetHookSoundClip;

    [Header("Layer")]
    [SerializeField] private LayerMask _whatIsTarget;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private LayerMask Default;

    [Header("Grappling Hook Ref")]
    [SerializeField] private float _maxGrappleDistance;
    [SerializeField] private float _grappleDelayTime;
    [SerializeField] private float _overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float _grapplingCd;
    private float _grapplingCdTimer;

    [Header("Boolean")]
    public bool _isGrappling;
    public bool isIncreaseFOV;
    public bool _isDecreaseRbDrag;

    public System.Action updateAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        
    }

    private void Update()
    {
        if(Physics.Raycast(_camera.position, _camera.forward, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsTarget")))
        {
            Debug.DrawRay(_camera.position, _camera.forward * 50, Color.red);
        }

        if (Input.GetKeyDown(KeyCode.A) && !_isGrappling)
        {
                StartGrapple();
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
        RaycastHit blockHit;

        if (Physics.Raycast(_camera.position, _camera.forward, out blockHit, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsGround"))) return; 
        if (Physics.Raycast(_camera.position, _camera.forward, out blockHit, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsPlayer"))) return; 
        if (Physics.Raycast(_camera.position, _camera.forward, out blockHit, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsWall"))) return; 

        SoundManager.PlayOneShot(HookSoundClip);

        _isGrappling = true;

        _pm._isFreezing = true;

        RaycastHit hit;
        if(Physics.Raycast(_camera.position, _camera.forward, out hit, _maxGrappleDistance, 1 << LayerMask.NameToLayer("WhatIsTarget")))
        //if(Physics.Raycast(_camera.position, _camera.forward, out hit, _maxGrappleDistance, _whatIsTarget))
        {
            _isDecreaseRbDrag = true;
            _pm.Jump();
            //grapplePoint = hit.point;
            grapplePoint = hit.transform.position;
            SoundManager.PlayOneShot(ImpactHookSoundClip);
            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
            var finalPosition = grapplePoint;
            return;
        }
        else
        {
            grapplePoint = _camera.position + _camera.forward * _maxGrappleDistance;
            SoundManager.PlayOneShot(ResetHookSoundClip);
            Invoke(nameof(StopGrapple), _grappleDelayTime);
            lr.enabled = true;
            lr.SetPosition(1, grapplePoint);
            var finalPosition = grapplePoint;
            return;
        }
        

        /*SetGraplin(finalPosion);
        updateAction = () => SetGraplin(finalPosion);*/
    }


    /*private void SetGraplin(Vector3 finalPos)
    {
        float i = 0f;
        lr.positionCount = 1000;
        var tempTime = Time.time;
        for (int y = 0; y < 1000; y++)
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
        _pm._isFreezing = true;
        isIncreaseFOV = true;
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z); //point de départ du personnage
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + _overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = _overshootYAxis;

        _pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        _pm._readyToJump = true;

        _pm._isFreezing = false;

        _isGrappling = false;

        _grapplingCdTimer = _grapplingCd;

        lr.enabled = false;

        isIncreaseFOV = false;

        _isDecreaseRbDrag = false;

    }
}

    
