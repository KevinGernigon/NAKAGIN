using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_MoveCamera : MonoBehaviour
{
    public Transform _cameraPosition;
    public S_PlayerMovement pm;
 // Update is called once per frame
    void Update()
    {
        transform.position = _cameraPosition.position;
        cameraPosSliding();
    }

    private void cameraPosSliding()
    {
        if (pm._isSliding)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z) ;
        }
    }
}
