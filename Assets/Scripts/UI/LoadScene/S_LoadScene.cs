using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class S_LoadScene : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_GestionnaireScene _GestionnaireScene;

    [SerializeField] private int _sceneId;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _GestionnaireScene = _referenceInterface._GestionnaireScene;
    }


    private void OnTriggerEnter(Collider other)
    {
       _GestionnaireScene.LoadNewScene(_sceneId); 
    }

}
