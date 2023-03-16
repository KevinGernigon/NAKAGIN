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


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
       
        _runCheckPointManager = _managerRun.GetComponent<S_RunCheckPointManager>();
       

    }
       

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

          

            /////Start Death/////

         
            _runCheckPointManager.DeathPlayer();

            /////After Death/////   
          

        }
    }

}