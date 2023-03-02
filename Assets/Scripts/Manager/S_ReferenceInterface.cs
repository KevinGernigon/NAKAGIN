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
    

    [Header("Refs Timer")]

    public TMP_Text _timerText;

    [Header("Refs Timer")]

    public GameObject _UICanvas;

    [Header("Refs Battery")]

    public GameObject EventSysteme;
    public S_BatteryManager _BatteryManager;


    [Header("Refs InputManager")]
    public S_InputManager _InputManager;

    [Header("Audio")]
    public S_PlayerSound PlayerSoundScript;
    private void Awake()
    {
        _BatteryManager = EventSysteme.GetComponent<S_BatteryManager>();
        PlayerSoundScript = GetComponent<S_PlayerSound>();
    }
}
