using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S_PauseMenuV2 : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Audio")]
    [SerializeField] private S_PlayerSound PlayerSound;

    [Header("Other")]
    [SerializeField] private S_GestionnaireScene S_GestionnaireScene;
    [SerializeField] private GameObject _pauseMenuHUD;
    [SerializeField] private GameObject _startGameHUD;

    [SerializeField] private GameObject _pauseInterface;
    [SerializeField] private GameObject _settingsInterface;
    [SerializeField] private GameObject _leaderboardInterface;
    [SerializeField] private GameObject _helpsettings;

    [SerializeField] private S_BatteryManager _BatteryManager;
    [SerializeField] private S_Jetpack _Jetpack;
    
    [Header("HUD")]

    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private Animator _infoTuto;

    [SerializeField] private GameObject FirstSelectButtonPause;
    [SerializeField] private GameObject FirstSelectButtonSetting;
    [SerializeField] private GameObject FirstSelectButtonLeaderboard;
    private GameObject LastSelectButton;
    private GameObject LastSelectButtonSetting;
    private GameObject LastSelectButtonLeaderboard;

    public bool _isPaused = false;
    [SerializeField] private bool _isSetting = false;
    [SerializeField] private bool _isLeaderboard = false;
    public bool _ischoose;
    public bool _IsRestart = false;
    public bool _inMenu;
    private int _sceneId;

    [SerializeField] private bool ControllerActive = true;

    [Header("Reset")]
    [SerializeField] private GameObject _player;


    [Header("QwertMode")]
    [SerializeField] private GameObject _qwertyKeyboardIMG;
    [SerializeField] private GameObject _azertyKeyboardIMG;
    public bool _qwertyMode;



    private void Awake()
    {
        
    }

    void Start()
    {
        //_ischoose = false ;
        _pauseMenuHUD.SetActive(false);
        ResetPauseHUD();
    }


    void Update()
    {
        if (S_GestionnaireScene.InMenu)
        {
            _ischoose = false;
        }

        if (S_InputManager._playerInputAction.Player.Pause.triggered)
        {
            if (S_InputManager._playerEnable)
            {
                PauseGame();
                return;
            }
        }


        if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            _helpsettings.SetActive(true);

            if (S_InputManager._playerInputAction.UI.Cancel.triggered)
            {
                if (!S_InputManager._playerEnable)
                {
                    if(_isSetting)
                    {
                        _isSetting = false;
                        _settingsInterface.SetActive(false);
                        _pauseInterface.SetActive(true);
                        PauseGame();
                    }
                    else if (_isLeaderboard)
                    {
                        _isLeaderboard = false;
                        _leaderboardInterface.SetActive(false);
                        _pauseInterface.SetActive(true);
                        PauseGame();
                    }
                    else
                    {
                        _isSetting = false;
                        _isLeaderboard = false;
                        ResumeGame();    
                    }
                }
            }
            if (S_InputManager._playerInputAction.UI.Pause.triggered)
            {
                if (!S_InputManager._playerEnable)
                {
                    {
                        _isSetting = false;
                        _isLeaderboard = false;
                        ResumeGame();
                    }
                }
            }
        }


        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
        {
            _helpsettings.SetActive(false);

            if (S_InputManager._playerInputAction.UI.Cancel.triggered)
            {
                if (!S_InputManager._playerEnable)
                {
                    if (_isSetting)
                    {
                        _isSetting = false;
                        _settingsInterface.SetActive(false);
                        _pauseInterface.SetActive(true);
                        PauseGame();
                    }
                    else if (_isLeaderboard)
                    {
                        _isLeaderboard = false;
                        _leaderboardInterface.SetActive(false);
                        _pauseInterface.SetActive(true);
                        PauseGame();
                    }
                    else
                    {
                        _isSetting = false;
                        _isLeaderboard = false;
                        ResumeGame();
                    }
                }
            }
        }


        if (S_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _isPaused && !_isSetting && !_isLeaderboard)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButton = FirstSelectButtonPause;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButton);
        }

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _isPaused && _isSetting)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButtonSetting = FirstSelectButtonSetting;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButtonSetting);
        }

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _isPaused && _isLeaderboard)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
                LastSelectButtonLeaderboard = FirstSelectButtonLeaderboard;
            ControllerActive = false;
            EventSystem.current.SetSelectedGameObject(LastSelectButtonLeaderboard);
        }


        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _isPaused && !_isSetting && !_isLeaderboard)
        {
            LastSelectButton = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _isPaused && _isSetting)
        {
            LastSelectButtonSetting = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _isPaused && _isLeaderboard)
        {
            LastSelectButtonLeaderboard = EventSystem.current.currentSelectedGameObject;
            ControllerActive = true;
            EventSystem.current.SetSelectedGameObject(null);
        }



    }
    public void PauseGame()
    {
        _infoTuto.Rebind();
        _infoTuto.Play("A_TooltipClose");

        S_InputManager.ActivePause();

        PlayerSound.PauseSound();

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonPause);
            LastSelectButton = EventSystem.current.currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButton = FirstSelectButtonPause;
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _pauseMenuHUD.SetActive(true);
        _startGameHUD.SetActive(false);

        Time.timeScale = 0f;
        //AudioListener = false;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        if (!_ischoose)
        {

            PlayerSound.UnPauseSound();
            StartCoroutine(waitcastchoose());

            S_InputManager.DesactivePause();

            Cursor.lockState = CursorLockMode.Confined;
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _pauseMenuHUD.SetActive(false);
            _startGameHUD.SetActive(true);

            ResetPauseHUD();


            Time.timeScale = 1f;
            //AudioListener = true;
            _isPaused = false;


        }
    }

    public void RestartLevel()
    {
        if (!_ischoose)
        {
           

            ResumeGame();
            Restart();
             
            StartCoroutine(waitcastchoose());

            _IsRestart = false;

            Scene _scene = SceneManager.GetActiveScene();

            SceneManager.LoadScene("Manager_Scene");

            _sceneId = _scene.buildIndex;

            S_GestionnaireScene.LoadNewScene(_sceneId);


        }
    }
   


    public void Setting()
    {
        _isSetting = true;

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonSetting);
            LastSelectButtonSetting = FirstSelectButtonSetting;
           
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButtonSetting = FirstSelectButtonSetting;
        }

    }

    public void CloseSettings()
    {
        if(!_inMenu)
        {
            _isSetting = false;
            _settingsInterface.SetActive(false);
            _pauseInterface.SetActive(true);
            PauseGame();
        }
        else
        {
            _isSetting = false;
            _settingsInterface.SetActive(false);
            _pauseMenuHUD.SetActive(false);
        }

    }



    public void Leaderboard()
    {
        _isLeaderboard = true;

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(FirstSelectButtonLeaderboard);
            LastSelectButtonLeaderboard = FirstSelectButtonLeaderboard;

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            LastSelectButtonLeaderboard = FirstSelectButtonLeaderboard;
        }

    }
    public void CloseLeaderboard()
    {
        _isLeaderboard = false;
        _leaderboardInterface.SetActive(false);
        _pauseInterface.SetActive(true);
        PauseGame();
    }





    public void MainMenu()
    {
        if (!_ischoose)
        {
            ResumeGame();
            StartCoroutine(waitcastchoose());
            Time.timeScale = 1f;
            //SceneManager.LoadScene("MainMenu_Scene");
        }
    }

    public void QuitGame()
    {
        if (!_ischoose)
        {
            StartCoroutine(waitcastchoose());
            Application.Quit();
        }
    }

    IEnumerator waitcastchoose()
    {
        _ischoose = true;
        yield return new WaitForSeconds(0.01f);
        _ischoose = false;
    }

    public void ChangePlayerPos(Transform newPos)
    {
        ResumeGame();
        _player.transform.position = newPos.position;
    }

    public void ResetPauseHUD()
    {
        _pauseInterface.SetActive(true);
        _settingsInterface.SetActive(false);
        _leaderboardInterface.SetActive(false);
    }

    public void Restart()
    { 
        _IsRestart = true ; 
        _BatteryManager._nbrBattery = 0;
        _settingsInterface.SetActive(false);

        _Jetpack.BooleanTriggerBoxExit();

    }

    public void KeyModeSwitch()
    {
        _qwertyMode = !_qwertyMode;

        if(_qwertyMode)
        {
            _qwertyKeyboardIMG.SetActive(true); ;
            _azertyKeyboardIMG.SetActive(false);
        }
        else
        {
            _qwertyKeyboardIMG.SetActive(false);
            _azertyKeyboardIMG.SetActive(true);
        }
    }
}