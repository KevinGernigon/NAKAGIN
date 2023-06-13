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
    [SerializeField] private GameObject _buttonselect;


    private S_ReferenceInterface _referenceInterface;
    private S_InputManager _inputManager;
    private GameObject _selectedGameObject;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _inputManager = _referenceInterface._InputManager;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(_buttonselect);

    }

    private void Update()
    {
        if(_inputManager._playerInput.currentControlScheme == "Gamepad")
        {

            if(EventSystem.current.currentSelectedGameObject != null)
            {
                _selectedGameObject = EventSystem.current.currentSelectedGameObject;

                //_nakagin.LookAt(_selectedGameObject.transform.position);
            }
        }

        if (_inputManager._playerInput.currentControlScheme == "KeyboardAndMouse")
        {
            _target.transform.SetParent(gameObject.transform);
            _target.transform.position = Vector3.zero;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue, LayerMask))
            {
                _target.transform.position = raycasthit.point;
            }

            _nakagin.LookAt(_target.transform.position); 
        }







        /*
        gameObject.transform.SetParent(gameObject.transform);
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue,LayerMask))
        {
               gameObject.transform.position = raycasthit.point;
        }
         _nakagin.LookAt(transform.position);*/

    }
}
