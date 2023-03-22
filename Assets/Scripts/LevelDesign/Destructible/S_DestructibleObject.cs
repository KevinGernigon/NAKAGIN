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
    private S_DeathPlayer DeathPlayer;
    private GameObject Camera;
    private bool _isDisable = false;
      private void Awake()
    {
        ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        Player = ReferenceInterface._playerGameObject;
        DeathPlayer = ReferenceInterface.deathPlayer;


        PlayerMovement = Player.GetComponent<S_PlayerMovement>();
        Camera = ReferenceInterface._CameraGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Player")
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            _isDisable = true;
        }
    }

    private void Update()
    {
        if(DeathPlayer.playerIsDead && _isDisable)
        {
            ResetWall();
        }

        if (PlayerMovement._isDashing && !_isDisable)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
        else
            GetComponent<BoxCollider>().isTrigger = false;
    }





    public void ResetWall()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        _isDisable = false;
    }
}
