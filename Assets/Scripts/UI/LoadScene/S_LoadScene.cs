using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class S_LoadScene : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_GestionnaireScene _GestionnaireScene;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _GestionnaireScene = _referenceInterface._GestionnaireScene;
    }

    public void LoadScene(int _sceneId)
    {
        _GestionnaireScene.LoadNewScene(_sceneId);
    }


  

}
