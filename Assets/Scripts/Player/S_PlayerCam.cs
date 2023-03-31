using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class S_PlayerCam : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Sensitivity")]

    public float _sensX;
    public float _sensY;


    [SerializeField] private Slider _sensiMouseSlider;
    [SerializeField] private Slider _sensiControllerSlider;

    [Header("References")]
    public S_PlayerMovement pm;
    public S_WallRunning wr;
    public S_Climbing ClimbingScript;
    public S_GrappinV2 GrapplingHookScript;
    public Transform _orientation;
    public Transform player;
    //public Transform respawnPoint;

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

    [Header("Head Bobbing")]
    [SerializeField] private float _headBobbing;
    [SerializeField] private float _speedBobbing;


    public bool _isActive;
    public bool boolChangement;
    private bool _isClimbingBool;
    [SerializeField]
    private GameObject _eventSystem;

    public int _RandomCount;
    private bool _isRandomNumber;
    public float tilt { get; private set; }

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
       
        _isActive = true;
        boolChangement = false;
        _isClimbingBool = false;
        cam.fieldOfView = _fov;
        tilt = 0;
    }
    private void FixedUpdate()
    {
        /*if (pm._isMoving && !_isClimbingBool)
        {
            float temps = Mathf.PingPong(Time.time * _speedBobbing, 1);
            tilt = Mathf.Lerp(-_headBobbing, _headBobbing, temps);
        }*/


    }
    // Update is called once per frame
    private void Update()
    {
        CameraFOVDash();
        CameraTiltWallRunFPS();
        CameraTiltSlide();
        CameraTiltClimb();
        CameraFOVGrapplingHook();
        ClimbCameraAdjusted();
        WallRunCameraAdjusted();

        if (!boolChangement)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _fov, _resetFovTime * Time.fixedDeltaTime);
            tilt = Mathf.Lerp(tilt, 0, _camTiltTime * Time.fixedDeltaTime);
        }

        if (_isActive)
        {
            if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
            {
                _mouseX = S_InputManager._playerInputAction.Player.CameraMouvement.ReadValue<Vector2>().x * Time.fixedDeltaTime * _sensiMouseSlider.value;
                _mouseY = S_InputManager._playerInputAction.Player.CameraMouvement.ReadValue<Vector2>().y * Time.fixedDeltaTime * _sensiMouseSlider.value;

                if (S_InputManager._invertAxeXMouse)
                    _yRotation += -_mouseX;
                else
                    _yRotation += _mouseX;

                if (S_InputManager._invertAxeYMouse)
                    _xRotation -= -_mouseY;
                else
                    _xRotation -= _mouseY;


            }


            if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
            {
                _mouseX = S_InputManager._playerInputAction.Player.CameraMouvement.ReadValue<Vector2>().x * Time.fixedDeltaTime * _sensiControllerSlider.value;
                _mouseY = S_InputManager._playerInputAction.Player.CameraMouvement.ReadValue<Vector2>().y * Time.fixedDeltaTime * _sensiControllerSlider.value;

                if (S_InputManager._invertAxeXGamepad)
                    _yRotation += -_mouseX;
                else
                    _yRotation += _mouseX;

                if (S_InputManager._invertAxeYGamepad)
                    _xRotation -= -_mouseY;
                else
                    _xRotation -= _mouseY;
            }

            //_yRotation += _mouseX;
            // _xRotation -= _mouseY;
            if (pm._isClimbing)
            {
                _xRotation = Mathf.Clamp(_xRotation, -90f, 0f);
            }
            else
                _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, tilt);
            _orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
        }

        if (!pm._isWallRunning && !pm._isSliding && !GrapplingHookScript.isIncreaseFOV && !pm._isDashing)
        {
            boolChangement = false;
            _isRandomNumber = false;
        }
    }

    public void RandomFunction()
    {
        if (!_isRandomNumber)
        {
            _isRandomNumber = true;
            _RandomCount = Random.Range(1, 3);
        }
    }
    private void ClimbCameraAdjusted() 
    {
        if (pm._isClimbing)
        {
            _mouseX = 0;
            //_mouseX = Mathf.Clamp(_mouseX, -0.2f, 0.2f);
            _yRotation += _mouseX;
        }
    }

    private void WallRunCameraAdjusted()
    {
        if (pm._isWallRunning || pm._isSliding)
        {
            _mouseX = Mathf.Clamp(_mouseX, -0.5f, 0.5f);
            _yRotation += _mouseX;
        }
    }


    private void CameraTiltWallRunFPS()
    {
        if (pm._isWallRunning)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _wallRunFov, _wallRunFovTime * Time.fixedDeltaTime);
            if (wr._isWallLeft)
            {
                tilt = Mathf.Lerp(tilt, -_camTiltWR, _camTiltTime * Time.fixedDeltaTime);
            }

            else if (wr._isWallRight)
            {
                tilt = Mathf.Lerp(tilt, _camTiltWR, _camTiltTime * Time.fixedDeltaTime);
            }
        }
    }
    private void CameraTiltSlide()
    {
        RandomFunction();
        if (pm._isSliding)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _wallSlideFov, _wallSlideFovTime * Time.fixedDeltaTime);
            if (_RandomCount == 1)
                tilt = Mathf.Lerp(tilt, _camTiltSlide, _camTiltTime * Time.fixedDeltaTime);
            else if (_RandomCount == 2)
                tilt = Mathf.Lerp(tilt, -_camTiltSlide, _camTiltTime * Time.fixedDeltaTime);
        }
    }
    private void CameraTiltClimb()
    {
        if (ClimbingScript._isAchievedClimb)
        {
            _isClimbingBool = true;
            boolChangement = true;
            tilt = Mathf.Lerp(tilt, _camTiltClimbAchieved, _camTiltTime * Time.fixedDeltaTime);
            StartCoroutine(EndClimbingAnimation());
        }     
    }
    private void CameraFOVGrapplingHook()
    {
        if (GrapplingHookScript.isIncreaseFOV)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _grapplingHookFov, _grapplingHookFovTime * Time.fixedDeltaTime);
        }
    }
    private void CameraFOVDash()
    {
        if (pm._isDashing)
        {
            boolChangement = true;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _dashFov, _dashFovTime * Time.fixedDeltaTime);
        }
    }

    public void CameraReset(float x, float y)
    {
        StartCoroutine(resetcam(x,y));      
    }

    IEnumerator resetcam(float x, float y)
    {
        _isActive = false;
        _xRotation = x;
        _yRotation = y;
        Camera.main.transform.rotation = Quaternion.identity;
        yield return new WaitForSeconds(0.01f);
        _isActive = true;
    }

    IEnumerator EndClimbingAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        _isClimbingBool = false;
    }
}