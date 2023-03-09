using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckPoint : MonoBehaviour
{
    [SerializeField] private Transform _capsuleCheckpointArea;
    //[SerializeField] private Transform _capsuleRespawn;
    [SerializeField]private S_RunCheckPointManager _RunCheckPointManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("checkPoint");
            _RunCheckPointManager.checkpointCapsule.transform.position = _capsuleCheckpointArea.transform.position;
            _RunCheckPointManager.checkpointCapsule.transform.position = new Vector3(_RunCheckPointManager.checkpointCapsule.transform.position.x, _RunCheckPointManager.checkpointCapsule.transform.position.y, _RunCheckPointManager.checkpointCapsule.transform.position.z);


            _RunCheckPointManager.checkpointCapsule.transform.eulerAngles = new Vector3(_capsuleCheckpointArea.transform.rotation.eulerAngles.x, _capsuleCheckpointArea.transform.rotation.eulerAngles.y, _capsuleCheckpointArea.transform.rotation.eulerAngles.z);
            
            Physics.SyncTransforms();
        }
    }

}
