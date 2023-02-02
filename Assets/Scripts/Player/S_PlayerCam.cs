using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_PlayerCam : MonoBehaviour
{
    public float _sensX;
    public float _sensY;
    [SerializeField]
    private Slider _sensiSlider;

    [Header("References")]
    public S_PlayerMovement pm;
    public S_WallRunning wr;
    public S_Climbing ClimbingScript;
    public S_GrappinV2 GrapplingHookScript;
    public Transform _orientation;
    public Transform player;
    public Transform respawnPoint;

    public float _xRotation;
    public float _yRotation;
    public float _mouseX;
    public float _mouseY;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private float _fov;
    [SerializeField] private float _wallRunFov;
    [SerializeField] private float _wallRunFovTime;
    [SerializeField] [Range(0, 30)] private float _camTiltWR;
    [SerializeField] [Range(0, 30)] private float _camTiltSlide;
    [SerializeField] [Range(0, 30)] private float _camTiltClimbAchieved;
    [SerializeField] private float _camTiltTime;
    [SerializeField] private float _wallSlideFovTime;
    [SerializeField] private float _wallSlideFov;
    [SerializeField] private float _grapplingHookFov;
    [SerializeField] private float _grapplingHookFovTime;
    [SerializeField] private float _dashFov;
    [SerializeField] private float _dashFovTime;
    [SerializeField] private float _resetFovTime;
    public bool _isAxisXInverted;
    public bool _isAxisYInverted;
    public bool _isActive;
    public bool boolChangement;
    [SerializeField]
    private GameObject _eventSystem;

    public float tilt { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _isAxisXInverted = true;
        _isAxisYInverted = true;
        _isActive = true;
        boolChangement = false;

        cam.fieldOfView = _fov;
        tilt = 0;
    }
    private void FixedUpdate()
    {
        if (!boolChangement)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _fov, _resetFovTime * Time.deltaTime);
            tilt = Mathf.Lerp(tilt, 0, _camTiltTime * Time.deltaTime);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        CameraFOVDash();
        CameraTiltWallRunFPS();
        CameraTiltSlide();
        CameraTiltClimb();
        CameraFOVGrapplingHook();
        if (_isActive && !_eventSystem.GetComponent<S_PauseMenuV2>()._isPaused)
        {
            // Mouse Input //
            if (_isAxisXInverted)
                _mouseX = Input.GetAxisRaw("Mouse X") * _sensX * _sensiSlider.value;
            else
                _mouseX = Input.GetAxisRaw("Mouse X") * -_sensX * _sensiSlider.value;

            if (_isAxisYInverted)
                _mouseY = Input.GetAxisRaw("Mouse Y") * _sensY * _sensiSlider.value;
            else
                _mouseY = Input.GetAxisRaw("Mouse Y") * -_sensY * _sensiSlider.value;
            ////////////////
            ///
            
                _yRotation += _mouseX;
                _xRotation -= _mouseY;
                _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

                transform.rotation = Quaternion.Euler(_xRotation, _yRotation, tilt);
                _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }

        if(!pm._isWallRunning && !pm._isSliding && !ClimbingScript._isAchievedClimb && !GrapplingHookScript.isIncreaseFOV && !pm._isDashing)
        {
            boolChangement = false;
        }
    }

    private void CameraTiltWallRunFPS()
    {
        if (pm._isWallRunning)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _wallRunFov, _wallRunFovTime * Time.deltaTime);
            if (wr._isWallLeft)
            {
                tilt = Mathf.Lerp(tilt, -_camTiltWR, _camTiltTime * Time.deltaTime);
            }

            else if (wr._isWallRight)
            {
                tilt = Mathf.Lerp(tilt, _camTiltWR, _camTiltTime * Time.deltaTime);
            }
        }
    }
    private void CameraTiltSlide()
    {
        if (pm._isSliding)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _wallSlideFov, _wallSlideFovTime * Time.deltaTime);
            tilt = Mathf.Lerp(tilt, _camTiltSlide, _camTiltTime * Time.deltaTime);
        }        
    }
    private void CameraTiltClimb()
    {
        if (ClimbingScript._isAchievedClimb)
        {
            boolChangement = true;
            tilt = Mathf.Lerp(tilt, _camTiltClimbAchieved, _camTiltTime * Time.deltaTime);
        }     
    }
    private void CameraFOVGrapplingHook()
    {
        if (GrapplingHookScript.isIncreaseFOV)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _grapplingHookFov, _grapplingHookFovTime * Time.deltaTime);
        }
    }
    private void CameraFOVDash()
    {
        if (pm._isDashing)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _dashFov, _dashFovTime * Time.deltaTime);
        }
    }

    public void InvertXAxis()
    {
        if (!_isAxisXInverted)
            _isAxisXInverted = true;
        else
            _isAxisXInverted = false;
    }

    public void InvertYAxis()
    {
        if (!_isAxisYInverted)
            _isAxisYInverted = true;
        else
            _isAxisYInverted = false;
    }

    public void CameraReset()
    {
        StartCoroutine(resetcam());      
    }

    IEnumerator resetcam()
    {
        _isActive = false;
        _xRotation = respawnPoint.rotation.eulerAngles.x;
        _yRotation = respawnPoint.rotation.eulerAngles.y;
        Camera.main.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.01f);
        _isActive = true;
    }
}