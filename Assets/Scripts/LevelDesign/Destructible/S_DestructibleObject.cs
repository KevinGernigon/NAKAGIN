using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_DestructibleObject : MonoBehaviour
{

    [Header("Reference")]
    private GameObject Player;
    private S_ReferenceInterface ReferenceInterface;
    private S_PlayerMovement PlayerMovement;
    private S_PlayerSound PlayerSoundScript;
    private GameObject Camera;

    private void Awake()
    {
        ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        Player = ReferenceInterface._playerGameObject;
        PlayerMovement = Player.GetComponent<S_PlayerMovement>();
        Camera = ReferenceInterface._CameraGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
        //SceneManager dans Respawn
        //Recup player death dans gestion scene
        ResetWall();

        if (PlayerMovement._isDashing)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
        else
            GetComponent<BoxCollider>().isTrigger = false;
    }

    public void ResetWall()
    {
            GetComponent<Renderer>().enabled = true;
    }
}
