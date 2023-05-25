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
    public bool InMenu;

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
        AsyncOperation operation1 = SceneManager.LoadSceneAsync(sceneId);

        if (sceneId == 3)
        {
            AsyncOperation operation2 = SceneManager.LoadSceneAsync("Asset_Scene", LoadSceneMode.Additive);
            AsyncOperation operation3 = SceneManager.LoadSceneAsync("Light_Scene", LoadSceneMode.Additive);

            while (!operation1.isDone || !operation2.isDone)
            {
                float progressValue = Mathf.Clamp01(operation1.progress + operation2.progress + operation3.progress / 2.7f);
                _loadingBarFill.fillAmount = progressValue; 
            
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (!operation1.isDone)
            {
                float progressValue = Mathf.Clamp01(operation1.progress / 0.9f);
                _loadingBarFill.fillAmount = progressValue;

                yield return new WaitForSeconds(0.01f);
            }
        }

        _loadingScreen.SetActive(false);
        yield return null;
    }

    public string getCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
