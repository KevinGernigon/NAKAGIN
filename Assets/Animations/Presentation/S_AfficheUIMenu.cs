using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AfficheUIMenu : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private S_GestionnaireScene _GestionnaireScene;
    [SerializeField] private GameObject _UIMenu;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _GestionnaireScene = _referenceInterface._GestionnaireScene;
    }

    private void Start()
    {
        if(!_GestionnaireScene.DisIntroGame)
        {
            _UIMenu.SetActive(false);
        }
    }

    public void AfficheUIMenu()
    {
        _UIMenu.SetActive(true);
        _GestionnaireScene.DisalbleIntro();
    }
}
