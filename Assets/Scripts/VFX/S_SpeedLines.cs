using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_SpeedLines : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    private S_PlayerMovement _Pm;
    private S_WallRunning _Wr;

    [SerializeField]
    private Image _speedLinesMat;

    private void Start()
    {
        _Pm = _player.GetComponent<S_PlayerMovement>();
        _Wr = _player.GetComponent<S_WallRunning>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_Pm._isDashing || _Pm._isSliding)
        {
            _speedLinesMat.material.SetFloat("_LineFallOff", 0.25f);
        }
        else
        {
            _speedLinesMat.material.SetFloat("_LineFallOff", 1.0f); 
        }

        if(_Wr._isWallLeft)
        {
            _speedLinesMat.material.SetVector("_Center", new Vector2(0.80f, 0.5f));

        }
        else if (_Wr._isWallRight)
        {
            _speedLinesMat.material.SetVector("_Center", new Vector2(0.2f, 0.5f));
        }
        else
        {
           _speedLinesMat.material.SetVector("_Center", new Vector2(0.5f, 0.5f));
        }
    }
}
