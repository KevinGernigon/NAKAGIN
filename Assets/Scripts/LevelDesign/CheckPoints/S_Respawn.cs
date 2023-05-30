using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Respawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private GameObject _camera;

    [SerializeField] private GameObject _managerRun;
    private S_RunCheckPointManager _runCheckPointManager;
    private S_DeathPlayer _DeathPlayer;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
       
        _runCheckPointManager = _managerRun.GetComponent<S_RunCheckPointManager>();

        _DeathPlayer = _referenceInterface.deathPlayer;

    }
       

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_DeathPlayer.playerIsDead)
        {
            /////Start Death/////
         
            _runCheckPointManager.DeathPlayer();

            /////After Death/////   

        }
    }

}