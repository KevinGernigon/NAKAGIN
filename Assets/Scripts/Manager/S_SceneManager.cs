using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class S_SceneManager : Manager
{

    public string sceneToStart;

    //private S_ManagerEditor _managerEditor;

    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(sceneToStart);
    }

    public void LoadGame()
    {
        /*SceneManager.LoadScene(sceneToStart);
        //SceneManager.LoadScene("Tom_Scene");
        if(sceneToStart == "Tom_Scene")
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }*/

        sceneToStart = "Tom_Scene";

        SceneManager.LoadScene(sceneToStart);
        //SceneManager.LoadScene("Tom_Scene");
        if (sceneToStart == "Tom_Scene")
        {
            SceneManager.LoadScene("Asset_Scene", LoadSceneMode.Additive);
        }



    }


}


