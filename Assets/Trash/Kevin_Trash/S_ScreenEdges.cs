using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ScreenEdges : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _pivotModules;
    private Transform _storeTransform;

    private S_ReferenceInterface _referenceInterface;
    private Transform _playerTransform = null;

    [SerializeField]
    private Material _shaderMat;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerTransform = _referenceInterface._playerTransform;
    }

    void Start()
    {
        AddPivotToList();
        StartCoroutine(CheckPlayerDistance());
    }

    public void AddPivotToList()
    {
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("Pivot"))
        {
            _pivotModules.Add(gameObj.transform);
        }
    }


    IEnumerator CheckPlayerDistance()
    {
        while (true)
        {
            _storeTransform = _pivotModules[0];
            for (int i = 1; i < _pivotModules.Count; i++)
            {
                if(Vector3.Distance(_playerTransform.position, _pivotModules[i].position) < Vector3.Distance(_playerTransform.position, _storeTransform.position))
                {
                    _storeTransform = _pivotModules[i];
                }
            }

            if(Vector3.Distance(_playerTransform.position, _storeTransform.position) < 30.0f)
            {
                _shaderMat.SetInt("_Activate_FBP", 1);
                _shaderMat.SetFloat("_FBP_CenterMaskEdge", 0.12f + Vector3.Distance(_playerTransform.position, _storeTransform.position) / 300);
            }
            else
            {
                _shaderMat.SetInt("_Activate_FBP", 0);
            }



            yield return new WaitForSeconds(0.01f);
        }
    }
}
