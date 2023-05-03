using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ObjectOnCamera : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;

    [SerializeField] private Collider _triggerUI;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _HUDGrappin;
    [SerializeField] private S_ObjectOnCamera _Self;

    private S_GrappinV2 _s_GrappinV2;

    private GameObject _eventSystem;
    private S_PauseMenuV2 S_PauseMenuV2;

    private GameObject _playerContent;
    private GameObject _canvasUIGameObject;

    private Canvas _canvasUI;
    private GameObject _GrappinUI;

    [SerializeField] public Transform lookat;

    Plane[] cameraFrustum;
    [SerializeField] private bool _seePlayer;
    private GameObject _UI;
    private Camera _mainCamera;
    public bool _createdUI;
    public bool _playAnimation;
    private float _timeoffset = 0;
    private bool _inRange;
    private bool _onDestroy;

    [SerializeField] private LayerMask Everything;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerGameObject;
        _canvasUIGameObject = _referenceInterface._UICanvas;

        _canvasUI = _canvasUIGameObject.GetComponent<Canvas>();
        _GrappinUI = _referenceInterface.HUDGrappin;
        S_PauseMenuV2 = _referenceInterface.EventSystem.GetComponent<S_PauseMenuV2>();

        _s_GrappinV2 = _referenceInterface._playerGameObject.GetComponent<S_GrappinV2>();
    }


    void Start()
    {
        _mainCamera = Camera.main;
        _createdUI = false;
    }

    void Update()
    {
        CheckWalls();

        var bounds = _collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(_mainCamera);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds) && _inRange == true && !S_PauseMenuV2._isPaused && !S_PauseMenuV2._IsRestart && _seePlayer)
        {
            CreateUI();
            _UI.GetComponent<S_Follow_UI>().ObjectToFollow = lookat;
            _timeoffset = 0;
            StartCoroutine(setHudActive(_UI));

        }
        else
        {
            StartCoroutine(PlayAnimationClose());            
        }

        if (S_PauseMenuV2._IsRestart)
        {
            DestroyUIGrappin();
        }
    }

    IEnumerator PlayAnimationClose()
    {
        _onDestroy = true;
        _playAnimation = false;
        yield return new WaitForSeconds(0.25f);
        _createdUI = false;
        DestroyUIGrappin();
    }



    void CreateUI()
    {
        if (_createdUI == false)
        {
            _UI = Instantiate(_HUDGrappin, _GrappinUI.transform);
            _UI.GetComponent<S_Follow_UI>()._GOGrappin = _Self;
            _createdUI = true;
            _playAnimation = true;
        }
    }

    void CheckWalls()
    {
        //var ray = new Ray(_mainCamera.transform.position, gameObject.transform.position);
        var ray = new Ray(gameObject.transform.position, (_mainCamera.transform.position - gameObject.transform.position));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,_s_GrappinV2._maxGrappleDistance, Everything))
        {
            Debug.Log(hit.collider);

            int _whatIsPlayer = LayerMask.NameToLayer("WhatIsPlayer");
            
            if (hit.collider.gameObject.layer != _whatIsPlayer)
            {
                _seePlayer = false;
            }
            else
            {
                _seePlayer = true;
            }
        }
    }

    public void InTrigger()
    {
        _inRange = true;
    }

    public void OutTrigger()
    {
        _inRange = false;
    }

    IEnumerator setHudActive(GameObject UI)
    {
        while (_timeoffset < 2 && _createdUI == true)
        {
            if(_timeoffset < 1)
            {
                _timeoffset += 1;
                yield return new WaitForSeconds(0.001f);
            }
            else
            {
                _timeoffset += 1;
                UI.GetComponent<Image>().enabled = true;
                yield return new WaitForSeconds(0.001f);
            }
            
        }
    }
    public void DestroyUIGrappin()
    {
        _createdUI = false;
        _playAnimation = false;
        Destroy(_UI);
    }

}




/*RaycastHit hit;
 
if(Vector3.Distance(transform.position, player.position) < maxRange )
{
    if(Physics.Raycast(transform.position, (player.position - transform.position), out hit, maxRange))
    {
        if(hit.transform == player)
        {
            // In Range and i can see you!
        }
    }
}*/