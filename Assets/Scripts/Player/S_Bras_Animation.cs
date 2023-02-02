using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bras_Animation : MonoBehaviour
{
    private bool _isAnimated = false;

    [SerializeField]
    private GameObject _Arms;

    private Animator arms_AC;

    [SerializeField]
    private GameObject _arms_Mesh;

    private void Awake()
    {
        arms_AC = _Arms.GetComponent<Animator>();
        _arms_Mesh.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isAnimated == false)
        {
            _arms_Mesh.SetActive(true);
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Arms_Left_0" + random.ToString(), 0, 0.0f);
        }
        else if (Input.GetMouseButtonDown(1) && _isAnimated == false)
        {
            _arms_Mesh.SetActive(true);
            int random = Random.Range(1, 3);
            _isAnimated = true;
            arms_AC.Play("A_Arms_Right_0" + random.ToString(), 0, 0.0f);
        }
        else if (arms_AC.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f)
        {
            _arms_Mesh.SetActive(false);
            _isAnimated = false;
        }
    }
}
