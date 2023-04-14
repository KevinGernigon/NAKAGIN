using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TeleportSpawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    private Rigidbody _rbplayer;
    private Transform _playerContent;
    private S_PlayerCam _playerCam;
    public Transform _respawnplayer;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
        _rbplayer = _referenceInterface._playerRigidbody;
        _playerCam = _referenceInterface._CameraGameObject.GetComponent<S_PlayerCam>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _playerContent.position = _respawnplayer.transform.position;
            _rbplayer.velocity = new Vector3(0, 0, 0);

            //Player Camera rest
            var x = this.transform.rotation.eulerAngles.x;
            var y = this.transform.rotation.eulerAngles.y;
            _playerCam.CameraReset(x, y);

            Physics.SyncTransforms();
        }
    }
}
