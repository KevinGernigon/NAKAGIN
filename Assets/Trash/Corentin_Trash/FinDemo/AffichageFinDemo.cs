using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageFinDemo : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_FinDemo _FinDemo;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _FinDemo = _referenceInterface._FinDemo;
    }

    private void OnTriggerEnter(Collider other)
    {
        _FinDemo.EnableHUDDemo();
    }
}
