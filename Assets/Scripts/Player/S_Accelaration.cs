using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Accelaration : MonoBehaviour
{
    private S_PlayerMovement PlayerMovement;
    private S_PauseMenuV2 PauseMenu;
    private S_ReferenceInterface _referenceInterface;

    private void Start()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        PauseMenu = _referenceInterface.EventSystem.GetComponent<S_PauseMenuV2>();
        PlayerMovement = GetComponent<S_PlayerMovement>();
    }
    public void VarianceVitesse()
    {
        if (PlayerMovement._isAccelerating)
        {
            StopAllCoroutines();
            StartCoroutine(acceleration());
        }
        else if (PlayerMovement._isDecelerating)
        {
            StopAllCoroutines();
            StartCoroutine(deceleration());
        }


    }
    IEnumerator acceleration()
    {
        var compteur = 0;
        while (PlayerMovement._walkSpeed < 45)
        {
            compteur += 1;
            Debug.Log("Accelerating");
            PlayerMovement._isAccelerating = true;
             
            PlayerMovement._walkSpeed += 0.45f * compteur;
            if (PlayerMovement._walkSpeed > 45) PlayerMovement._walkSpeed = 45;
           yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator deceleration()
    {
        var compteur = 0;
        while (PlayerMovement._walkSpeed > 10 && !PauseMenu._isPaused)
        {
            compteur += 1;
            Debug.Log("Decelerating");
            PlayerMovement._isDecelerating = false;
             
            PlayerMovement._walkSpeed -= 0.45f * compteur;
            if (PlayerMovement._walkSpeed < 10) PlayerMovement._walkSpeed = 10;
           yield return new WaitForSeconds(0.05f);
        }
    }

}
