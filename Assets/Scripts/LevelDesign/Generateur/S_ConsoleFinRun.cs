using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class S_ConsoleFinRun : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    public InputActionReference ActionRef = null;

    [SerializeField] private S_GenerateurEnergetique _GenerateurEnergetique;

    [SerializeField] private bool _consoleActive = false;
    [SerializeField] private bool _OnTrigger = false;
    [SerializeField] private LayerMask _whatIsInteractable;

    private GameObject _HUD_Interaction;

    private TMP_Text _TextInteraction;

    [SerializeField] private GameObject _interfaceConsole;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _HUD_Interaction = _referenceInterface.HUD_InteractGenerateurEnable;

        _TextInteraction = _HUD_Interaction.GetComponentInChildren<TMP_Text>();
    }


    private void Start()
    {
        _interfaceConsole.GetComponent<Renderer>().material.color = new Color(255, 0, 0);

    }



    private void Update()
    {

        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && !_consoleActive && _OnTrigger)
        {
            //Son

            _consoleActive = true;
            _GenerateurEnergetique.ChargeUp();

            _HUD_Interaction.SetActive(false);

            _interfaceConsole.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }


        RaycastHit hit;
        if (Physics.Raycast(_referenceInterface._CameraGameObject.transform.position,_referenceInterface._CameraGameObject.transform.forward,out hit,1, _whatIsInteractable))
        {

            
            if(hit.collider.gameObject == gameObject)
            {
                _OnTrigger = true;

                if (!_consoleActive)
                {
  
                    if (_referenceInterface._InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
                    {

                        int bindingIndex = ActionRef.action.GetBindingIndexForControl(ActionRef.action.controls[0]);

                        _TextInteraction.text = InputControlPath.ToHumanReadableString(
                        ActionRef.action.bindings[bindingIndex].effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice
                        );

                       //_TextInteraction.text = _referenceInterface._InputManager._playerInputAction.Player.Interaction;
                       //Insert text ou Image lier a l'interaction 

                    }
                    if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                    {

                        _TextInteraction.text = "Y";

                        //Image lier a l'interaction 

                    }


                    _HUD_Interaction.SetActive(true);
                }

            
                 _referenceInterface._InputManager.DesactiveJetpackInput();

            }
            

        }
        else
        {
            _OnTrigger = false;
            if (!_consoleActive)
                _HUD_Interaction.SetActive(false);
            
            _referenceInterface._InputManager.ActiveJetpackInput();

        }
    }

}
