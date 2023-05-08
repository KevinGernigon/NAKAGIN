using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_FinDemo : MonoBehaviour
{
    
    [SerializeField] private S_InputManager _InputManager;
    [SerializeField] private GameObject _finDemoHUD;

    private void Start()
    {
        _finDemoHUD.SetActive(false);
    }

    public void EnableHUDDemo()
    {
        _finDemoHUD.SetActive(true);    
        _InputManager.ActivePause();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }


    public void DisableHUDDemo()
    {
        _finDemoHUD.SetActive(false);
        _InputManager.DesactivePause();
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

}
