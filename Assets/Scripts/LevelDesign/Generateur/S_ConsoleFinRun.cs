using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class S_ConsoleFinRun : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private GameObject Player;
    [SerializeField] private GameObject[] _energetiqueDoor;
    private bool _activeEnergetiqueDoor;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;
    public InputActionReference ActionRef = null;

    [Header("Generateur Lié")]
    [SerializeField] private S_GenerateurEnergetique[] _GenerateurEnergetique;

    [Header("Console Info")]
    public bool consoleOpen;
    [SerializeField] private bool _consoleActive = false;
    private bool _OnTrigger = false;
    [SerializeField] private LayerMask _whatIsInteractable;

    [Header("Animation")]
    [SerializeField] private Animator _animOuverture;

    [Header("Mat Ecran Verouillage")]
    [SerializeField] private Renderer _screenVerrou;
    [SerializeField] private Material _dfltMatVerrou;
    [SerializeField] private Material _actvMatVerrou;
    [Header("Mat Ecran Information ")]
    [SerializeField] private Renderer _screenInfo;
    [SerializeField] private Material _dfltMatInfo;
    [SerializeField] private Material _actvMatInfo;


    private GameObject _HUD_Interaction;
    private TMP_Text _textInteraction;
    private GameObject _imageInteraction;
    private Animator _AnimInfoTuto;
    [SerializeField] private bool _isOnSphereTrigger;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();



        Player = _referenceInterface._playerGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();

        /* _HUD_Interaction = _referenceInterface.HUD_InteractGenerateurEnable;
         _TextInteraction = _HUD_Interaction.GetComponentInChildren<TMP_Text>();*/

        _AnimInfoTuto = _referenceInterface.animInfo;
        _HUD_Interaction = _referenceInterface.HUD_InteractGenerateurEnable;
        _textInteraction = _referenceInterface.TextInteraction;
        _imageInteraction = _referenceInterface.ImageInteraction;
    }


    private void Start()
    {
        if(!_consoleActive)
        {
            _screenVerrou.material = _dfltMatVerrou;
            _screenInfo.material = _dfltMatInfo;
        }
        else
        {
            _screenVerrou.material = _actvMatVerrou;
            _screenInfo.material = _actvMatInfo;
        }

        if(_activeEnergetiqueDoor)
        {
            for (int i = 0; i < _energetiqueDoor.Length; i++)
            {
                _energetiqueDoor[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < _energetiqueDoor.Length; i++)
            {
                _energetiqueDoor[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (consoleOpen)
        {
            _screenVerrou.material = _actvMatVerrou;
        


            RaycastHit hit;
            if (Physics.Raycast(_referenceInterface._CameraGameObject.transform.position,_referenceInterface._CameraGameObject.transform.forward,out hit,5, _whatIsInteractable))
            {
                if(hit.collider.gameObject == gameObject)
                {
                    _OnTrigger = true;
                    if (!_consoleActive)
                    {
                        if (_referenceInterface._InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
                        {
                            _imageInteraction.SetActive(false);

                            int bindingIndex = ActionRef.action.GetBindingIndexForControl(ActionRef.action.controls[0]);
                            _textInteraction.text = InputControlPath.ToHumanReadableString(
                            ActionRef.action.bindings[bindingIndex].effectivePath,
                            InputControlPath.HumanReadableStringOptions.OmitDevice
                            );

                           //_TextInteraction.text = _referenceInterface._InputManager._playerInputAction.Player.Interaction;
                           //Insert text ou Image lier a l'interaction 

                        }
                        if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                        {

                            _textInteraction.text = "Y";
                            _imageInteraction.SetActive(true);
                            
                        }
                        _HUD_Interaction.SetActive(true);
                        if(_isOnSphereTrigger)
                            _AnimInfoTuto.SetBool("IsOpen", true);                //  _AnimInfoTuto.Play("A_TooltipOpen");
                    }
                     _referenceInterface._InputManager.DesactiveJetpackInput();
                }    
            }
            else
            {
                _OnTrigger = false;
                if (!_consoleActive)
                    _HUD_Interaction.SetActive(false);
                if (_isOnSphereTrigger)
                    _AnimInfoTuto.SetBool("IsOpen", false);                //_AnimInfoTuto.Play("A_TooltipClose");

                _referenceInterface._InputManager.ActiveJetpackInput();
            }
        }

        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && !_consoleActive && _OnTrigger)
        {
            //Son
            PlayerSoundScript.ValidationConsoleSound();

            _consoleActive = true;

            for(int i = 0; i < _GenerateurEnergetique.Length; i++)
            {
                _GenerateurEnergetique[i].ChargeUp();

            }

            _HUD_Interaction.SetActive(false);

            if (_isOnSphereTrigger)
                _AnimInfoTuto.SetBool("IsOpen", false);                //_AnimInfoTuto.Play("A_TooltipClose");

            _screenInfo.material = _actvMatInfo;

            _activeEnergetiqueDoor = true;
            for (int i = 0; i < _energetiqueDoor.Length; i++)
            {
                _energetiqueDoor[i].SetActive(true);
            }
           

        }
    }

    public void Deployment()
    {
        if (consoleOpen && !_consoleActive)
        {
            _animOuverture.Rebind();
            _animOuverture.Play("A_Ouverture");
        }
            _isOnSphereTrigger = true ;
    }

    public void Replit()
    {
        if (consoleOpen && !_consoleActive)
        {
            _animOuverture.Rebind();
            _animOuverture.Play("A_Fermeture");
        }
            _isOnSphereTrigger = false;
    }
   
    

}
