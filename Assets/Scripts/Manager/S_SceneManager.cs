using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum DisplayCategory
{
    Corentin_Scene, Kiki_Scene, HUB, Alexis_Scene, Assets_Scene, MAIN_VerticalSlice, Kilian_Scene, Killian_Scene, Tom_Scene, Tom_Test_Scene, Playtest_Scene, Maxime_Scene, Kevin_Scene, Tuto_Scene, IA_Scene, MainMenu, Light_Scene, Run_2_et_HUB, Run_3, Run_1, Decor_Scene, Scene_IA_Fin
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

        if(sceneToStart == "Kilian_Scene")
        {
            SceneManager.LoadScene("Run_1", LoadSceneMode.Additive);
            SceneManager.LoadScene("Run_2_et_HUB", LoadSceneMode.Additive);
            SceneManager.LoadScene("Run_3", LoadSceneMode.Additive);
            //ceneManager.LoadScene("Light_Scene", LoadSceneMode.Additive);
            //zSceneManager.LoadScene("Run_2", LoadSceneMode.Additive);
            SceneManager.LoadScene("Decor_Scene", LoadSceneMode.Additive);
            //SceneManager.LoadScene("Alexis_Scene", LoadSceneMode.Additive);
        }

        /*if (sceneToStart == "MainMenu")
        {
            gameObject.SetActive(false);
        } */
    }
}


