using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MovingOnPlatform : MonoBehaviour
{
    private GameObject PlayerContent;
    private Rigidbody Rigidbody;
    private S_ReferenceInterface ReferenceInterface;

    private bool _isOnTrigger;
    private void Start()
    {
        ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        PlayerContent = ReferenceInterface._playerGameObject;
        Rigidbody = ReferenceInterface._playerRigidbody;
    }

    private void Update()
    {
        if (_isOnTrigger)
            Rigidbody.AddForce(this.transform.forward * 50, ForceMode.Force);
        else
            return;
    }
    private void OnTriggerEnter(Collider other)
    {
        _isOnTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isOnTrigger = false;
    }

}
