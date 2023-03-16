using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_ReferenceInterface : Manager
{

    [Header("Refs PlayerContent")]

    public GameObject _playerGameObject;
    public Transform _playerTransform;
    public Rigidbody _playerRigidbody;

    [Header("Refs Orentiation")]

    public Transform _orientationTransform;


    [Header("Refs Camera")]

    public GameObject _CameraGameObject;


    [Header("Refs Jetpack")]
    public S_Jetpack _Jetpack;

    [Header("Refs UI")]

    public TMP_Text _timerText;

    public GameObject HUD_InteractGenerateurEnable;
    public GameObject HUD_InteractGenerateurDisable;

    [Header("Refs UI")]

    public GameObject _UICanvas;
    public GameObject _UIStartHUD;
    public GameObject HUD_Death;

    [Header("Refs Battery")]

    public GameObject EventSystem;
    public S_BatteryManager _BatteryManager;


    [Header("Refs InputManager")]
    public S_InputManager _InputManager;

    [Header("Audio")]
    public S_PlayerSound PlayerSoundScript;
    private void Awake()
    {
        _BatteryManager = EventSystem.GetComponent<S_BatteryManager>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
    }
}
