using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JumpPadV2 : MonoBehaviour
{
    [Range(0, 10000)] public float _BounceHight;
    [Range(0, 10000)] public float _BounceFront;


    private S_ReferenceInterface _referenceInterface;

    private Transform _orientationPlayer = null;
    private GameObject _playerContent = null;


    private S_PlayerMovement _pm;


    private void Awake()
    {

        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerGameObject;
        _orientationPlayer = _referenceInterface._orientationTransform;
        _pm = _playerContent.GetComponent<S_PlayerMovement>();

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _playerContent)
        {
            GameObject _bouncer = collision.gameObject;
            Rigidbody _rb = _bouncer.GetComponent<Rigidbody>();

            if (_pm._isSliding || _pm._isDashing)
            {
                _rb.AddForce(Vector3.up * _BounceHight * 0.7f);
                _rb.AddForce(_orientationPlayer.forward * _BounceFront * 0.7f);
            }
            else
            {
                _rb.AddForce(Vector3.up * _BounceHight);
                _rb.AddForce(_orientationPlayer.forward * _BounceFront);
            }
        }
    }
}
