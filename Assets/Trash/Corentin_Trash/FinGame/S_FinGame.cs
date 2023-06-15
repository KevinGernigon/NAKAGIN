using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class S_FinGame : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;
    private GameObject _HUD_Interaction;
    private TMP_Text _textInteraction;
    private GameObject _imageInteraction;
    private Animator _AnimInfoTuto;
    private GameObject Player;

    public InputActionReference ActionRef = null;
    private S_PlayerSound PlayerSoundScript;
    private S_GestionnaireScene _GestionnaireScene;

    [SerializeField] private bool _consoleActive = false;
    [SerializeField] private Animator _animOuverture;

    [SerializeField] private Animator _animCredit;
    [SerializeField] private GameObject _Credit;

    [SerializeField] private LayerMask _whatIsInteractable;
    [SerializeField] private bool _isOnSphereTrigger;

    [SerializeField] private Renderer _screenInfo;
    [SerializeField] private Material _dfltMatInfo;
    [SerializeField] private Material _actvMatInfo;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();

        _AnimInfoTuto = _referenceInterface.animInfo;
        _HUD_Interaction = _referenceInterface.HUD_InteractGenerateurEnable;
        _textInteraction = _referenceInterface.TextInteraction;
        _imageInteraction = _referenceInterface.ImageInteraction;
        _GestionnaireScene = _referenceInterface._GestionnaireScene;

        Player = _referenceInterface._playerGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(_referenceInterface._CameraGameObject.transform.position, _referenceInterface._CameraGameObject.transform.forward, out hit, 15, _whatIsInteractable))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!_consoleActive)
                {
                    if (_referenceInterface._InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
                    {
                        _imageInteraction.SetActive(false);
                        _textInteraction.gameObject.SetActive(true);

                        int bindingIndex = ActionRef.action.GetBindingIndexForControl(ActionRef.action.controls[0]);
                        _textInteraction.text = InputControlPath.ToHumanReadableString(
                        ActionRef.action.bindings[bindingIndex].effectivePath,
                        InputControlPath.HumanReadableStringOptions.OmitDevice
                        );

                    }
                    if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                    {

                        _textInteraction.text = "Y";
                        _textInteraction.gameObject.SetActive(false);
                        _imageInteraction.SetActive(true);

                    }
                    _HUD_Interaction.SetActive(true);
                    if (_isOnSphereTrigger)
                        _AnimInfoTuto.SetBool("IsOpen", true);                //  _AnimInfoTuto.Play("A_TooltipOpen");
                }
            }
        }
        else
        {

            if (!_consoleActive)
                _HUD_Interaction.SetActive(false);
            if (_isOnSphereTrigger)
                _AnimInfoTuto.SetBool("IsOpen", false);                //_AnimInfoTuto.Play("A_TooltipClose");

        }


        if (_referenceInterface._InputManager._playerInputAction.Player.Interaction.triggered && !_consoleActive)
        {
            PlayerSoundScript.ValidationConsoleSound();
            _consoleActive = true;
            _HUD_Interaction.SetActive(false);

            if (_isOnSphereTrigger)
                _AnimInfoTuto.SetBool("IsOpen", false);                //_AnimInfoTuto.Play("A_TooltipClose");

            _screenInfo.material = _actvMatInfo;


            StartCoroutine(LancementCredit());


        }

    }


    IEnumerator LancementCredit()
    {
        _referenceInterface._InputManager._playerInput.DeactivateInput();

        
        //Désactive l'ia qui eteint son oeil




        yield return new WaitForSeconds(5f);

        Debug.Log("Fin du Game ");

        _Credit.SetActive(true);
        _animCredit.Play("A_Credits");//Fade noir avec(défilé) des credits

        yield return new WaitForSeconds(10f);
        
        _Credit.SetActive(false);
        _referenceInterface._InputManager._playerInput.ActivateInput();

        
        _GestionnaireScene.LoadNewScene(1); //Retour au menu du jeu

    }

    

    /*\
     * 
     * Le joueur appuis sur une consolle        -ok
     * 
     * Le joueur ne peut plus bouger
     * 
     * Désactive l'ia qui eteint son oeil
     * 
     * Fade noir avec (défilé) des credits 
     *
     * Retour au menu du jeu                    -ok
     *
    \*/

    public void Deployment()
    {
        if (!_consoleActive)
        {
            _animOuverture.Rebind();
            _animOuverture.Play("A_Ouverture");
        }
        _isOnSphereTrigger = true;
    }

    public void Replit()
    {
        if (!_consoleActive)
        {
            _animOuverture.Rebind();
            _animOuverture.Play("A_Fermeture");
        }
        _isOnSphereTrigger = false;
    }

    public void IALootPlayer()
    {

    }
    public void IAStopLootPlayer()
    {

    }


   
}
