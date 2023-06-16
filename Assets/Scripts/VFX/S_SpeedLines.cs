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

    [SerializeField] private S_PauseMenuV2 S_PauseMenuV2;

    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;
    [SerializeField] private S_GestionnaireScene S_GestionnaireScene;


    private void Start()
    {
        //StartCoroutine("incrementSpeedLines");
    }

    private void OnEnable()
    {
        StartCoroutine("incrementSpeedLines");
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
            _shaderMat.SetFloat("_SL_LineDensity", 0.5f);
        }
        /*else if(_playerMovement.state == S_PlayerMovement.MovementState.air)
        {
            _shaderMat.SetFloat("_SL_LineDensity", 0.3f);
        }*/
       /* else if(_playerMovement._walkSpeed > 50.0f)
        {
            //_shaderMat.SetFloat("_SL_LineDensity", 0.45f / 2 * (playerVelocityx + playerVelocityy + playerVelocityz + 0.1f * _increaseSpeedLines));
            _shaderMat.SetFloat("_SL_LineDensity", _playerMovement._walkSpeed/150);
        }
        else
        {
            _shaderMat.SetFloat("_SL_LineDensity", 0);
        }*/

        if(S_GestionnaireScene.InMenu)
        {
            _shaderMat.SetFloat("_Activate_SpeedLines", 0);
        }
        else
        {
            _shaderMat.SetFloat("_Activate_SpeedLines", 1);
        }


    }

    IEnumerator incrementSpeedLines()
    {
        while (true)
        {
            if (_playerMovement.state != S_PlayerMovement.MovementState.dashing || _playerMovement.state != S_PlayerMovement.MovementState.air && !_playerMovement._isGrappleActive)
            {

                if (_playerMovement._walkSpeed > 50.0f)
                {
                    if (_shaderMat.GetFloat("_SL_LineDensity") < 0.4f)
                    {
                        _shaderMat.SetFloat("_SL_LineDensity", _shaderMat.GetFloat("_SL_LineDensity") + 0.001f);
                    }
                    else if (_shaderMat.GetFloat("_SL_LineDensity") > 0.4f) _shaderMat.SetFloat("_SL_LineDensity", _shaderMat.GetFloat("_SL_LineDensity") - 0.005f);
                }
                else if (_shaderMat.GetFloat("_SL_LineDensity") > 0 && _shaderMat.GetFloat("_SL_LineDensity") < 0.4)
                {
                    _shaderMat.SetFloat("_SL_LineDensity", _shaderMat.GetFloat("_SL_LineDensity") - 0.0025f);
                }
                else _shaderMat.SetFloat("_SL_LineDensity", 0);
            }
            yield return new WaitForSeconds(0.01f);
        }
        
    }

}
