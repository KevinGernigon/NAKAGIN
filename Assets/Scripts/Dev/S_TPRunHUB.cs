using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TPRunHUB : MonoBehaviour
{
    private S_ReferenceInterface _ReferenceInterface;
    private Transform PlayerContent;
    private Rigidbody _rbplayer;
    private S_PlayerCam _playerCam;

    private Transform RespawnOrientation;

    [SerializeField] Transform SpawnpointRun1;
    [SerializeField] Transform SpawnpointRun2;
    [SerializeField] Transform SpawnpointRun3;
    [SerializeField] Transform SpawnpointRunPrincipal;

    private void Awake()
    {
        _ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        PlayerContent = _ReferenceInterface._playerTransform;
        _rbplayer = _ReferenceInterface._playerRigidbody;
        _playerCam = _ReferenceInterface._PlayerCam;
    }


    private void Start()
    {

        StartCoroutine(DebugTPHUB());

    }


    IEnumerator DebugTPHUB()
    {
        yield return new WaitForSeconds(1.5f);

        S_Debugger.UpdatableLog("TP Run ", "");
        S_Debugger.AddButton("   ", Clear);

        S_Debugger.UpdatableLog("Reactor", "");
        S_Debugger.AddButton("Run1 ", Run1);

        S_Debugger.UpdatableLog("Outside", "");
        S_Debugger.AddButton("Run2 ", Run2);

        S_Debugger.UpdatableLog("Warehouse ", "");
        S_Debugger.AddButton("Run3", Run3);

        S_Debugger.UpdatableLog("Validation", "");
        S_Debugger.AddButton("RunPrincipal", RunPrincipal);
    }

    private void Clear()
    {

    }

    private void Run1()
    {
        RespawnOrientation = SpawnpointRun1 ;
        PlayerContent.position = SpawnpointRun1.position;

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = RespawnOrientation.rotation.eulerAngles.x;
        var y = RespawnOrientation.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }

    private void Run2()
    {
        RespawnOrientation = SpawnpointRun2;
        PlayerContent.position = SpawnpointRun2.position;


        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = RespawnOrientation.rotation.eulerAngles.x;
        var y = RespawnOrientation.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();



    }

    private void Run3()
    {
        RespawnOrientation = SpawnpointRun3;
        PlayerContent.position = SpawnpointRun3.position;

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = RespawnOrientation.rotation.eulerAngles.x;
        var y = RespawnOrientation.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }

    private void RunPrincipal()
    {
        RespawnOrientation = SpawnpointRunPrincipal;
        PlayerContent.position = SpawnpointRunPrincipal.position;

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = RespawnOrientation.rotation.eulerAngles.x;
        var y = RespawnOrientation.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }


}
