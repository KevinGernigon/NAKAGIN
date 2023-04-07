using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject PlatformWBox;
    [SerializeField] private Transform StartPos;
    [SerializeField] private Cinemachine.CinemachineDollyCart CinemachineDC;
    [SerializeField] private Cinemachine.CinemachineSmoothPath CinemachineSP;

    public float TimeToDestroy; 

    private bool _isTrue;
    private bool isSpawnable;
    private bool _isBoxSpawnable;

    private void Start()
    {
        _isTrue = true;
        isSpawnable = true;
        _isBoxSpawnable = true;
    }

    private void FixedUpdate(){
        CraftConveyor();
    }

    private void CraftConveyor(){
        if (isSpawnable){
            var randomNumber = Random.Range(1,6);
            if(randomNumber <= 4){
                var PlatformVar = Instantiate(Platform, StartPos);
                PlatformVar.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
                StartCoroutine(ConveyorCraft());
                    Destroy(PlatformVar, 10f);
            }
            else{
                if(_isBoxSpawnable){
                    var PlatformBoxVar = Instantiate(PlatformWBox, StartPos);
                    PlatformBoxVar.GetComponent<Cinemachine.CinemachineDollyCart>().m_Position = 0;
                    StartCoroutine(BoxCraft());
                        Destroy(PlatformBoxVar, 10f);    
                }
            }

    }

    IEnumerator ConveyorCraft(){
                isSpawnable = false;
                yield return new WaitForSeconds(0.25f);
                isSpawnable = true;
        }
    IEnumerator BoxCraft(){
        _isBoxSpawnable = false;
        yield return new WaitForSeconds(1.5f);
        _isBoxSpawnable = true;
    }    
    }
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
