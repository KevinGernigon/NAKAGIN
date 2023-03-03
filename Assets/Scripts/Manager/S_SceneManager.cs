using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManager : Manager
{
    private enum Scene
    {
        Manager_Scene,
        Assets_Scene,
        Corentin_Scene,
        Kiki_Scene,Tom_Scene

    }

    private List<string> _allScenes;
    private int _indexCurrent;


    private void LoadScene(Scene scene) 
    {
        SceneManager.LoadScene(scene.ToString());
    }


    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.Manager_Scene.ToString());
    }


    public void LoadGame()
    {
        SceneManager.LoadScene(Scene.Tom_Scene.ToString());
    }


    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1) ;
    }

   
}


