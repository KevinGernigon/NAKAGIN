using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class S_ChromaticAbberation : MonoBehaviour
{
    VolumeProfile profile;

    ChromaticAberration myChromaticAberration;


    [SerializeField]
    private S_PlayerMovement _playerMovement;

    private void Start()
    {
        profile = GameObject.Find("PostProcessVolume_01").GetComponent<Volume>().profile;
        profile.TryGet(out myChromaticAberration);
    }
    private void Update()
    {
        myChromaticAberration.intensity.Override(_playerMovement._moveSpeed / 100);
    }
}
