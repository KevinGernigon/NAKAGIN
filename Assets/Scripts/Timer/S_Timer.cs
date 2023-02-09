using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_Timer : MonoBehaviour
{
    private S_ReferenceInterface _referenceInterface;
    private GameObject _TimerAffichage;
    private TMP_Text _textTimer;

    private float _timeReflevel1 = 0f;
    private float _timeReflevel2 = 0f;


    public float _hours, _minutes, _seconds, _milliseconds;
    public float _timerTime;
    
    public float _startTime, _stopTime;

    private bool _timerPlay = false;



    private void Awake()
    {
         _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
         _textTimer = _referenceInterface._timerText;

    }



    private void Start()
    {
        _timerTime = Time.time;

    }

    private void Update()
    {

        _timerTime = _stopTime + (Time.time) -(_startTime);

        _hours = (int)(_timerTime / 3600f);
        _minutes = (int)(_timerTime / 60f) % 60;
        _seconds = (int)(_timerTime % 60f);
        _milliseconds = (int)(_timerTime * 1000f) % 1000;


        if (_timerPlay) // affichage 
        {
            if (_minutes < 10)// ajoute un 0 devant les 10 premiere min
            {
                if (_seconds < 10)// ajoute un 0 devant les 10 premiere sec
                {

                    if (_milliseconds < 100)

                        _textTimer.text = "0" + _minutes + ":" + "0" + _seconds + ":" + "0" + _milliseconds;

                    else
                        _textTimer.text = "0" + _minutes + ":" + "0" + _seconds + ":" + _milliseconds;
                }
                else
                {
                    if (_milliseconds < 100)

                        _textTimer.text = "0" + _minutes + ":" + _seconds + ":" + "0" + _milliseconds;

                    else
                        _textTimer.text = "0" + _minutes + ":" + _seconds + ":" + _milliseconds;
                }
            }
            else
            {
                if (_seconds < 10)
                {
                    if (_milliseconds < 100)
                        _textTimer.text = _minutes + ":" + "0" + _seconds + ":" + "0" + _milliseconds;
                    else
                        _textTimer.text = _minutes + ":" + "0" + _seconds + ":" + _milliseconds;
                }
                else
                {
                    if (_milliseconds < 100)
                        _textTimer.text = _minutes + ":" + _seconds + ":" + "0" + _milliseconds;
                    else
                        _textTimer.text = _minutes + ":" + _seconds + ":" + _milliseconds;

                }

            }
        }
    }


    public void TimerStart()
    {
        if (!_timerPlay)
        {

            
            _timerPlay = true;
            _startTime = Time.time;

        }
    }

    public void TimerStop()
    {
        if (_timerPlay)
        {
            
            _timerPlay = false;
            _stopTime = _timerTime;

            //Debug.Log("TimerBattle : " + _hours + "h : " + _minutes + "min : " + _seconds + "Sec :  " + _milliseconds + "mill");

        }

    }
    public void TimerReset()
    {
        
        _timerPlay = false;
        _stopTime = _timerTime;

        _textTimer.text = "";
       
        _startTime = Time.time;
        _stopTime = 0f;

    }

    public void SetTimerRef(float TimerLevel1, float TimerLevel2)
    {

        _timeReflevel1 = TimerLevel1;
        _timeReflevel2 = TimerLevel2;

        
    }

}

