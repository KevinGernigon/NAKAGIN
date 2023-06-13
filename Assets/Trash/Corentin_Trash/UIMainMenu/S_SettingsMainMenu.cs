using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_SettingsMainMenu : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    private S_InputManager _InputManager;
    private GameObject UI;
    private GameObject PauseHUD;
    private GameObject SettingsHUD;
    private GameObject VideoSettingsHUD;
    private GameObject StartHUD;

    private bool _inSetting;
    private bool _inMenu;
    private bool _inCredit;

    [SerializeField] private GameObject FirstSelectButtonMenu;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject FirstSelectButtonCredit;
    [SerializeField] private GameObject CreditHUD;


    private GameObject FirstSelectButtonSettings;
    private GameObject LastSelectButtonSetting;
    private GameObject LastSelectButtonCredit;

    [SerializeField] private GameObject LastSelectButton;

    private bool ControllerActive = true;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _InputManager = _referenceInterface._InputManager;

        UI = _referenceInterface._UICanvas;
        PauseHUD = _referenceInterface._UIPauseHUD;
        SettingsHUD = _referenceInterface._UISettingsInterface;
        VideoSettingsHUD = _referenceInterface._UIVideoSettings;
        StartHUD = _referenceInterface._UIStartHUD;
        FirstSelectButtonSettings = _referenceInterface._UIVideoButton;
        _inMenu = _referenceInterface._PauseMenuV2._inMenu;
    }


    private void Start()
    {
        InMenu();
    }

    private void Update()
    {
        SwitchControlleMenuSettings();

        if(_InputManager._playerInputAction.UI.Cancel.triggered && (_inSetting || _inCredit))
        {
            InMenu();
        }
        if (_InputManager._playerInputAction.UI.Pause.triggered && (_inSetting || _inCredit))
        {
            InMenu();
        }

    }

    private void SwitchControlleMenuSettings()
    {
        if (_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _inMenu)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButton = FirstSelectButtonMenu;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButton);
        }

        if (_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _inSetting)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButtonSetting = FirstSelectButtonSettings;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButtonSetting);
        }

        if (_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _inCredit)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButton = FirstSelectButtonCredit;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButton);
        }

        if (_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _inMenu)
        {
            LastSelectButton = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _inSetting)
        {
            LastSelectButtonSetting = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _inCredit)
        {
            LastSelectButton = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }


    }

    public void Menufalse()
    {
        _inMenu = false;
    }


    public void InMenuSettings()
    {
        _mainMenu.SetActive(false);

        _inSetting = true; 
        _inCredit = false;
        _inMenu = false;

        UI.SetActive(true);
        StartHUD.SetActive(false);
        PauseHUD.SetActive(true);
        SettingsHUD.SetActive(true);
        VideoSettingsHUD.SetActive(true);

        if (_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonSettings);
            LastSelectButtonSetting = FirstSelectButtonSettings;

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButtonSetting = FirstSelectButtonSettings;
        }
    }


    public void InMenuCredit()
    {
        //_mainMenu.SetActive(false);

        _inSetting = false;
        _inCredit = true;
        _inMenu = false;

        if (_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonCredit);
            LastSelectButton = FirstSelectButtonCredit;

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButton = FirstSelectButtonCredit;
        }
    }



    public void InMenu()
    {
        _mainMenu.SetActive(true);

        CreditHUD.SetActive(false);

        _inSetting = false;
        _inCredit = false;
        _inMenu = true;

        VideoSettingsHUD.SetActive(false);
        SettingsHUD.SetActive(false);
        StartHUD.SetActive(true);
        PauseHUD.SetActive(false);
        UI.SetActive(false);

        if (_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonMenu);
            LastSelectButton = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButton = FirstSelectButtonMenu;
        }
    }


}
