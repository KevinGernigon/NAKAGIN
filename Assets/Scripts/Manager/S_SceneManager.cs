using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public enum DisplayCategory
{
    Corentin_Scene, Kiki_Scene, Alexis_Scene, Assets_Scene, MAIN_VerticalSlice, Killian_Scene, Tom_Scene, Playtest_Scene, Maxime_Scene, Kevin_Scene
}


public class S_SceneManager : Manager
{

    public string sceneToStart;

    private S_ManagerEditor _managerEditor;
    [SerializeField] private DisplayCategory categoryToDisplay;


    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(sceneToStart);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToStart);

        //SceneManager.LoadScene("Tom_Scene");

        if(sceneToStart == "Tom_Scene")
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }
/*
        sceneToStart = "Tom_Scene";

        SceneManager.LoadScene(sceneToStart);

        //SceneManager.LoadScene("Tom_Scene");
        if (sceneToStart == "Tom_Scene")
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }

*/

    }


}


