using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private Transform _nakagin;

    private void Update()
    {
     
      
       Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycasthit, float.MaxValue,LayerMask))
        {
            transform.position = raycasthit.point;
        }

        _nakagin.LookAt(transform.position);

    }
}
