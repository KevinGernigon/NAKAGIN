using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Follow_UI : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private S_ObjectOnCamera ScriptOnCamera;
    public Transform ObjectToFollow;
    private Camera main_camera;

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
    }
}
