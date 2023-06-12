using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_IALootYou : MonoBehaviour
{
    private S_ReferenceInterface S_ReferenceInterface;
    private Transform Player;

    [SerializeField] private Transform _oeil;
    [SerializeField] private Animator _animDisableIA;
    private bool _isTarget;
    private bool _IADisable;
    private Transform _defaultPlacement;


    private void Awake()
    {
        S_ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        Player = S_ReferenceInterface._playerTransform;
        _defaultPlacement = _oeil;
    }



    private void Update()
    {
        if (_isTarget)
        {
            _oeil.LookAt(Player);

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DisableIA();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        _isTarget = true;
    }



    private void OnTriggerExit(Collider other)
    {
        _isTarget = false;
    }


    public void StopLookPlayer()
    {
        _isTarget = false;
    }



    public void DisableIA()
    {
        StopLookPlayer();

        _animDisableIA.Play("");
        
    }


}

