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
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        _loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            _loadingBarFill.fillAmount = progressValue; 
            
            yield return new WaitForSeconds(0.01f);
        }

        if (sceneId== 2)
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }

        _loadingScreen.SetActive(false);
        yield return null;
    }
}
