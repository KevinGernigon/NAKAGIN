using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ShowConsole : MonoBehaviour
{  
    
    
    [SerializeField] private S_BatteryManager Battery;



    // Start is called before the first frame update
    void Start()
    {

        Debug.developerConsoleVisible = true;
      
        /*S_Debugger.AddButton("Scene Maxime", ChangeSceneMaxime);
        S_Debugger.AddButton("Scene GD", ChangeSceneGD);*/
        S_Debugger.AddButton("Main", ChangeSceneLD);
        S_Debugger.AddButton("Scene Assets", ChangeSceneAsset);
        S_Debugger.AddButton("Add Battery", Battery.GetOneBattery);
    }

    private void ChangeSceneMaxime()
    {
        SceneManager.LoadScene("Scene_Industriel");
    }
    private void ChangeSceneGD()
    {
        SceneManager.LoadScene("TestGDScene");
    }

    private void ChangeSceneLD()
    {
        SceneManager.LoadScene("MAIN_Vertical_Slice");
    }

    private void ChangeSceneAsset()
    {
        SceneManager.LoadScene("Assets_Scene");
    }
}
