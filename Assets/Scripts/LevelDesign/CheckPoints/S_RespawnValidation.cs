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


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        _runCheckPointManagerValidation = _managerRun.GetComponent<S_RunCheckPointManagerValidation>();


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /////Start Death/////

            
            _runCheckPointManagerValidation.DeathPlayer();

            /////After Death/////   

        }
    }

}