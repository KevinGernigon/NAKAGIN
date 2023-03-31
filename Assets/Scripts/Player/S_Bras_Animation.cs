using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bras_Animation : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private S_PlayerMovement _Pm;

    private bool _isAnimated = false;

    [SerializeField] private GameObject _Arms;
    private Animator arms_AC;
    [SerializeField] private GameObject _arms_Mesh;

    private void Awake()
    {
        arms_AC = _Arms.GetComponent<Animator>();
        _arms_Mesh.SetActive(false);
    }

    private void Start()
    {
        _Pm = _player.GetComponent<S_PlayerMovement>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isAnimated == false)
        {
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Left_Arm_Plateforme_0" + random.ToString(), 0, 0.0f);
        }
        else if (Input.GetMouseButtonDown(1) && _isAnimated == false)
        {
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Right_Arm_Plateforme_0" + random.ToString(), 0, 0.0f);
        }
        else if(_Pm.state == S_PlayerMovement.MovementState.walking)
        {

        }

        else if (arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && _Pm.state != S_PlayerMovement.MovementState.walking)
        {
            _isAnimated = false;
        }
        

    }
}
