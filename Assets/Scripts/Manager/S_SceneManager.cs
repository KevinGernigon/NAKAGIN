using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManager : Manager
{

    public string sceneToStart;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(sceneToStart);
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(sceneToStart);
    }


}


