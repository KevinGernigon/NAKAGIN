using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ContinueButton : MonoBehaviour
{

    private S_PlayFabManager PlayFabManager;
    private S_ReferenceInterface ReferenceInterface;


    private void Awake() {
        ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        PlayFabManager = ReferenceInterface.PlayFabManager;
    }

    public void OnClickContinue(){
        PlayFabManager.Continue();
    }
}
