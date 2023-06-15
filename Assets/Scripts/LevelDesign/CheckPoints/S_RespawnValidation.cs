using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_RespawnValidation : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private GameObject _camera;

    [SerializeField] private GameObject _managerRun;
    private S_RunCheckPointManagerValidation _runCheckPointManagerValidation;
    private S_DeathPlayer _DeathPlayer;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        _runCheckPointManagerValidation = _managerRun.GetComponent<S_RunCheckPointManagerValidation>();
        _DeathPlayer = _referenceInterface.deathPlayer;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_DeathPlayer.playerIsDead)
        {
            /////Start Death/////

            
            _runCheckPointManagerValidation.DeathPlayer();

            /////After Death/////   

        }
    }

}