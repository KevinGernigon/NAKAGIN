using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class S_PauseMenuV2 : MonoBehaviour
{
    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    [Header("Other")]

    [SerializeField] private GameObject _pauseMenuHUD;
    [SerializeField] private GameObject _startGameHUD;

    [SerializeField] private GameObject _pauseInterface;
    [SerializeField] private GameObject _settingsInterface;

    [SerializeField] private S_BatteryManager _BatteryManager;

    public bool _isPaused = false;
    private bool _ischoose;

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
                ResumeGame(); 
        }
    }

    public void PauseGame()
    {
        S_InputManager.ActivePause();

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

            
        }
    }

    private void ResetPlayer()
    {
        _BatteryManager._nbrBattery = 0 ;
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