using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Drones : MonoBehaviour
{
    [SerializeField] private GameObject OriginalDrone;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Cinemachine.CinemachineDollyCart CinemachineDC;
    [SerializeField] private Cinemachine.CinemachineSmoothPath CinemachineSP;

    private Transform EndPos;
    private bool isSpawnable;
    private bool _isTrue;
    private void Start()
    {
        _isTrue = true;
        isSpawnable = true;
        //StartCoroutine(DronesCraft());
    }

    private void FixedUpdate() {
        CraftingDrone();
    }

    private void CraftingDrone(){
        if(isSpawnable)
        {
            var Drone = Instantiate(OriginalDrone, StartPos);
            Drone.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
            StartCoroutine(DronesCraft());
                Destroy(Drone, 5f);
        }
    }

    IEnumerator DronesCraft()
    {
                isSpawnable = false;
                yield return new WaitForSeconds(1f);
                isSpawnable = true;
    }


    /* IEnumerator ConveyorCraft()
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
    } */
}
