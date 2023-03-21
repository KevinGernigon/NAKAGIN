using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_InfoScoreValidation : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;

    [SerializeField]private S_Timer ScriptTimer;

    private Transform _playerContent;
    [SerializeField] private Transform _respawnplayer;

    public bool _runStart;

    [SerializeField] private GameObject _HUDInfoScore;
    [SerializeField] private TMP_Text _level1TimerTxt;

    [SerializeField] private GameObject _detectionRunBox;
    [SerializeField] private LayerMask _whatIsInformativeValidation;
    [SerializeField] private LayerMask Everything;
    public bool _isAnimPlaying = false;
    [SerializeField] private Animator _aniamHUDInfoRun;

    [SerializeField] private S_RunCheckPointManagerValidation S_RunCheckPointManagerValidation;

    public float _level1Time = 10f;
    private float _level2Time = 0f;
    private float _level1Timeminutes, _level1Timeseconds, _level1Timemilliseconds;
   

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
    }

    private void Start()
    {
        ShowTimerChallenge();
    }

    private void Update()
    {
       if( _runStart && (ScriptTimer._timerTime > _level1Time))
       {
            GetBestTimePlayer();
       }

        RaycastHit hit;
        if (Physics.Raycast(_referenceInterface._CameraGameObject.transform.position, _referenceInterface._CameraGameObject.transform.forward, out hit, 30, Everything))
        {
            int whatIsInformativeValidation = LayerMask.NameToLayer("WhatIsInformativeValidation");

            if (hit.collider.gameObject.layer == whatIsInformativeValidation && hit.collider.gameObject == _detectionRunBox)
            {
                ShowTimerChallenge();
                
            }
            if (hit.collider.gameObject.layer == whatIsInformativeValidation)
            {
                _HUDInfoScore.SetActive(true);

                if (_isAnimPlaying)
                {
                    _aniamHUDInfoRun.Rebind();
                    _aniamHUDInfoRun.Play("A_InfoScoreOpenValidation");
                    _isAnimPlaying = false;
                }
            }
            else if (!_isAnimPlaying)
            {
                _aniamHUDInfoRun.Rebind();
                _aniamHUDInfoRun.Play("A_InfoScoreClosing");
                _isAnimPlaying = true;
            }

        }
        else if (!_isAnimPlaying)
        {   
            _aniamHUDInfoRun.Rebind();
            _aniamHUDInfoRun.Play("A_InfoScoreClosing");
            _isAnimPlaying = true;
        }
    }


    private void ShowTimerChallenge()
    {

        _level1Timeminutes = (int)(_level1Time / 60f) % 60;
        _level1Timeseconds = (int)(_level1Time % 60f);
        _level1Timemilliseconds = (int)(_level1Time * 1000f) % 1000;

        //_level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;

        if (_level1Timeminutes < 10)// ajoute un 0 devant les 10 premiere min
        {
            if (_level1Timeseconds < 10)// ajoute un 0 devant les 10 premiere sec
            {

                if (_level1Timemilliseconds < 100)

                    _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + "0" + _level1Timemilliseconds;

                else
                    _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + _level1Timemilliseconds;
            }
            else
            {
                if (_level1Timeseconds < 100)

                    _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + "0" + _level1Timemilliseconds;

                else
                    _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + _level1Timemilliseconds;
            }
        }
        else
        {
            if (_level1Timeseconds < 10)
            {
                if (_level1Timemilliseconds < 100)
                    _level1TimerTxt.text = _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + "0" + _level1Timemilliseconds;
                else
                    _level1TimerTxt.text = _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + _level1Timemilliseconds;
            }
            else
            {
                if (_level1Timemilliseconds < 100)
                    _level1TimerTxt.text = _level1Timeminutes + ":" + _level1Timeseconds + ":" + "0" + _level1Timemilliseconds;
                else
                    _level1TimerTxt.text = _level1Timeminutes + ":" + _level1Timeseconds + ":" + _level1Timemilliseconds;

            }

        }
    }


   

    public void SendTimeChallengeToTimer()         //fonction lancé au passage de la trigger box start de la run 
    {

        ScriptTimer.SetTimerRef(_level1Time, _level2Time);
        _runStart = true;
    }

    public void GetBestTimePlayer()                //fonction lancé au passage de la trigger box stop de la run passage au hub suivant
    {
        if (_runStart)
        {
            _runStart = false;

            float timePlayer = ScriptTimer._timerTime;


            if (timePlayer < _level1Time)
            {
                    _level1TimerTxt.color = Color.green;
            }
            else
            {
                _runStart = false;

                
                //ScriptTimer.TimerReset()
                //S_RunCheckPointManagerValidation.FintimerRespawn();
                S_RunCheckPointManagerValidation.DeathPlayer();
            }

        }
    }
}
