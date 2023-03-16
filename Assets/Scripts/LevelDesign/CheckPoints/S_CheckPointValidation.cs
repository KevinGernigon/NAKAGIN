using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CheckPointValidation : MonoBehaviour
{
    [SerializeField] private Transform _capsuleCheckpointArea;
    //[SerializeField] private Transform _capsuleRespawn;
    [SerializeField] private S_RunCheckPointManagerValidation _RunCheckPointManagerValidation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("checkPoint");
            _RunCheckPointManagerValidation.checkpointCapsule.transform.position = _capsuleCheckpointArea.transform.position;
            _RunCheckPointManagerValidation.checkpointCapsule.transform.position = new Vector3(_RunCheckPointManagerValidation.checkpointCapsule.transform.position.x, _RunCheckPointManagerValidation.checkpointCapsule.transform.position.y, _RunCheckPointManagerValidation.checkpointCapsule.transform.position.z);


            _RunCheckPointManagerValidation.checkpointCapsule.transform.eulerAngles = new Vector3(_capsuleCheckpointArea.transform.rotation.eulerAngles.x, _capsuleCheckpointArea.transform.rotation.eulerAngles.y, _capsuleCheckpointArea.transform.rotation.eulerAngles.z);

            Physics.SyncTransforms();
        }
    }

}