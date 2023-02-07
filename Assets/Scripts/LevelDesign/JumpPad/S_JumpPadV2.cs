using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JumpPadV2 : MonoBehaviour
{
    [Range(0, 10000)] public float _BounceHight;
    [Range(0, 10000)] public float _BounceFront;

    [SerializeField] private Transform _orientationPlayer;
    [SerializeField] private GameObject _PlayerContent;
    [SerializeField] private S_PlayerMovement pm;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == _PlayerContent)
        {
            GameObject _bouncer = collision.gameObject;
            Rigidbody _rb = _bouncer.GetComponent<Rigidbody>();

            if (pm._isSliding || pm._isDashing)
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
