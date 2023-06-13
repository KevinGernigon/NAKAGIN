using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class S_AffichageInfoTuto : MonoBehaviour
{
     private S_ReferenceInterface _referenceInterface;
     [SerializeField] private SO_TutoAffichage SO_TutoAffichage;

        private GameObject _infoTuto;
        private Animator _AnimInfoTuto;
        
        private GameObject _textzone1act;
        private GameObject _textzone2act;
        private GameObject _simpleTextZone;
        
        private TMP_Text _simpleText;
       
        private TMP_Text _textPrefixe_InfoTuto1Action;
        private TMP_Text _textSuffixe_InfoTuto1Action;
        private Image _imageGamepad1Action;
        private GameObject _imageGamepadGO1Action;
        private TMP_Text _text1Action;

        
        private TMP_Text _textPrefixe_InfoTuto2Action;
        private TMP_Text _textSuffixe_InfoTuto2Action;
        private TMP_Text _textRadical_InfoTuto2Action;
        private Image _image1Gamepad2Action;
        private Image _image2Gamepad2Action;
        private GameObject _images2Action;
        private TMP_Text _text2Action;


    private bool _ontrigger;
    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _infoTuto = _referenceInterface.infoTuto;
        _AnimInfoTuto = _referenceInterface.animInfo;

        _textzone1act = _referenceInterface.textzone1act;
        _textzone2act = _referenceInterface.textzone2act;
        _simpleTextZone = _referenceInterface.simpleTextZone;

        _simpleText = _referenceInterface.simpleText;

        _textPrefixe_InfoTuto1Action = _referenceInterface.textPrefixe_InfoTuto1Action;
        _textSuffixe_InfoTuto1Action = _referenceInterface.textSuffixe_InfoTuto1Action;
        _imageGamepad1Action = _referenceInterface.imageGamepad1Action;
        _imageGamepadGO1Action = _referenceInterface.imageGamepadGO1Action;
        _text1Action = _referenceInterface.text1Action;

        _textPrefixe_InfoTuto2Action = _referenceInterface.textPrefixe_InfoTuto2Action;
        _textSuffixe_InfoTuto2Action = _referenceInterface.textSuffixe_InfoTuto2Action;
        _textRadical_InfoTuto2Action = _referenceInterface.textRadical_InfoTuto2Action;
        _image1Gamepad2Action = _referenceInterface.image1Gamepad2Action;
        _image2Gamepad2Action = _referenceInterface.image2Gamepad2Action;
        _images2Action = _referenceInterface.images2Action;
        _text2Action = _referenceInterface.text2Action;

    }


    private void Update()
    {

        if (_ontrigger)
        {

            if (SO_TutoAffichage._nbrAction < 1)
            {
                _simpleTextZone.SetActive(true);
                _textzone1act.SetActive(false);
                _textzone2act.SetActive(false);

                _simpleText.text = SO_TutoAffichage._radicalTextAffiche;

            }

            if (SO_TutoAffichage._nbrAction > 0 && SO_TutoAffichage._nbrAction < 2)
            {
                _simpleTextZone.SetActive(false);
                _textzone1act.SetActive(true);
                _textzone2act.SetActive(false);

                    if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                    {
                        //_textPrefixe_InfoTuto1Action.text = SO_TutoAffichage._prefixTextAffiche;
                        _imageGamepad1Action.sprite = SO_TutoAffichage._ImageGamepad[0];
                        _imageGamepadGO1Action.SetActive(true);
                        _textSuffixe_InfoTuto1Action.text = SO_TutoAffichage._suffixTextAffiche;

                        _text1Action.text = "";
                    }
                    else
                    {
                        if (SO_TutoAffichage._ImageMode)
                        {

                            //_text1Action.text = SO_TutoAffichage._prefixTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[0].action.bindings[SO_TutoAffichage._actionRef[0].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[0].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._suffixTextAffiche;
                            //_textPrefixe_InfoTuto1Action.text = SO_TutoAffichage._prefixTextAffiche;
                        
                            _imageGamepadGO1Action.SetActive(true);
                            _imageGamepad1Action.sprite = SO_TutoAffichage._ImageKeyboard[0];
                            _textSuffixe_InfoTuto1Action.text = SO_TutoAffichage._suffixTextAffiche;
                            _text1Action.text = "";

                    }
                        else
                        {
                            _imageGamepadGO1Action.SetActive(false);
                            _text1Action.text = InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[0].action.bindings[SO_TutoAffichage._actionRef[0].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[0].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
                            _textSuffixe_InfoTuto1Action.text = SO_TutoAffichage._suffixTextAffiche;

                        }
                    }
            }

            if (SO_TutoAffichage._nbrAction >= 2)
            {
                _simpleTextZone.SetActive(false);
                _textzone1act.SetActive(false);
                _textzone2act.SetActive(true);

                if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                {
                    //_textPrefixe_InfoTuto2Action.text = SO_TutoAffichage._prefixTextAffiche;
                    _images2Action.SetActive(true);
                    _image1Gamepad2Action.sprite = SO_TutoAffichage._ImageGamepad[0];
                    _textRadical_InfoTuto2Action.text = SO_TutoAffichage._radicalTextAffiche;
                    _image2Gamepad2Action.sprite = SO_TutoAffichage._ImageGamepad[1];
                    _textSuffixe_InfoTuto2Action.text = SO_TutoAffichage._suffixTextAffiche;

                    //_text2Action.text = "";
                }
                else
                {
                    if (SO_TutoAffichage._ImageMode)
                    {


                        //_textPrefixe_InfoTuto2Action.text = SO_TutoAffichage._prefixTextAffiche;
                        _images2Action.SetActive(true);
                        _image1Gamepad2Action.sprite = SO_TutoAffichage._ImageKeyboard[0];
                        _textRadical_InfoTuto2Action.text = SO_TutoAffichage._radicalTextAffiche;
                        _image2Gamepad2Action.sprite = SO_TutoAffichage._ImageKeyboard[1];
                        _textSuffixe_InfoTuto2Action.text = SO_TutoAffichage._suffixTextAffiche;

                        _text2Action.text = "";
                    }
                    else
                    {
                        _text2Action.text = SO_TutoAffichage._prefixTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[0].action.bindings[SO_TutoAffichage._actionRef[0].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[0].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._radicalTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[1].action.bindings[SO_TutoAffichage._actionRef[1].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[1].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._suffixTextAffiche;
                        _images2Action.SetActive(false);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _infoTuto.SetActive(true);

        //_AnimInfoTuto.SetBool("IsOpen", true);
        _AnimInfoTuto.Play("A_TooltipOpen");

        if (SO_TutoAffichage._nbrAction < 1)
            _simpleTextZone.SetActive(true);
        else if (SO_TutoAffichage._nbrAction > 0 && SO_TutoAffichage._nbrAction < 2)
            _textzone1act.SetActive(true);
        else
            _textzone2act.SetActive(true);
        _ontrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {

        //_AnimInfoTuto.SetBool("IsOpen", false);               
        _AnimInfoTuto.Play("A_TooltipClose");

        _simpleTextZone.SetActive(false);
        _textzone1act.SetActive(false);
        _textzone2act.SetActive(false);
        _ontrigger = false;

        _infoTuto.SetActive(false);
    }



}
