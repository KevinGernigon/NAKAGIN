using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_ShowConsole : MonoBehaviour
{  
    
    
    [SerializeField] private S_BatteryManager Battery;
    [SerializeField] private S_GestionnaireScene GestionnaireScene;
    private bool _unlimitedBat;


    // Start is called before the first frame update
    void Start()
    {

        Debug.developerConsoleVisible = false;
      
      /*S_Debugger.AddButton("Scene Maxime", ChangeSceneMaxime);
        S_Debugger.AddButton("Scene GD", ChangeSceneGD);
        S_Debugger.AddButton("Scene Assets", ChangeSceneAsset);
        S_Debugger.AddButton("Main", ChangeSceneLD);*/

        S_Debugger.UpdatableLog("UnlimitedBattery ", "False", Color.white);
        S_Debugger.AddButton("Unlimited Battery", UnlimitedBattery);
        
        S_Debugger.UpdatableLog("Add Battery ","");
        S_Debugger.AddButton("Add Battery", Battery.GetOneBattery);

        S_Debugger.UpdatableLog("TP Tuto ", "");
        S_Debugger.AddButton("Tuto", ChangeSceneTuto);

        S_Debugger.UpdatableLog("TP HUB ", "");
        S_Debugger.AddButton("Hub", ChangeSceneHUB);

        S_Debugger.UpdatableLog("TP Fin ", "");
        S_Debugger.AddButton("Fin", ChangeSceneFin);

    }
    private void Update()
    {
        if(_unlimitedBat && Battery._nbrBattery <= 2 )
        {
            Battery._nbrBattery = 3;
        }
    }

    private void UnlimitedBattery()
    {
        if (!_unlimitedBat)
        {
            _unlimitedBat = true;
            S_Debugger.UpdatableLog("UnlimitedBattery ","True", Color.green); 
        }
        else
        {
            _unlimitedBat = false;
            S_Debugger.UpdatableLog("UnlimitedBattery ", "False", Color.white);
        }
    }

    private void ChangeSceneTuto()
    {
        GestionnaireScene.LoadNewScene(2);
    }

    private void ChangeSceneHUB()
    {
        GestionnaireScene.LoadNewScene(3);
    }
    private void ChangeSceneFin()
    {
        GestionnaireScene.LoadNewScene(4);
    }

    


}
