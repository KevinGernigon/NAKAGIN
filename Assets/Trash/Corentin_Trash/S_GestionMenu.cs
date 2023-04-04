using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GestionMenu : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_InputManager _InputManager;
    private GameObject _disableManager;

        
    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _InputManager = _referenceInterface._InputManager;
        _disableManager = _referenceInterface.DisableManager;

    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        _disableManager.SetActive(false);
        _InputManager.ActivePause();

    }

    public void EndScene()
    {
        _disableManager.SetActive(true);
        _InputManager.DesactivePause();
    }
}
