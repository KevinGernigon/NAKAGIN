using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpeedLines : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _playerRb;
    [SerializeField]
    private Material _shaderMat;
    private void Update()
    {
        Debug.Log(_playerRb.velocity.normalized);
    }
}
