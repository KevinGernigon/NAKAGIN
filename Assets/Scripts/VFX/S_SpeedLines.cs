using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SpeedLines : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _playerRb;
    [SerializeField]
    private Material _shaderMat;
    [SerializeField]
    private S_PlayerMovement _playerMovement;

    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;


    private void Start()
    {
        //StartCoroutine("incrementSpeedLines");
    }

    private void Update()
    {
        var playerVelocityx = _playerRb.velocity.normalized.x;
        var playerVelocityy = _playerRb.velocity.normalized.y;
        var playerVelocityz = _playerRb.velocity.normalized.z;

        

        if (playerVelocityx < 0)
        {
            playerVelocityx = -playerVelocityx;
        }
        if (playerVelocityy < 0)
        {
            playerVelocityy = -playerVelocityy;
        }
        if (playerVelocityz < 0)
        {
            playerVelocityz = -playerVelocityz;
        }

        if (_playerMovement.state == S_PlayerMovement.MovementState.dashing || _playerMovement.state == S_PlayerMovement.MovementState.air && _playerMovement._isGrappleActive)
        {
            _shaderMat.SetFloat("_SL_LineDensity", 0.4f);
        }
        /*else if(_playerMovement.state == S_PlayerMovement.MovementState.air)
        {
            _shaderMat.SetFloat("_SL_LineDensity", 0.3f);
        }*/
        else
        {
            //_shaderMat.SetFloat("_SL_LineDensity", 0.45f / 2 * (playerVelocityx + playerVelocityy + playerVelocityz + 0.1f * _increaseSpeedLines));
            _shaderMat.SetFloat("_SL_LineDensity", 0);
        }
    }

    /*IEnumerator incrementSpeedLines()
    {
        while (true)
        {
            if (S_InputManager._mouvementInput.x != 0 || S_InputManager._mouvementInput.y != 0)
            {
                if (_increaseSpeedLines < 1.0f)
                {
                    _increaseSpeedLines += 0.001f;
                }
            }
            else
            {
                _increaseSpeedLines = 0.0f;
            }
            yield return new WaitForSeconds(0.01f);
        }
        
    }*/
}
