using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_GestionnaireScene : MonoBehaviour
{

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _loadingBarFill;

    public void LoadNewScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }


    IEnumerator LoadSceneAsync(int sceneId)
    {
        _loadingScreen.SetActive(true);
        AsyncOperation operation1 = SceneManager.LoadSceneAsync(sceneId);
        if (sceneId == 2)
        {
            AsyncOperation operation2 = SceneManager.LoadSceneAsync("Asset_Scene", LoadSceneMode.Additive);

            while (!operation1.isDone || !operation2.isDone)
            {
                float progressValue = Mathf.Clamp01(operation1.progress + operation2.progress / 1.8f);
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
}
