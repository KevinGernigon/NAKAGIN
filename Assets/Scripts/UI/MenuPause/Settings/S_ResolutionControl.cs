using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] _resolutions;
    private List<Resolution> _filteredResolutions;

    private float _currentRefreshRate;
    private int _currentResolutionIndex = 0;

    [SerializeField] private bool _isFullScreen = true;

    private void Start()
    {

        _resolutions = Screen.resolutions;
        _filteredResolutions = new List<Resolution>();

        _resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

       
        //Debug.Log("RefreshRate : " + _currentRefreshRate);

        for (int i = 0; i < _resolutions.Length; i++)
        {
            //Debug.Log("Resolution : " + _resolutions[i]);
            if (_resolutions[i].refreshRate == _currentRefreshRate)
            {
                _filteredResolutions.Add(_resolutions[i]);
            }
        }

        List<string> option = new List<string>();
        for(int i = 0; i < _filteredResolutions.Count; i++)
        {
            string resolutionOption = _filteredResolutions[i].width + "x" + _filteredResolutions[i].height /* + " " + _filteredResolutions[i] + "Hz"*/;

            option.Add(resolutionOption);
            
            if (_filteredResolutions[i].width == Screen.width && _filteredResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(option);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;
        //Screen.SetResolution(1920, 1080, Screen.fullScreen);

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
        
        //Screen.fullScreen = _isFullScreen;
        
    }

    public void SetFullscrenn(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;

        _isFullScreen = IsFullScreen;
   
    }
 
}
