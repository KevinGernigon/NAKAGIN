using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameObject Platform;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Cinemachine.CinemachineDollyCart CinemachineDC;
    [SerializeField] private Cinemachine.CinemachineSmoothPath CinemachineSP;

    private bool _isTrue;
    private void Start()
    {
        _isTrue = true;
        StartCoroutine(ConveyorCraft());
    }

    /* IEnumerator ConveyorCraft()
    {
        while (_isTrue)
        {
            for(int i = 0; i < CinemachineSP.PathLength; i++)
            {
                var DollyCart = Instantiate(Platform, StartPos);
                DollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position += i;
                yield return new WaitForSeconds(0.01f);
            }

            _isTrue = false;
        }

    } */

    IEnumerator ConveyorCraft()
    {
        while (_isTrue){
            for (int i = 0; i < 20; i++)
            {
                var DollyCart = Instantiate(Platform, StartPos);
                DollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position += i;
                yield return new WaitForSeconds(0.2f);
                if(DollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position >= CinemachineSP.PathLength){
                    Destroy(this);
                    DollyCart.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
                }
            }
        }
    }
}
