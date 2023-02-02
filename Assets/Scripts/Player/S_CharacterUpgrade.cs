using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CharacterUpgrade : MonoBehaviour
{
    [Header("Character Upgrade")]

    [Header("References")]
    [SerializeField] private S_PlayerMovement pm;
    [SerializeField] private S_BatteryManager ScriptBatteryManager;

    [Header("Boolean")]
    private bool _isSpeedUpgradable;

    private void Start()
    {
        _isSpeedUpgradable = true;
    }
    private void Update()
    {
        InputManager();
    }

    private void InputManager()
    {
        if ((Input.GetButtonDown("BoostSpeed") && _isSpeedUpgradable) && ScriptBatteryManager._nbrBattery >= 1)
        {
            ScriptBatteryManager.UseOneBattery();
            _isSpeedUpgradable = false;
            SpeedUpgrade();
            StartCoroutine(upgradeSpeed());
        }
    }

    private void SpeedUpgrade()
    {
        pm.SpeedValueUpgrade();
    }

    IEnumerator upgradeSpeed()
    {
        yield return new WaitForSeconds(2f);
        _isSpeedUpgradable = true;
    }
}
