using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_TriggerBattery : MonoBehaviour
{

    [SerializeField] private S_BatteryManager ScriptBatteryManager;

    private void OnTriggerEnter(Collider other)
    {
        if((ScriptBatteryManager._nbrBattery >= ScriptBatteryManager._nbrMaxBattery) && !ScriptBatteryManager._overdrive)
        {
            Destroy(gameObject);
            ScriptBatteryManager.GetOneBattery();
        }
        if((ScriptBatteryManager._nbrBattery < ScriptBatteryManager._nbrMaxBattery) && !ScriptBatteryManager._overdrive)
        {
            Destroy(gameObject);
            ScriptBatteryManager.GetOneBattery();
        }
       
    }
}



/*if((ScriptBatteryManager._nbrBattery < ScriptBatteryManager._nbrMaxBattery) && ScriptBatteryManager._overdrive)
        {
            Debug.Log("Battery < Max && overdrive");
            Destroy(gameObject);
            ScriptBatteryManager.GetOneBattery();
        }
        else if(ScriptBatteryManager._nbrBattery <= ScriptBatteryManager._nbrMaxBattery && !ScriptBatteryManager._overdrive)
        {
            Debug.Log("Battery <= Max && !overdrive");
            Destroy(gameObject);
            ScriptBatteryManager.GetOneBattery();       
        }
        else
        {
            //Full Bat + Overdive
        }*/