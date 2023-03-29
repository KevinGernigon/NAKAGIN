using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bras_Animation : MonoBehaviour
{
    private bool _isAnimated = false;

   
    [SerializeField] private GameObject _Arms;

    private Animator arms_AC;

    [SerializeField] private GameObject _arms_Mesh;


    [Header("InputManager")]
    [SerializeField] private S_InputManager S_InputManager;

    private void Awake()
    {
        arms_AC = _Arms.GetComponent<Animator>();
        _arms_Mesh.SetActive(false);



    }
    void Update()
    {
        
        if (S_InputManager._playerInputAction.Player.MoveModuleLeft.triggered && _isAnimated == false)
        {
            _arms_Mesh.SetActive(true);
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Left_Arm_Plateforme_0" + random.ToString(), 0, 0.0f);
        }
        else if (S_InputManager._playerInputAction.Player.MoveModuleRight.triggered && _isAnimated == false)
        {
            _arms_Mesh.SetActive(true);
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Right_Arm_Plateforme_0" + random.ToString(), 0, 0.0f);
        }
        else if (arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            _arms_Mesh.SetActive(false);
            _isAnimated = false;
        }
    }
}
