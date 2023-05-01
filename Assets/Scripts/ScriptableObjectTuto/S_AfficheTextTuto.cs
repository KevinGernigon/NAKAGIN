using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class S_AfficheTextTuto : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    public SO_TutoAffichage SO_TutoAffichage;

    [SerializeField] private GameObject _textzone1act;
    [SerializeField] private GameObject _textzone2act;
    [SerializeField] private GameObject _simpleTextZone;
    [Header("0 Action")]
    [SerializeField] private TMP_Text _simpleText;
    [Header("1 Action")]
    [SerializeField] private TMP_Text _textPrefixe_InfoTuto1Action;
    [SerializeField] private TMP_Text _textSuffixe_InfoTuto1Action;
    [SerializeField] private Image _imageGamepad1Action;
    [SerializeField] private GameObject _imageGamepadGO1Action;
    [SerializeField] private TMP_Text _text1Action;

    [Header("2 Action")]
    [SerializeField] private TMP_Text _textPrefixe_InfoTuto2Action;
    [SerializeField] private TMP_Text _textSuffixe_InfoTuto2Action;
    [SerializeField] private TMP_Text _textRadical_InfoTuto2Action;
    [SerializeField] private Image _image1Gamepad2Action;
    [SerializeField] private Image _image2Gamepad2Action;
    [SerializeField] private GameObject _images2Action;
    [SerializeField] private TMP_Text _text2Action;

    private bool _ontrigger;
    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
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
                    _textPrefixe_InfoTuto1Action.text = SO_TutoAffichage._prefixTextAffiche;
                    _imageGamepad1Action.sprite = SO_TutoAffichage._ImageGamepad[0];
                    _imageGamepadGO1Action.SetActive(true);
                    _textSuffixe_InfoTuto1Action.text = SO_TutoAffichage._suffixTextAffiche;

                    _text1Action.text = "";
                }
                else
                {
                    _text1Action.text = SO_TutoAffichage._prefixTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[0].action.bindings[SO_TutoAffichage._actionRef[0].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[0].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._suffixTextAffiche;

                    _textPrefixe_InfoTuto1Action.text = "";
                    _imageGamepadGO1Action.SetActive(false);
                    _textSuffixe_InfoTuto1Action.text = "";
                }
                if (SO_TutoAffichage._actionRef[0].action.triggered)
                {
                   
                    _textzone1act.SetActive(false);
                    _ontrigger = false;
                }
            }

            if (SO_TutoAffichage._nbrAction >= 2)
            {
                _simpleTextZone.SetActive(false);
                _textzone1act.SetActive(false);
                _textzone2act.SetActive(true);

                if (_referenceInterface._InputManager._playerInput.currentControlScheme == "Gamepad")
                {
                    _textPrefixe_InfoTuto2Action.text = SO_TutoAffichage._prefixTextAffiche;
                    _images2Action.SetActive(true);
                    _image1Gamepad2Action.sprite = SO_TutoAffichage._ImageGamepad[0];
                    _textRadical_InfoTuto2Action.text = SO_TutoAffichage._radicalTextAffiche;
                    _image2Gamepad2Action.sprite = SO_TutoAffichage._ImageGamepad[1];
                    _textSuffixe_InfoTuto2Action.text = SO_TutoAffichage._suffixTextAffiche;

                    _text2Action.text = "";
                }
                else
                {
                    _text2Action.text = SO_TutoAffichage._prefixTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[0].action.bindings[SO_TutoAffichage._actionRef[0].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[0].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._radicalTextAffiche + " " + InputControlPath.ToHumanReadableString(SO_TutoAffichage._actionRef[1].action.bindings[SO_TutoAffichage._actionRef[1].action.GetBindingIndexForControl(SO_TutoAffichage._actionRef[1].action.controls[0])].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice) + " " + SO_TutoAffichage._suffixTextAffiche;

                    _textPrefixe_InfoTuto2Action.text = "";
                    _images2Action.SetActive(false);
                    _textSuffixe_InfoTuto2Action.text = "";
                }
                if (SO_TutoAffichage._actionRef[0].action.triggered)
                {
                    _textzone2act.SetActive(false);
                    _ontrigger = false;

                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(SO_TutoAffichage._nbrAction < 1)
            _simpleTextZone.SetActive(true);
        else if (SO_TutoAffichage._nbrAction > 0 && SO_TutoAffichage._nbrAction < 2)
            _textzone1act.SetActive(true);
        else
            _textzone2act.SetActive(true);

        _ontrigger = true;

    }
    private void OnTriggerExit(Collider other)
    {
        _simpleTextZone.SetActive(false);
        _textzone1act.SetActive(false);
        _textzone2act.SetActive(false);
        _ontrigger = false;
    }


}
