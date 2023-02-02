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
    [Header("Layer")]
    [SerializeField] private LayerMask _whatIsTarget;

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

    public System.Action updateAction;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pm = GetComponent<S_PlayerMovement>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            StartGrapple();

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

        _isGrappling = true;

        _pm._isFreezing = true;

        RaycastHit hit;
        if(Physics.Raycast(_camera.position, _camera.forward, out hit, _maxGrappleDistance, _whatIsTarget))
        {
            grapplePoint = hit.point;
            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        else
        {
            grapplePoint = _camera.position + _camera.forward * _maxGrappleDistance;
            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
        var finalPosion = grapplePoint;
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
        _pm._isFreezing = false;

        _isGrappling = false;

        _grapplingCdTimer = _grapplingCd;

        lr.enabled = false;

        isIncreaseFOV = false;

    }
}

    
