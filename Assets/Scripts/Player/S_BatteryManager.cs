using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class S_BatteryManager : MonoBehaviour
{


    [Header("Battery")]
    public float _nbrBattery;
    public float _nbrMaxBattery = 3 ;
    [SerializeField] private float _nbrMinBattery = 0;
    [SerializeField] private float _nbrBatteryFind = 0;

     [Header("UI")]
    public Image Battery_1;
    public Image Battery_2;
    public Image Battery_3;
    public Sprite Empty_Battery;
    public Sprite Full_Battery;


    private void Update()
    {
        if (_nbrBattery == 0)
        {
            Battery_1.sprite = Empty_Battery;
            Battery_2.sprite = Empty_Battery;
            Battery_3.sprite = Empty_Battery;
        }
        else if (_nbrBattery == 1)
        {
            Battery_1.sprite = Full_Battery;
            Battery_2.sprite = Empty_Battery;
            Battery_3.sprite = Empty_Battery;
        }
        else if (_nbrBattery == 2)
        {
            Battery_1.sprite = Full_Battery;
            Battery_2.sprite = Full_Battery;
            Battery_3.sprite = Empty_Battery;
        }
        else if (_nbrBattery == 3)
        {
            Battery_1.sprite = Full_Battery;
            Battery_2.sprite = Full_Battery;
            Battery_3.sprite = Full_Battery;
        }

        
    }

    public void UseOneBattery()
    {
        if( _nbrBattery >= 1)
        {
            _nbrBattery -= 1;
        }

    }

    public void UseTwoBattery()
    {
        if (_nbrBattery >= 2)
        {
            _nbrBattery -= 2;
        }

    }

    public void UseThreeBattery()
    {
        if (_nbrBattery >= 3)
        {
            _nbrBattery -= 3;
        }

    }

    public void UseAllBattery()
    {
        if (_nbrBattery >= _nbrMaxBattery)
        {
            _nbrBattery = _nbrMinBattery;
        }
        else
        {
            //UI si batterie vide 
        }

    }

    public void GetOneBattery()
    {
        _nbrBatteryFind = 1;
        if ((_nbrBattery < _nbrMaxBattery) && (_nbrBatteryFind + _nbrBattery) <= _nbrMaxBattery )
        {
            _nbrBattery = _nbrBattery + _nbrBatteryFind;

            if ( _nbrBattery  > _nbrMaxBattery )
            {
                _nbrBattery = _nbrMaxBattery;
            }
        }
  
    }
    public void GetTwoBattery()
    {
        _nbrBatteryFind = 2;
        if (_nbrBattery < _nbrMaxBattery && (_nbrBatteryFind + _nbrBattery) <= _nbrMaxBattery)
        {
            _nbrBattery = _nbrBattery + _nbrBatteryFind;

            if (_nbrBattery > _nbrMaxBattery)
            {
                _nbrBattery = _nbrMaxBattery;

            }
        }
 
    }

    public void GetThreeBattery()
    {
        _nbrBatteryFind = 3;
        if (_nbrBattery < _nbrMaxBattery && (_nbrBatteryFind + _nbrBattery) <= _nbrMaxBattery)
        {
            _nbrBattery = _nbrBattery + _nbrBatteryFind;
            if (_nbrBattery + _nbrBatteryFind > _nbrMaxBattery)
            {
                _nbrBattery = _nbrMaxBattery;

            }
        }
       

    }

}
