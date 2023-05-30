using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_DeathPlayer : MonoBehaviour
{
    public bool playerIsDead = false;
    public int DeadCount;

    [SerializeField] private Rigidbody _rbplayer;
    [SerializeField] private S_PlayerCam _playerCam;
    [SerializeField] private TMP_Text _robotName;
    [SerializeField] private S_Dash _Dash;
    [SerializeField] private S_GrappinV2 _GrappinV2;

    public float TimeToRespawnPlayer = 2f;

    public void RespawnPlayer(Transform RespawnOrientation)
    {
        DeadCount++;
        NewRobotsName();

        _Dash._limitDash = 3f;
        _GrappinV2._isGrappling = false;

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = RespawnOrientation.rotation.eulerAngles.x;
        var y = RespawnOrientation.rotation.eulerAngles.y;
        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }

    public void NewRobotsName()
    {
        if(DeadCount < 10)
            _robotName.text = "00" + DeadCount;
        else if(DeadCount < 100)
            _robotName.text = "0" + DeadCount;
        else
            _robotName.text = "" + DeadCount;
    }

}
