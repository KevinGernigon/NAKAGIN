using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestructibleObject : MonoBehaviour
{

    [Header("Reference")]
    private GameObject Player;
    private S_ReferenceInterface ReferenceInterface;
    private S_PlayerMovement PlayerMovement;
    private GameObject Camera;

    private void Awake()
    {
        ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        Player = ReferenceInterface._playerGameObject;
        PlayerMovement = Player.GetComponent<S_PlayerMovement>();
        Camera = ReferenceInterface._CameraGameObject;

    }

    private void Update()
    {
        //SceneManager dans Respawn
        //Recup player death dans gestion scene
        ResetWall();

        RaycastHit hit;
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, 3f, 1 << LayerMask.NameToLayer("WhatIsDestructible")) && PlayerMovement._isDashing)
        {
            //Destroy(hit.collider.gameObject);
            hit.collider.GetComponent<Renderer>().enabled = false;
            hit.collider.GetComponent<BoxCollider>().enabled = false;
            
        }
    }

    public void ResetWall()
    {
        /*if (_isDead)
        {

        }*/
        if (Input.GetKeyDown(KeyCode.M)){

            Debug.Log("?");
            GetComponent<Renderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
