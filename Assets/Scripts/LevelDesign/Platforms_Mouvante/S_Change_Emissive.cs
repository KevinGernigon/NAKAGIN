using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Change_Emissive : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;

    [SerializeField]
    private List<GameObject> _lumiere;
    private Material[]_materials;

   
    private GameObject _playerContent;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerGameObject;
        _playerContent.GetComponent<S_Bras_Animation>();
    }


    private void Start()
    {
        
        for (int i = 0; i < _lumiere.Count; i++)
        {
            _materials = _lumiere[i].GetComponent<MeshRenderer>().materials;
            //_materials = _lumiere[i].GetComponent<Renderer>().materials;       
            _materials[1].SetColor("_BaseColor", new Color(255,0,0,1));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("TriggerEnter");

            for (int i = 0; i < _lumiere.Count; i++)
            {
                _materials = _lumiere[i].GetComponent<MeshRenderer>().materials;
                //_materials = _lumiere[i].GetComponent<Renderer>().materials;
                _materials[1].SetColor("_BaseColor", new Color(0, 0, 255, 1));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < _lumiere.Count; i++)
            {
                _materials = _lumiere[i].GetComponent<MeshRenderer>().materials;
                //_materials = _lumiere[i].GetComponent<Renderer>().materials;
                _materials[1].SetColor("_BaseColor", new Color(255, 0, 0, 1));
            }
        }
    }
}
