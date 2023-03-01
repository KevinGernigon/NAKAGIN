using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_OutlineAlpha : MonoBehaviour
{
    [SerializeField]
    private Material _outlineMaterial;
    [SerializeField]
    private Transform _cameraHolder;

    private void Update()
    {
        _outlineMaterial.SetVector("_CameraHolder", _cameraHolder.position);
    }
}
