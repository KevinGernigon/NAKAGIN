using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RunCheckPointManagerValidation : MonoBehaviour
{
    public Transform _spawnRunCapsule;
    public Transform checkpointCapsule;
    [SerializeField]private S_Timer _Timer;
    [SerializeField] private S_InfoScoreValidation _InfoScoreValidation;
    [SerializeField] private S_GestionScene _gestionScene;


    private S_ReferenceInterface _referenceInterface;
    private Transform _playerContent;
    private Rigidbody _rbplayer;
    private GameObject _camera;
    private S_PlayerCam _playerCam;
    private S_DeathPlayer _deathPlayer;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
        _rbplayer = _referenceInterface._playerRigidbody;
        _camera = _referenceInterface._CameraGameObject;
        _deathPlayer = _referenceInterface.deathPlayer;

        _playerCam = _camera.GetComponent<S_PlayerCam>();

    }

    private void Update()
    {
        if (_referenceInterface._InputManager._playerInputAction.Player.ResetRun.triggered && _InfoScoreValidation._runStart)
        {
            DeathPlayer();
        }
        if (_referenceInterface._InputManager._playerInputAction.Player.RestartRun.triggered && _InfoScoreValidation._runStart)
        {
            checkpointCapsule.position = _spawnRunCapsule.position;
            DeathPlayer();
        }

    }

    public void FintimerRespawn()
    { 
        _playerContent.position = _spawnRunCapsule.position;
        _deathPlayer.RespawnPlayer(_playerContent);
    }


    public void DeathRespawn()
    {
        _playerContent.position = checkpointCapsule.position;
        _deathPlayer.RespawnPlayer(_playerContent);
    }


    public void ResetSpawnPoint()
    {
        checkpointCapsule.position = _spawnRunCapsule.position;
    }

    public void StartChrono()
    {
            _Timer.TimerStart();
    }

    public void StopChrono()
    {
            _Timer.TimerStop();
    }

    public void ResetChrono()
    {
        _Timer.TimerReset();
    }
    public void DeathPlayer()
    {
        StartCoroutine(WaitToRespawn());
    }

    IEnumerator WaitToRespawn()
    {
        _deathPlayer.playerIsDead = true;

        StopChrono();

        _referenceInterface._playerGameObject.GetComponent<S_PlayerSound>().DeathSound();
        _referenceInterface._InputManager._playerInputAction.Player.Disable();
        _referenceInterface.HUD_Death.SetActive(true);

        yield return new WaitForSeconds(_deathPlayer.TimeToRespawnPlayer);

        /////After Death/////  
        
        _gestionScene.ResetEventOnRun();

        if (checkpointCapsule.position == _spawnRunCapsule.position)
        {
            _InfoScoreValidation._runStart = false;
            DeathRespawn();
            ResetChrono();
        }
        else
        {
            DeathRespawn();
            StartChrono();
        }

        _referenceInterface._InputManager._playerInputAction.Player.Enable();
        _referenceInterface.HUD_Death.SetActive(false);

        _deathPlayer.playerIsDead = false;

    }
}
