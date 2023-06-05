using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_GestionMenu : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_GestionnaireScene _GestionnaireScene;
    private S_InputManager _InputManager;
    private GameObject _disableManager;
    private GameObject _UI;
    private GameObject _loadingScreen;
    [SerializeField]private Animator _animIntroGame;
        
    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _InputManager = _referenceInterface._InputManager;
        _disableManager = _referenceInterface.DisableManager;
        _UI = _referenceInterface._UICanvas;
        _loadingScreen = _referenceInterface._LoadingScreen;
        _GestionnaireScene = _referenceInterface._GestionnaireScene;
    }



    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        _loadingScreen.SetActive(false);
        _disableManager.SetActive(false);
        _InputManager.ActivePause();
        //_InputManager.ActivePause();


        if(!_GestionnaireScene.DisIntroGame)
        {
            _animIntroGame.Play("GamePres");
            //_GestionnaireScene.DisIntroGame = true;
        }
    }

    public void EndScene()
    {
        _disableManager.SetActive(true);
       //_InputManager.DesactivePause();
        _UI.SetActive(true);

    }
}
