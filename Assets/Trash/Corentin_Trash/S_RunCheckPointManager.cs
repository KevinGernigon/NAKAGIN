using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RunCheckPointManager : MonoBehaviour
{
    public Transform _spawnRunCapsule;
    public Transform checkpointCapsule;
    [SerializeField]private S_Timer _Timer;
    //[SerializeField] private S_InfoScore _InfoScore;

    private S_ReferenceInterface _referenceInterface;
    private Transform _playerContent;
    private Rigidbody _rbplayer;
    private GameObject _camera;
    private S_PlayerCam _playerCam;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;

        _rbplayer = _referenceInterface._playerRigidbody;
        _camera = _referenceInterface._CameraGameObject;

        _playerCam = _camera.GetComponent<S_PlayerCam>();

    }


    public void FintimerRespawn()
    { 
        //ResetSpawnPoint();

        _playerContent.position = _spawnRunCapsule.position;
       
        RespawnPlayer();
    }


    public void DeathRespawn()
    {
        _playerContent.position = checkpointCapsule.position;
        RespawnPlayer();
    }


    public void ResetSpawnPoint()
    {
        checkpointCapsule.position = _spawnRunCapsule.position;
       
    }

    private void RespawnPlayer()
    {

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;
        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }

    public void StartChrono()
    {
        _Timer.TimerStart();
        //_InfoScore.SendTimeChallengeToTimer();
    }

    public void StopChrono()
    {
        _Timer.TimerStop();
    }

    public void ResetChrono()
    {
        _Timer.TimerReset();
    }

}
