using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_PlayerCam _playerCam;

    private Transform _playerContent;
    private Rigidbody _rbplayer;
    private GameObject _camera;

    public Transform _respawnplayer;


    private void Awake()
    {

        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        _playerContent = _referenceInterface._playerTransform;
        _rbplayer = _referenceInterface._playerRigidbody;
        _camera = _referenceInterface._CameraGameObject;
        
        _playerCam = _camera.GetComponent<S_PlayerCam>();
    }



    //[SerializeField] private S_ModuleManager _moduleManager;                      //Commentaire temporaire


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /////Start Death/////
            _referenceInterface._playerGameObject.GetComponent<S_PlayerSound>().DeathSound();
            StartCoroutine(WaitForEndOfTheSound());
        }
    }

    IEnumerator WaitForEndOfTheSound()
    {
        _referenceInterface._InputManager._playerInputAction.Player.Disable();
        yield return new WaitForSeconds(4f);
        _referenceInterface._InputManager._playerInputAction.Player.Enable();
        /////After Death/////
        _playerContent.position = _respawnplayer.position;
        _rbplayer.velocity = new Vector3(0, 0, 0);

        //_moduleManager.ResetPlatformRotation();                               //Commentaire temporaire

        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;

        _playerCam.CameraReset(x, y);

        Physics.SyncTransforms();
    }

}