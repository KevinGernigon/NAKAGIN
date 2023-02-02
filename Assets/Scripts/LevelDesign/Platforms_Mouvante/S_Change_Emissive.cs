using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Change_Emissive : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> _lumiere;
    private Material[]_materials;

    [SerializeField]
    private GameObject _player;

    private void Start()
    {
        _player.GetComponent<S_Bras_Animation>();
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
