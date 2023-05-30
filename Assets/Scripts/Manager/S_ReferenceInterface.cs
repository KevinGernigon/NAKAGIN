using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class S_ReferenceInterface : Manager
{
    [Header("Refs DisableManager")]
    public GameObject DisableManager;

    [Header("Refs PlayerContent")]
    public GameObject _playerGameObject;
    public Transform _playerTransform;
    public Rigidbody _playerRigidbody;

    [Header("Refs Death")]
    public S_DeathPlayer deathPlayer;

    [Header("Refs Orentiation")]
    public Transform _orientationTransform;

    [Header("Refs Camera")]
    public GameObject _CameraGameObject;

    [Header("Refs Jetpack")]
    public S_Jetpack _Jetpack;

    [Header("Refs Dash")]
    public S_Dash _Dash;

    [Header("Refs UI Anim Timer")]
    public Animator _HUDtimer;
    public TMP_Text _timerText;
    public Animator _HUDTimerFont;

    [Header("Refs UI")]
    public GameObject HUD_InteractGenerateurEnable;
    public TMP_Text TextInteraction;
    public GameObject ImageInteraction;

    public GameObject _UICanvas;
    public GameObject _UIStartHUD;
    public GameObject _UIPauseHUD;
    public GameObject _UIFinDemoHUD;
    public GameObject _UISettingsInterface;
    public GameObject _UIVideoSettings;
    public GameObject _UIVideoButton;

    public GameObject _LoadingScreen;
    public GameObject HUD_Death;
    public GameObject HUDGrappin;


    [Header("Refs UI InfoTuto")]
    public GameObject infoTuto;
    public Animator animInfo;
    public GameObject textzone1act;
    public GameObject textzone2act;
    public GameObject simpleTextZone;
    [Header("0 Action")]
    public TMP_Text simpleText;
    [Header("1 Action")]
    public TMP_Text textPrefixe_InfoTuto1Action;
    public TMP_Text textSuffixe_InfoTuto1Action;
    public Image imageGamepad1Action;
    public GameObject imageGamepadGO1Action;
    public TMP_Text text1Action;
    [Header("2 Action")]
    public TMP_Text textPrefixe_InfoTuto2Action;
    public TMP_Text textSuffixe_InfoTuto2Action;
    public TMP_Text textRadical_InfoTuto2Action;
    public Image image1Gamepad2Action;
    public Image image2Gamepad2Action;
    public GameObject images2Action;
    public TMP_Text text2Action;

    [Header("Refs Battery")]
    public GameObject EventSystem;
    public S_BatteryManager _BatteryManager;
    public TMP_Text Nb_Generators;

    [Header("Refs InputManager")]
    public S_InputManager _InputManager;

    [Header("Refs Gestionnaire Scene")]
    public S_GestionnaireScene _GestionnaireScene;
    public S_FinDemo _FinDemo;

    public S_PlayFabManager PlayFabManager;

    [Header("Audio")]
    public S_PlayerSound PlayerSoundScript;
    private void Awake()
    {
        _BatteryManager = EventSystem.GetComponent<S_BatteryManager>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
    }
}
