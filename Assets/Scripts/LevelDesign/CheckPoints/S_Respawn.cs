using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_PlayerCam _playerCam;
    private S_DestructibleObject DestructibleObject;

    private Transform _playerContent;
    private Rigidbody _rbplayer;
    private GameObject _camera;

    public Transform _respawnplayer;

    public bool _isDead = false;


    private void Awake()
    {

        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        DestructibleObject = GetComponent<S_DestructibleObject>();
        _playerContent = _referenceInterface._playerTransform;
        _rbplayer = _referenceInterface._playerRigidbody;
        _camera = _referenceInterface._CameraGameObject;
        
        _playerCam = _camera.GetComponent<S_PlayerCam>();
    }



    [SerializeField] private S_ModuleManager _moduleManager;                    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /////Start Death/////
            _referenceInterface._playerGameObject.GetComponent<S_PlayerSound>().DeathSound();
            //_isDead = true;
            //Change _isDead dans GestionScene
            StartCoroutine(WaitAnimToRespawn());
        }
    }

    IEnumerator WaitAnimToRespawn()
    {
        _referenceInterface._InputManager._playerInputAction.Player.Disable();
        yield return new WaitForSeconds(5f); //Death Anim
        _referenceInterface._InputManager._playerInputAction.Player.Enable();
        /////After Death/////
        //_isDead = false;
        //Change _isDead dans GestionScene
        _playerContent.position = _respawnplayer.position;
        _rbplayer.velocity = new Vector3(0, 0, 0);

        _moduleManager.ResetPlatformRotation();
        
        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);

        Physics.SyncTransforms();
    }

}