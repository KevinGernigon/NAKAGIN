using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class S_PauseMenuV2 : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Audio")]
    [SerializeField] private S_PlayerSound PlayerSound;

    [Header("Other")]
    [SerializeField] private GameObject _pauseMenuHUD;
    [SerializeField] private GameObject _startGameHUD;

    [SerializeField] private GameObject _pauseInterface;
    [SerializeField] private GameObject _settingsInterface;

    [SerializeField] private S_BatteryManager _BatteryManager;

    [Header("HUD")]

    [SerializeField] private EventSystem _eventSystem;

    [SerializeField] private GameObject FirstSelectButtonPause;
    [SerializeField] private GameObject FirstSelectButtonSetting;
    [SerializeField] private GameObject LastSelectButton;
    [SerializeField] private GameObject LastSelectButtonSetting;

    public bool _isPaused = false;
    private bool _isSetting = false;
    private bool _ischoose;
    [SerializeField] private bool ControllerActive = true;

    [Header("ResetPlayer")]
    [SerializeField] private GameObject _player;
    /*[SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Transform _Spawnpoint;*/

    void Start()
    {
        _pauseMenuHUD.SetActive(false);
        ResetPauseHUD();

    }


    void Update()
    {

        if (S_InputManager._playerInputAction.Player.Pause.triggered)
        {
            if (S_InputManager._playerEnable)
            {
                PauseGame();
                return;
            }
        }

        if (S_InputManager._playerInputAction.UI.Pause.triggered)
        {
            if (!S_InputManager._playerEnable)
            {   
                _isSetting = false;
                ResumeGame();    
            }

        }

        if (S_InputManager._playerInput.currentControlScheme == "Gamepad" && ControllerActive && _isPaused && !_isSetting)
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


        if (S_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse" && !ControllerActive && _isPaused && !_isSetting)
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



    }
    public void PauseGame()
    {
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

            Cursor.lockState = CursorLockMode.Locked;
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
            Debug.Log("RestartLevel");
            ResumeGame();
            ResetPlayer();
            StartCoroutine(waitcastchoose());

            Scene _scene = SceneManager.GetActiveScene();

            SceneManager.LoadScene("Manager_Scene");

            SceneManager.LoadScene(_scene.name);

            if(_scene.name == "Tom_Scene")
                SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }
    }
    private void ResetPlayer()
    {
        _BatteryManager._nbrBattery = 0 ;
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

    public void MainMenu()
    {
        if (!_ischoose)
        {
            StartCoroutine(waitcastchoose());
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu_Scene");
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
    }


}