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
    
    [SerializeField] private MeshFilter DestructibleWall;
    [SerializeField] private Mesh DestructedWall;
    [SerializeField] private Mesh OriginalDestructibleWall;

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
            DestructibleWall.mesh = DestructedWall;
            GetComponent<MeshCollider>().enabled = false;
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
            GetComponent<MeshCollider>().isTrigger = true;
        }
        else{
            GetComponent<MeshCollider>().enabled = true;
            GetComponent<MeshCollider>().isTrigger = false;
            FixTriggerConvexBug();
            
        }
    }

    private void FixTriggerConvexBug(){
        if(_isDisable){
            GetComponent<MeshCollider>().sharedMesh = DestructedWall;       
            GetComponent<MeshCollider>().convex = false; 
        }
    }

    public void ResetWall()
    {
        DestructibleWall.mesh = OriginalDestructibleWall;
        GetComponent<MeshCollider>().sharedMesh = DestructibleWall.mesh;       
        GetComponent<MeshCollider>().convex = true; 
        _isDisable = false;
    }
}
