using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class S_GenerateurEnergetique : MonoBehaviour
{
   
    private S_ReferenceInterface _referenceInterface;
    private GameObject Player;

    [Header("Audio")]
    private S_PlayerSound PlayerSoundScript;

    [Header("Charge Energetique")]
    [SerializeField] private int DefaultCharge = 0;
    [SerializeField] private int ChargeEnergetique = 0;
    [SerializeField] private bool _OnTrigger = false;
    private TMP_Text _nb_Generators;
    [Header("Mesh Renderer")]
    [SerializeField] private MeshRenderer _generateurRendererDeploie;
    [SerializeField] private MeshRenderer _generateurRendererpart1;
    [SerializeField] private MeshRenderer _generateurRendererpart2;
    [SerializeField] private MeshRenderer _generateurRendererpart3;
    [SerializeField] private MeshRenderer _ecran;
    [Header("Materials")]
    [SerializeField] private Material _emissiveDefaultMat;
    [SerializeField] private Material _emissiveDisableMat;
    [SerializeField] private Material _ecranDefault;
    [SerializeField] private Material _ecranActive;
    [SerializeField] private Material _2BatTexture;
    [SerializeField] private Material _3BatTexture;
    [Header("Autre")]
    [SerializeField] private LayerMask _whatIsInteractable;
    private GameObject _HUD_InteractGenerateurEnable;
    private GameObject _HUD_InteractGenerateurDisable;
    public InputActionReference ActionRef = null;
    private bool _isSoundRunning = false;
    private bool _isSoundRunning0Bat = false;
    //private TMP_Text _TextInteraction;



    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        Player = _referenceInterface._playerGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();
        _HUD_InteractGenerateurEnable = _referenceInterface.HUD_InteractGenerateurEnable;
        //_HUD_InteractGenerateurDisable = _referenceInterface.HUD_InteractGenerateurDisable;
        _nb_Generators = _referenceInterface.Nb_Generators;
        //_TextInteraction = _HUD_InteractGenerateurEnable.GetComponentInChildren<TMP_Text>();
    }


    private void Start()
    {
        ChargeEnergetique = DefaultCharge;

        _nb_Generators.text = ""+ ChargeEnergetique;

        _generateurRendererpart1.material = _emissiveDisableMat;
        _generateurRendererpart2.material = _emissiveDisableMat;
        _generateurRendererpart3.material = _emissiveDisableMat;

        _generateurRendererDeploie.material = _emissiveDisableMat;

        _ecran.material = _ecranDefault;
      
    }



    private void Update()
    {
        RaycastInteractableGenerateur();

        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && _OnTrigger)
        {

            //Sound Interaction Impossible
            if (ChargeEnergetique <= 0 && !_isSoundRunning0Bat)
            {
                PlayerSoundScript.AccessDeniedSound();
                StartCoroutine(WaitUntilEndSound0Battery());

            }

            if (ChargeEnergetique > 0 && !(ChargeEnergetique == _referenceInterface._BatteryManager._nbrBattery) && !_isSoundRunning)
            {
                PlayerSoundScript.AccessAcceptedSound();
                StartCoroutine(WaitUntilEndSound());
            }

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

                /*if (_referenceInterface._InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
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
     
                }*/

                if (ChargeEnergetique > 0)
                    _HUD_InteractGenerateurEnable.SetActive(true);

               // if(ChargeEnergetique == 0)
                    //_HUD_InteractGenerateurDisable.SetActive(true);


                //_referenceInterface._InputManager.DesactiveJetpackInput();

            }
        }
        else
        {

            _OnTrigger = false;
            _HUD_InteractGenerateurEnable.SetActive(false);
            //_HUD_InteractGenerateurDisable.SetActive(false);

        }
    }

   
    
    public void ChargeUp()
    {
        ChargeEnergetique += 1;
        _nb_Generators.text = ""+ChargeEnergetique;

        _generateurRendererDeploie.material = _emissiveDefaultMat;
       
        if (ChargeEnergetique == 1)
        {
            _generateurRendererpart1.material = _emissiveDefaultMat;
           
            _ecran.material = _ecranActive;
        }

        if (ChargeEnergetique == 2)
        {
            _generateurRendererpart2.material = _emissiveDefaultMat;

            _ecran.material = _2BatTexture;
            
        }

        if (ChargeEnergetique == 3)
        {
            _generateurRendererpart3.material = _emissiveDefaultMat;

            _ecran.material = _3BatTexture;
        }
    }



    public void ReloadBattery()
    {

      //Son Interaction Generateur 

        if ( _referenceInterface._BatteryManager._nbrBattery < ChargeEnergetique )
        {
            //_referenceInterface._BatteryManager._nbrBattery = ChargeEnergetique;

            for (int i = 0; i < ChargeEnergetique; i++)
            {
                _referenceInterface._BatteryManager.GetOneBattery();
            }


        }
    }


    IEnumerator WaitUntilEndSound()
    {
        _isSoundRunning = true;
        yield return new WaitForSeconds(0.5f);
        _isSoundRunning = false;
        ReloadBattery();
    }
    IEnumerator WaitUntilEndSound0Battery()
    {
        _isSoundRunning0Bat = true;
        yield return new WaitForSeconds(1f);
        _isSoundRunning0Bat = false;
    }

}
