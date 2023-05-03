using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Follow_UI : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private S_ObjectOnCamera ScriptOnCamera;
    public S_ObjectOnCamera _GOGrappin;
    public Transform ObjectToFollow;
    private Camera main_camera;
    [SerializeField] private Animator _anim;

    void Start()
    {
        main_camera = Camera.main;
        ObjectToFollow = ScriptOnCamera.lookat;
        Vector3 pos = main_camera.WorldToScreenPoint(ObjectToFollow.position + offset);
    }

    void Update()
    {
        Vector3 pos = main_camera.WorldToScreenPoint(ObjectToFollow.position + offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }

        if(_GOGrappin._playAnimation)
        {
            //_anim.SetBool("IsOpen", true);
            _anim.Play("A_RotationGrappin");
            
        }
        if (!_GOGrappin._playAnimation)
        {
            //_anim.SetBool("IsOpen", false);      
            _anim.Play("A_RotationGrappinClose");
        }
    }
}
