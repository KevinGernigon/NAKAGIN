using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class S_SceneManager : Manager
{ 
    private enum Scene
    {
        Corentin_Scene,
    }

    [SerializeField]
    private S_ManagerEditor _managerEditor;

    public void LoadGame()
    { 
        //SceneManager.LoadScene(_managerEditor.sceneToStart);
        SceneManager.LoadScene(Scene.Corentin_Scene.ToString());
    }

}


