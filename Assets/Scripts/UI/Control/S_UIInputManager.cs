using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_UIInputManager : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("HUD")]

    [SerializeField] private GameObject _HUD_InteractionGenerateurEnableKeyboard;
    [SerializeField] private GameObject _HUD_InteractionGenerateurEnableController;

    [SerializeField] private GameObject _HUDBackgroundTouchKeyboard;
    [SerializeField] private GameObject _HUDBackgroundTouchController;

    [SerializeField] private GameObject _HUDButtonJetpackKeyboard;
    [SerializeField] private GameObject _HUDButtonJetpackController;

    [SerializeField] private GameObject _HUDButtonDashKeyboard;
    [SerializeField] private GameObject _HUDButtonDashController;

    [SerializeField] private GameObject _HUDButtonGrappinKeyboard;
    [SerializeField] private GameObject _HUDButtonGrappinController;

    [SerializeField] private GameObject _HUDButtontournPlatformeGaucheKeyboard;
    [SerializeField] private GameObject _HUDButtontournPlatformeGaucheContoller;

    [SerializeField] private GameObject _HUDButtontournPlatformeDroiteKeyboard;
    [SerializeField] private GameObject _HUDButtontournPlatformeDroiteController;


    private void Start()
    {
        
    }

    
    private void Update()
    {
        //Switch UI keyboard / Manette
        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
        {

            _HUDBackgroundTouchController.SetActive(false);
            _HUDBackgroundTouchKeyboard.SetActive(true);

            _HUD_InteractionGenerateurEnableController.SetActive(false);
            _HUD_InteractionGenerateurEnableKeyboard.SetActive(true);


            _HUDButtonJetpackController.SetActive(false);
            _HUDButtonDashController.SetActive(false);
            _HUDButtonGrappinController.SetActive(false);
            _HUDButtontournPlatformeGaucheContoller.SetActive(false);
            _HUDButtontournPlatformeDroiteController.SetActive(false);

            _HUDButtonJetpackKeyboard.SetActive(true);
            _HUDButtonDashKeyboard.SetActive(true);
            _HUDButtonGrappinKeyboard.SetActive(true);
            _HUDButtontournPlatformeGaucheKeyboard.SetActive(true);
            _HUDButtontournPlatformeDroiteKeyboard.SetActive(true);


        }

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
        {

            _HUDBackgroundTouchKeyboard.SetActive(false);
            _HUDBackgroundTouchController.SetActive(true);


            _HUDButtonJetpackKeyboard.SetActive(false);
            _HUDButtonDashKeyboard.SetActive(false);
            _HUDButtonGrappinKeyboard.SetActive(false);
            _HUDButtontournPlatformeGaucheKeyboard.SetActive(false);
            _HUDButtontournPlatformeDroiteKeyboard.SetActive(false);

            _HUDButtonJetpackController.SetActive(true);
            _HUDButtonDashController.SetActive(true);
            _HUDButtonGrappinController.SetActive(true);
            _HUDButtontournPlatformeGaucheContoller.SetActive(true);
            _HUDButtontournPlatformeDroiteController.SetActive(true);


            _HUD_InteractionGenerateurEnableKeyboard.SetActive(false);
            _HUD_InteractionGenerateurEnableController.SetActive(true);

        }
    }
}
