using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class S_PlayerData
{
    public int robotName;
    public float nbrBatteryAfterSave;
    public float bestTime;

    public S_PlayerData(S_SaveData player)
    {
        robotName = player.deadCount;
        nbrBatteryAfterSave = player.nbrBattery;
        bestTime = player.bestTime;
    }
}
