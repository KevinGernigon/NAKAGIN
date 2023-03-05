using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class S_GenerateurEnergetique : MonoBehaviour
{
   
    private S_ReferenceInterface _referenceInterface;


    [Header("Charge Energetique")]

    [SerializeField] private float DefaultCharge = 0f;
    [SerializeField] private float ChargeEnergetique = 0f;
    [SerializeField] private bool _OnTrigger = false;


    [SerializeField] private GameObject _indicateurCharge1;
    [SerializeField] private GameObject _indicateurCharge2;
    [SerializeField] private GameObject _indicateurCharge3;


    [SerializeField] private LayerMask _whatIsInteractable;

    private GameObject _HUD_InteractGenerateurEnable;
    private GameObject _HUD_InteractGenerateurDisable;

    public InputActionReference ActionRef = null;
    private TMP_Text _TextInteraction;
  


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _HUD_InteractGenerateurEnable = _referenceInterface.HUD_InteractGenerateurEnable;
        _HUD_InteractGenerateurDisable = _referenceInterface.HUD_InteractGenerateurDisable;

        _TextInteraction = _HUD_InteractGenerateurEnable.GetComponentInChildren<TMP_Text>();
    }


    private void Start()
    {
        ChargeEnergetique = DefaultCharge;
        _indicateurCharge1.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        _indicateurCharge2.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        _indicateurCharge3.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
    }



    private void Update()
    {
        RaycastInteractableGenerateur();

        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && _OnTrigger)
        {

            //Sound Interaction Impossible
            if (ChargeEnergetique > 0)
                ReloadBattery();

        }

        

    }
    public void RaycastInteractableGenerateur()
    {
        RaycastHit hit;
        if (Physics.Raycast(_referenceInterface._CameraGameObject.transform.position, _referenceInterface._CameraGameObject.transform.forward, out hit, 1, _whatIsInteractable))
        {
            if (hit.collider.gameObject == gameObject)
            {
                _OnTrigger = true;

                if (_referenceInterface._InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
                {
                    //Insert text ou Image lier a l'interaction 

                    int bindingIndex = ActionRef.action.GetBindingIndexForControl(ActionRef.action.controls[0]);

                    _TextInteraction.text = InputControlPath.ToHumanReadableString(
                    ActionRef.action.bindings[bindingIndex].effectivePath,
                    InputControlPath.HumanReadableStringOptions.OmitDevice
                    );

                  
                    

                }
                if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                {
                    //Insert text ou Image lier a l'interaction 
                    _TextInteraction.text = "Y";


                }

                if (ChargeEnergetique > 0)
                    _HUD_InteractGenerateurEnable.SetActive(true);

                if(ChargeEnergetique == 0)
                    _HUD_InteractGenerateurDisable.SetActive(true);


                //_referenceInterface._InputManager.DesactiveJetpackInput();

            }
        }
        else
        {
            _OnTrigger = false;
            _HUD_InteractGenerateurEnable.SetActive(false);
            _HUD_InteractGenerateurDisable.SetActive(false);
            //_referenceInterface._InputManager.ActiveJetpackInput();

        }
    }

   
    
    public void ChargeUp()
    {
        ChargeEnergetique += 1f;

        if(ChargeEnergetique == 1)
            _indicateurCharge1.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        if (ChargeEnergetique == 2)
            _indicateurCharge2.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        if (ChargeEnergetique == 3)
            _indicateurCharge3.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
    }




    private void ReloadBattery()
    {
      if ( _referenceInterface._BatteryManager._nbrBattery < ChargeEnergetique )
      {
            _referenceInterface._BatteryManager._nbrBattery = ChargeEnergetique;
      }
    }



}
