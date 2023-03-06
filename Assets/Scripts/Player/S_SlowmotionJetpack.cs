using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_SlowmotionJetpack : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_Jetpack S_Jetpack;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        S_Jetpack = _referenceInterface._Jetpack;

    }


    private void OnTriggerEnter(Collider other)
    {
        S_Jetpack.BooleanTriggerBoxEnter();
        _referenceInterface._InputManager._playerInputAction.Player.Dash.Disable();



    }
    private void OnTriggerExit(Collider other)
    {

        S_Jetpack.BooleanTriggerBoxExit();
        _referenceInterface._InputManager._playerInputAction.Player.Dash.Enable();
    }
}
