using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_GestionScene : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;


    [Header("Reset Player")]
    private Transform _playerTransform;
    private Rigidbody _playerRb;
    private GameObject _camera;
    private S_PlayerCam _playerCam;

    [Header("Reset Wall")]
    public bool _IsDead;

    [SerializeField] private Transform _spawnPoint;
    private TMP_Text _textTimer;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerTransform = _referenceInterface._playerTransform;
        _playerRb = _referenceInterface._playerRigidbody;

        _camera = _referenceInterface._CameraGameObject;
        _playerCam = _camera.GetComponent<S_PlayerCam>();

        _textTimer = _referenceInterface._timerText;

    }

    private void Start()
    {
        _playerTransform.position = _spawnPoint.position;
        _playerRb.velocity = new Vector3(0, 0, 0);
        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;
        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();

        _textTimer.text = "";

    }


    private void Update()
    {
        
    }
}
