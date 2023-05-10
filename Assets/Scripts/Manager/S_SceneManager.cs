using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum DisplayCategory
{
    Corentin_Scene, Kiki_Scene, HUB, Alexis_Scene, Assets_Scene, MAIN_VerticalSlice, Killian_Scene, Tom_Scene, Tom_Test_Scene, Playtest_Scene, Maxime_Scene, Kevin_Scene, Tuto_Scene, IA_Scene, MainMenu, Light_Scene, Run_2, Decor_Scene
}


public class S_SceneManager : Manager
{
    public string sceneToStart;

    [SerializeField] private DisplayCategory categoryToDisplay;

    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(sceneToStart);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToStart);
        gameObject.SetActive(true);

        if(sceneToStart == "HUB")
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
            SceneManager.LoadScene("Light_Scene", LoadSceneMode.Additive);
            //zSceneManager.LoadScene("Run_2", LoadSceneMode.Additive);
            SceneManager.LoadScene("Decor_Scene", LoadSceneMode.Additive);
        }

        /*if (sceneToStart == "MainMenu")
        {
            gameObject.SetActive(false);
        } */
    }
}


