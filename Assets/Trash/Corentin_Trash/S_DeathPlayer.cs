using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_DeathPlayer : MonoBehaviour
{
    public bool playerIsDead = false;
    public int DeadCount = 0;

    [SerializeField] private Rigidbody _rbplayer;
    [SerializeField] private S_PlayerCam _playerCam;
    [SerializeField] private TMP_Text _robotName;

    public float TimeToRespawnPlayer = 2f;

    public void RespawnPlayer()
    {
        DeadCount += 1;
        NewRobotsName();

        _rbplayer.velocity = new Vector3(0, 0, 0);

        //Player Camera rest
        var x = this.transform.rotation.eulerAngles.x;
        var y = this.transform.rotation.eulerAngles.y;
        _playerCam.CameraReset(x, y);
        Physics.SyncTransforms();
    }

    private void NewRobotsName()
    {
        if(DeadCount < 10)
            _robotName.text = "_00" + DeadCount;
        else if(DeadCount < 100)
            _robotName.text = "_0" + DeadCount;
        else
            _robotName.text = "_" + DeadCount;
    }

}
