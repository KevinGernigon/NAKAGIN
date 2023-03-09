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

    private GameObject _eventSystem;
    private S_PauseMenuV2 S_PauseMenuV2;

    private GameObject _playerContent;
    private GameObject _canvasUIGameObject;

    private Canvas _canvasUI;
    private GameObject _GrappinUI;

    [SerializeField] public Transform lookat;

    Plane[] cameraFrustum;
    private bool _seePlayer;
    private GameObject _UI;
    private Camera _mainCamera;
    private bool _createdUI;
    private float _timeoffset = 0;
    private bool _inRange;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerGameObject;
        _canvasUIGameObject = _referenceInterface._UICanvas;

        _canvasUI = _canvasUIGameObject.GetComponent<Canvas>();
        _GrappinUI = _referenceInterface._UIStartHUD;
        S_PauseMenuV2 = _referenceInterface.EventSystem.GetComponent<S_PauseMenuV2>();
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
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds) && _inRange == true && !S_PauseMenuV2._isPaused && !S_PauseMenuV2._IsRestart)
        {
            CreateUI();
            _UI.GetComponent<S_Follow_UI>().ObjectToFollow = lookat;
            _timeoffset = 0;
            StartCoroutine(setHudActive(_UI));

        }
        else
        {
            Destroy(_UI);
            _createdUI = false;
        }

        if (S_PauseMenuV2._IsRestart)
            DestroyUIGrappin();
    }

    void CreateUI()
    {
        if (_createdUI == false)
        {
            _UI = Instantiate(_HUDGrappin, _GrappinUI.transform);
            _createdUI = true;
        }
    }

    void CheckWalls()
    {
        var ray = new Ray(_playerContent.transform.position, _collider.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != _collider)
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
        Destroy(_UI);
        _createdUI = false;
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