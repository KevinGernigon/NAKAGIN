using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class S_SlomotionTuto : MonoBehaviour
{
    private float _timerCd;
    [SerializeField] private float _timerMaxCd;

    [SerializeField] private bool _isPlay = false;
    [SerializeField] private bool _istrigger;
    [SerializeField] InputActionReference _actionRef;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isPlay)
        {
            _istrigger = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!_isPlay)
        {
            _istrigger = false;
        }
    }

    private void Update()
    {
        if (_actionRef.action.triggered && _istrigger)
        {
            _timerCd = 0;
            _istrigger = false;

            //_isPlay = true;
        }
    }
    private void FixedUpdate()
    {
        if(_istrigger)
        {
            _timerCd += Time.deltaTime;
            if (_timerCd < _timerMaxCd)
            {
                Time.timeScale = 0.2f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        
    }



}
