using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_GestionScene : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;
    private S_InputManager _InputManager;

    [SerializeField] Texture2D cursor;

    [Header("Reset Player")]
    private Transform _playerTransform;
    private Rigidbody _playerRb;
    private GameObject _camera;
    private S_PlayerCam _playerCam;
    private TMP_Text _textTimer;
    private Color _savecolor;
    [SerializeField] private Transform _spawnPoint;

    [Header("Reset Wall")]
    [SerializeField] private S_ModuleManager _moduleManager;

    [Header("Info ManagerRun")]
    [SerializeField] private float _bestTimeSaveRun1;
    [SerializeField] private S_InfoScore _ManagerRun1;
    [SerializeField] private float _bestTimeSaveRun2;
    [SerializeField] private S_InfoScore _ManagerRun2;
    [SerializeField] private float _bestTimeSaveRun3;
    [SerializeField] private S_InfoScore _ManagerRun3;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _InputManager = _referenceInterface._InputManager;

        _playerTransform = _referenceInterface._playerTransform;
        _playerRb = _referenceInterface._playerRigidbody;

        _camera = _referenceInterface._CameraGameObject;
        _playerCam = _camera.GetComponent<S_PlayerCam>();

        _textTimer = _referenceInterface._timerText;
        _savecolor = _textTimer.color;


        
        //Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Start()
    {
        _InputManager.DesactivePause();
  
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        _playerTransform.position = _spawnPoint.position;

        _playerRb.velocity = new Vector3(0, 0, 0);
        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;
        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();

        _textTimer.text = "";
        _textTimer.color = _savecolor;


        if (_bestTimeSaveRun1 != 0f)
        {
            _ManagerRun1.ChargeSave(_bestTimeSaveRun1);
        }
        if (_bestTimeSaveRun2 != 0f)
        {
            _ManagerRun2.ChargeSave(_bestTimeSaveRun2);
        }
        if (_bestTimeSaveRun3 != 0f)
        {
            _ManagerRun3.ChargeSave(_bestTimeSaveRun3);
        }
    }

    
    public void ResetEventOnRun()           //call a la mort du joueur!
    { 
        _moduleManager.ResetPlatformRotation();
    }




}
