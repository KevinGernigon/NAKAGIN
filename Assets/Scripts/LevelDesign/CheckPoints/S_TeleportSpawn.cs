using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TeleportSpawn : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    private Rigidbody _rbplayer;
    private Transform _playerContent;

    public Transform _respawnplayer;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
        _rbplayer = _referenceInterface._playerRigidbody;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _playerContent.position = _respawnplayer.transform.position;
            _rbplayer.velocity = new Vector3(0, 0, 0);

            Physics.SyncTransforms();
        }
    }
}
