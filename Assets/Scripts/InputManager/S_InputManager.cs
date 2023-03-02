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

        ///Exemple :
        //groundMouvement.[action].perfomed += cts => do something
        //_playerMovement.Jump.started += _ => S_movement.OnJumpPressed();


        //////// PLAYER
        /// Mouvement
        _playerInputAction.Player.Movement.performed += ctxMouvement => _mouvementInput = ctxMouvement.ReadValue<Vector2>();
        _playerInputAction.Player.Movement.canceled += ctxMouvement => _mouvementInput = ctxMouvement.ReadValue<Vector2>();
        

        /// Jump     
        /*_playerInputAction.Player.Jump.started += ctxJump => _jumpInput = ctxJump.ReadValueAsButton();
        _playerInputAction.Player.Jump.performed += _ => StartCoroutine(ResetJump());
        _playerInputAction.Player.Jump.canceled += ctxJump => _jumpInput = ctxJump.ReadValueAsButton();   //_playerInputAction.Player.Jump.started += ctxJump => _jumpInput = ctxJump.ReadValue<float>();*/


        /// CameraMouvement 
        _playerInputAction.Player.CameraMouvement.started += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();
        _playerInputAction.Player.CameraMouvement.performed += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();
        _playerInputAction.Player.CameraMouvement.canceled += ctxCamera => _cameraInput = ctxCamera.ReadValue<Vector2>();

        /// Dash
        _playerInputAction.Player.Dash.started += _ => _dashInput = true;
        _playerInputAction.Player.Dash.performed += _ => StartCoroutine(ResetDash());
        _playerInputAction.Player.Dash.canceled += _ => _dashInput = false;


        /// Slide !!!!!!!!!!!!!!!!!! BUG avec S_PlayerMovement

        //Gerer dans S_Sliding avec un triggered et la recuperation de la valeur durant la frame active

        /*_playerInputAction.Player.Slide.started += _ => _slideInput = true;
        //_playerInputAction.Player.Slide.performed += _ => _slideInput = true;
        _playerInputAction.Player.Slide.canceled += _ => _slideInput = false;*/

        /// MoveModuleLeft
        //Gerer dans S_RotationPlatformes avec un triggered
        /// MoveModuleRight 
        //Gerer dans S_RotationPlatformes avec un triggered

        /// Grappin 
        //Gerer dans S_GrappinV2 avec un triggered

        /// Jetpack 
        //Gerer dans S_jetpack avec un triggered + les fonction ActiveJetpackInput et DesactiveJetpackInput

        /// Pause
        //Gerer dans fonction ActivePause et S_PauseMenuV2

        /// Interaction
        //Gerer dans fonction S_GenerateurEnergetique avec un triggered



        /////// UI

        /// Pause
        //Gerer dans fonction DesactivePause et S_PauseMenuV2


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
            Debug.Log("Mode UI");
    }

    public void DesactivePause()
    {       _playerInputAction.UI.Disable();
            _playerInputAction.Player.Enable();
            _playerEnable = true;
            Debug.Log("Mode Player");
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
