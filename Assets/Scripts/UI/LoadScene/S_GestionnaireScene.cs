using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_GestionnaireScene : MonoBehaviour
{

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBarFill;
    [SerializeField] private S_PauseMenuV2 S_PauseMenuV2;
    [SerializeField] private S_InputManager _InputManager;

    [SerializeField] private GameObject DisablePlayer;
    [SerializeField] private GameObject _controlerIMG;
    [SerializeField] private GameObject _keyboardIMGAzerty;
    [SerializeField] private GameObject _keyboardIMGQwerty;


    public bool InMenu;
    public bool DisIntroGame;



    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            InMenu = true;
            S_PauseMenuV2._ischoose = false;
        }
        else
        {
            InMenu = false;
        }
    }



    public void LoadNewScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }


    IEnumerator LoadSceneAsync(int sceneId)
    {
        _loadingScreen.SetActive(true);
        if(_InputManager._playerInput.currentControlScheme == "Gamepad") 
        {
            _controlerIMG.SetActive(true);

            if (S_PauseMenuV2._qwertyMode)
            {
                _keyboardIMGAzerty.SetActive(false);
                _keyboardIMGQwerty.SetActive(true);
            }
            else
            {
                _keyboardIMGAzerty.SetActive(true);  
                _keyboardIMGQwerty.SetActive(false);
            }
        }
        else
        {
            if (S_PauseMenuV2._qwertyMode)
            {
                _keyboardIMGAzerty.SetActive(false);
                _keyboardIMGQwerty.SetActive(true);
            }
            else
            {
                _keyboardIMGQwerty.SetActive(false);
                _keyboardIMGAzerty.SetActive(true);
            }

            _controlerIMG.SetActive(false);
        }

        AsyncOperation operation1 = SceneManager.LoadSceneAsync(sceneId);

        if (sceneId == 3)
        {
            DisablePlayer.SetActive(false);

            AsyncOperation operation2 = SceneManager.LoadSceneAsync("Run_1", LoadSceneMode.Additive);
            AsyncOperation operation3 = SceneManager.LoadSceneAsync("Run_2_et_HUB", LoadSceneMode.Additive);
            AsyncOperation operation4 = SceneManager.LoadSceneAsync("Run_3", LoadSceneMode.Additive);
            AsyncOperation operation5 = SceneManager.LoadSceneAsync("Decor_Scene", LoadSceneMode.Additive);
            AsyncOperation operation6 = SceneManager.LoadSceneAsync("Run_Validation", LoadSceneMode.Additive);

           // AsyncOperation operation2 = SceneManager.LoadSceneAsync("Asset_Scene", LoadSceneMode.Additive);
           // AsyncOperation operation3 = SceneManager.LoadSceneAsync("Light_Scene", LoadSceneMode.Additive);

            while (!operation1.isDone || !operation2.isDone || !operation3.isDone || !operation4.isDone || !operation5.isDone)
            {
                float progressValue = Mathf.Clamp01(operation1.progress + operation2.progress + operation3.progress + operation4.progress + operation5.progress + operation6.progress / 6.48f);
                _loadingBarFill.fillAmount = progressValue; 
            
                yield return new WaitForSeconds(0.01f);

            }
            DisablePlayer.SetActive(true);
        }
        else
        {
            while (!operation1.isDone)
            {
                float progressValue = Mathf.Clamp01(operation1.progress / 0.9f);
                _loadingBarFill.fillAmount = progressValue;

                yield return new WaitForSeconds(0.01f);
                
            }
            //DisablePlayer.SetActive(true);
        }

        _loadingScreen.SetActive(false);

        yield return null;
    }
    
    public void DisalbleIntro()
    {
        DisIntroGame = true;
    }

}
