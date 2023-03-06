using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ModuleManager : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _listPivotModules;
    private List<Vector3> _listRotationPivots = new List<Vector3>();

    void Start()
    {
        for (int i = 0; i < _listPivotModules.Count; i++)
        {
            //Debug.Log(_listPivotModules[i].transform.localEulerAngles);
            _listRotationPivots.Add(_listPivotModules[i].transform.localEulerAngles);
        }
    }

    public void ResetPlatformRotation()
    {
        for (int i = 0; i < _listPivotModules.Count; i++)
        {
            _listPivotModules[i].transform.localEulerAngles = _listRotationPivots[i];
        }
    }
}