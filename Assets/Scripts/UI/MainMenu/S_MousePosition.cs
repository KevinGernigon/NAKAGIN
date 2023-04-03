using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Transform _nakagin;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _NewGameGameObject;


    private S_ReferenceInterface _referenceInterface;
    private S_InputManager _InputManager;
    private GameObject _selectedGameObject;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _InputManager = _referenceInterface._InputManager;


    }
    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_NewGameGameObject);
        
    }
    private void Update()
    {
        if (_InputManager._playerInput.currentControlScheme == "Gamepad")
        {
            _target.transform.position = Vector3.zero;

           if (EventSystem.current.currentSelectedGameObject != null)
           {
                _selectedGameObject = EventSystem.current.currentSelectedGameObject;
                _target.transform.SetParent(_selectedGameObject.transform);

                Ray ray = _mainCamera.ScreenPointToRay(_selectedGameObject.transform.position);
                if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, LayerMask))
                {
                    _target.transform.position = raycasthit.point;
                }
           }
        }

        if (_InputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
        {
            _target.transform.SetParent(gameObject.transform);
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue,LayerMask))
            {
                _target.transform.position = raycasthit.point;
            }
        }

        _nakagin.LookAt(transform.position);

    }
}
