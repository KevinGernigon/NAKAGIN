using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_TeleportSpawn : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rbplayer;
    public Transform _respawnplayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            _player.transform.position = _respawnplayer.transform.position;
            _rbplayer.velocity = new Vector3(0, 0, 0);

            Physics.SyncTransforms();
        }
    }
}
