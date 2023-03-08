using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_InputManager : MonoBehaviour
{
    
    public PlayerInputAction _playerInputAction;
    public PlayerInput _playerInput;


    [Header("Infos Input")]

    public Vector2 _mouvementInput;
    public Vector2 _cameraInput;
    public bool _jumpInput;
    public bool _dashInput;


    //public bool _slideInput;

    [Header("Infos Influence Input")]
    
    public bool _playerEnable;
    public bool _jetpackActive = true;

    public bool _invertAxeXGamepad = false;
    public bool _invertAxeYGamepad = false;
    public bool _invertAxeXMouse = false;
    public bool _invertAxeYMouse = false;

    private void Awake()
    {
        //_playerInput = GetComponent<PlayerInput>();
        _playerInputAction = new PlayerInputAction();

        _playerInputAction.Player.Enable();// active l' action map "Player"
        _playerInputAction.UI.Disable();
        _playerEnable = true;


        //////// PLAYER
        /// Mouvement
        _playerInputAction.Player.Movement.performed += ctxMouvement => _mouvementInput = ctxMouvement.ReadValue<Vector2>();
        _playerInputAction.Player.Movement.canceled += ctxMouvement => _mouvementInput = ctxMouvement.ReadValue<Vector2>();
        

        /// CameraMouvement 
        _playerInputAction.Player.CameraMouvement.started += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();
        _playerInputAction.Player.CameraMouvement.performed += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();
        _playerInputAction.Player.CameraMouvement.canceled += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();

        /// Dash
        _playerInputAction.Player.Dash.started += _ => _dashInput = true;
        _playerInputAction.Player.Dash.performed += _ => StartCoroutine(ResetDash());
        _playerInputAction.Player.Dash.canceled += _ => _dashInput = false;

      
        /// Jetpack 
        //Gerer dans S_jetpack avec un triggered + les fonction ActiveJetpackInput et DesactiveJetpackInput

        /// Pause
        //Gerer dans fonction ActivePause et S_PauseMenuV2

        /// Interaction
        //Gerer dans fonction S_GenerateurEnergetique avec un triggered

    }

    private void Update()
    {
       

    }

    

    IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(0.01f);
        _jumpInput = false;
    }
    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(0.01f);
        _dashInput = false;
    }
   


    // S'active si le joueur n'est pas proche d'une console d'energie
    public void ActiveJetpackInput()
    {
        _jetpackActive = true;
    }
    public void DesactiveJetpackInput()
    {
        _jetpackActive = false;
    }

    public void ActivePause()
    {
            _playerInputAction.Player.Disable();
            _playerInputAction.UI.Enable();
            _playerEnable = false;
            //Debug.Log("Mode UI");
    }

    public void DesactivePause()
    {       _playerInputAction.UI.Disable();
            _playerInputAction.Player.Enable();
            _playerEnable = true;
            //Debug.Log("Mode Player");
    }

    private void OnEnable()
    {
        _playerInputAction.Enable();
    }

    private void OnDestroy()
    {
        _playerInputAction.Disable();
    }
}
