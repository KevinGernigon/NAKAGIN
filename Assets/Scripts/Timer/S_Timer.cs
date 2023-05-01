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

    private Animator _animaHUDTimer;
    private Animator _HUDTimerOpen;
    private bool _HUDTimerIsClose = true;

    private float _timeReflevel1 = 0f;
    private float _timeReflevel2 = 0f;

    public float _hours, _minutes, _seconds, _milliseconds;
    public float _timerTime;
    
    public float _startTime, _stopTime;
    public bool _timerPlay = false;

    [SerializeField] private Color _warningcolor = Color.red;
    private Color _saveColor;

    private bool DebuggerStartStop;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _textTimer = _referenceInterface._timerText;
        _animaHUDTimer = _referenceInterface._HUDtimer;
        _saveColor = _textTimer.color;
        _HUDTimerOpen = _referenceInterface._HUDTimerFont;
    }



    private void Start()
    {
        _timerTime = Time.time;

        S_Debugger.UpdatableLog("Timer ", "", Color.white);
        S_Debugger.AddButton("Start / Stop", DebuggerTimer);
    }

    private void DebuggerTimer()
    {
        if (DebuggerStartStop)
        {
            S_Debugger.UpdatableLog("Timer ", "Start", Color.white);
            TimerStart();
            DebuggerStartStop = false;
        }
        else
        {
            S_Debugger.UpdatableLog("Timer ", "Stop",Color.cyan);
            TimerStop();
            DebuggerStartStop = true;
        }

    }


    private void Update()
    {

        if (_timerPlay) // affichage 
        {
             _timerTime = _stopTime + (Time.time) -(_startTime);

            _hours = (int)(_timerTime / 3600f);
            _minutes = (int)(_timerTime / 60f) % 60;
            _seconds = (int)(_timerTime % 60f);
            _milliseconds = (int)(_timerTime * 1000f) % 1000;


       
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


    public void WarningTimer()
    {
        _animaHUDTimer.Play("A_Timer");
        _textTimer.color = _warningcolor;
    }



    public void TimerStart()
    {
        if (!_timerPlay)
        {
           if(_HUDTimerIsClose)
           {
                
                _HUDTimerOpen.Play("A_TimerOpen");
                _HUDTimerIsClose = false;
           }

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
        if (!_HUDTimerIsClose)
        {

            _HUDTimerOpen.Play("A_TimerClose");
            _HUDTimerIsClose = true;
        }

        _timerPlay = false;
        _timerTime = 0f;

        _textTimer.text = "";
        _textTimer.color = _saveColor;

        _animaHUDTimer.Rebind();

        _startTime = Time.time;
        _stopTime = 0f;

    }

    public void SetTimerRef(float TimerLevel1, float TimerLevel2)
    {

        _timeReflevel1 = TimerLevel1;
        _timeReflevel2 = TimerLevel2;

        
    }

}

