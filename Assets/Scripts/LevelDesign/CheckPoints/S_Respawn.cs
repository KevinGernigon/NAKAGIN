using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Respawn : MonoBehaviour
{

    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rbplayer;
    public Transform _respawnplayer;
    [SerializeField] private Transform _camera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            _player.transform.position = _respawnplayer.transform.position;
            _rbplayer.velocity = new Vector3(0, 0, 0);

            _camera.GetComponent<S_PlayerCam>().CameraReset();

            Physics.SyncTransforms();
        }
    }
}
