using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_Respawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private GameObject _camera;

    //public Transform _respawnplayer;

    public bool _isDead = false;
    [SerializeField] private S_ModuleManager _moduleManager;

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

            _isDead = false;

            /////Start Death/////
    
            //_referenceInterface._playerGameObject.GetComponent<S_PlayerSound>().DeathSound();
            StartCoroutine(WaitToRespawn());

            /////After Death/////   


        }
    }

    IEnumerator WaitToRespawn()
    {
        //stop crono
        _runCheckPointManager.StopChrono();

        _referenceInterface._InputManager._playerInputAction.Player.Disable();
        yield return new WaitForSeconds(2f); //Death Anim
        _referenceInterface._InputManager._playerInputAction.Player.Enable();
        _isDead = true;



        /////After Death/////       
        //Module Rotation reset
        _moduleManager.ResetPlatformRotation();

        //Player position reset
        if (_runCheckPointManager.checkpointCapsule.position == _runCheckPointManager._spawnRunCapsule.position )
        {
            _runCheckPointManager.DeathRespawn();
            //reset chrono
            _runCheckPointManager.ResetChrono();

           
        }
        else
        {
            _runCheckPointManager.DeathRespawn();

            //start crono
            _runCheckPointManager.StartChrono();
        }
    }
}