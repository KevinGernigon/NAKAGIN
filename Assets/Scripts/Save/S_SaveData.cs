using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SaveData : MonoBehaviour
{
    public S_DeathPlayer DeathPlayer;
    public S_BatteryManager BatteryManager;
    public S_InfoScore InfoScore;

    public int deadCount;
    public float nbrBattery;
    public float bestTime;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.L)){
            SaveStats();
        }

        if(Input.GetKeyDown(KeyCode.M)){         
            LoadPlayer();
        }
    }

    public void SaveStats(){
        deadCount = DeathPlayer.DeadCount;
        nbrBattery = BatteryManager._nbrBattery;
        SavePlayer();
    }

    public void SavePlayer(){
        S_SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer(){
        S_PlayerData data = S_SaveSystem.LoadPlayer();

        deadCount = data.robotName;
        nbrBattery = data.nbrBatteryAfterSave;
        bestTime = data.bestTime;
        LoadStats();
    }

    public void LoadStats(){
        DeathPlayer.DeadCount = deadCount;
        DeathPlayer.NewRobotsName();

        BatteryManager._nbrBattery = nbrBattery;
    }
}
