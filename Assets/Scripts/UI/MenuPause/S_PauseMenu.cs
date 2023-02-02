using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class S_PauseMenu: MonoBehaviour
{
    public GameObject _pauseMenu;
    public static bool _isPaused;
    private bool _ischoose;
    [SerializeField]
    private GameObject _player;

    void Start()
    {

        _pauseMenu.SetActive(false);
        S_Debugger.AddButton("Quit", QuitGame);

    }


    void Update()
    {
        if (Input.GetButtonDown("MenuPause"))
        {
            if (_isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        //AudioListener = false;
        _isPaused = true;
    }

    public void ResumeGame()
    {
        if (!_ischoose)
        {
            StartCoroutine(waitcastchoose());
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _pauseMenu.SetActive(false);
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
            StartCoroutine(waitcastchoose());
            Scene _scene = SceneManager.GetActiveScene();
            if (_scene.name == "Tom_Scene")
            {
                SceneManager.LoadScene(_scene.name);
                SceneManager.LoadScene("Alexis_Blocking_Environment", LoadSceneMode.Additive);
            }
            else
            {
                SceneManager.LoadScene(_scene.name);
            }
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

    }
