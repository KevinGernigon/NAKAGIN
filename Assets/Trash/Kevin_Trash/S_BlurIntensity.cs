using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_BlurIntensity : MonoBehaviour
{
    [SerializeField]
    private S_PlayerMovement _playerMovement;
    [SerializeField]
    private Material _shaderMat;

    // Update is called once per frame
    void Update()
    {
        _shaderMat.SetFloat("_Blur_CenterMaskEdge", -_playerMovement._moveSpeed * 3 + 20);
    }
}
