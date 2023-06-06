using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_InfoScoreValidation : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;

    [SerializeField] private S_Timer ScriptTimer;

    private Transform _playerContent;
    [SerializeField] private Transform _respawnplayer;

    public bool _runStart;

    [SerializeField] private GameObject _detectionRunBox;
    [SerializeField] private LayerMask _whatIsInformativeValidation;
    [SerializeField] private LayerMask Everything;

    [Header("Affichage UI")]
    [SerializeField] private TMP_Text _level1TimerTxt;
    [SerializeField] private GameObject _HUDInfoScoreRunValidation;
    [SerializeField] private Animator _animOpenCLoseInfo;

    [Header("Info Run")]
    [SerializeField] private S_RunCheckPointManagerValidation S_RunCheckPointManagerValidation;
    private S_PauseMenuV2 S_PauseMenuV2;

    private bool _RunValidation;
    private bool _Run1IA;
    private bool _Run2IA;
    [SerializeField] private TMP_Text _bestTimeTxt;

    public bool endRun = false;

    public float _level1Time = 10f;
    private float _level2Time = 0f;
    private float _level1Timeminutes, _level1Timeseconds, _level1Timemilliseconds;
    [SerializeField] private float _bestTime = 0f;
    private float _bestTimeminutes, _bestTimeseconds, _bestTimemilliseconds;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
        S_PauseMenuV2 = _referenceInterface.EventSystem.GetComponent<S_PauseMenuV2>();
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
            
            if (S_PauseMenuV2._isPaused)
            {
                _HUDInfoScoreRunValidation.SetActive(false);
            }
            else
            {
                _HUDInfoScoreRunValidation.SetActive(true);

                if (hit.collider.gameObject.layer == whatIsInformativeValidation && hit.collider.gameObject == _detectionRunBox)
                {
                    ShowTimerChallenge();
                    AfficheBesttimeplayer();
                }

                if (hit.collider.gameObject.layer == whatIsInformativeValidation)
                {
                    _animOpenCLoseInfo.SetBool("IsOpen", true);                //_animOpenCLoseInfo.Play("A_InfoRunOpen");
                }
                else
                {
                    _animOpenCLoseInfo.SetBool("IsOpen", false);                //_animOpenCLoseInfo.Play("A_InfoRunClose"); 
                }
            }      
        }
    }


    private void ShowTimerChallenge()
    {

        _level1Timeminutes = (int)(_level1Time / 60f) % 60;
        _level1Timeseconds = (int)(_level1Time % 60f);
        _level1Timemilliseconds = (int)(_level1Time * 1000f) % 1000;

        if (_level1Timeseconds < 10)
            _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;
        else
            _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;


    }

    public void AfficheBesttimeplayer()
    {
        if (_bestTime != 0f)
        {
            _bestTimeTxt.text = _bestTimeminutes + ":" + _bestTimeseconds + ":" + _bestTimemilliseconds;
            BestTimeAffichageMinutes();

            if (_bestTime < _level1Time)
            {
                _level1TimerTxt.color = Color.green;
            }
            else
                _level1TimerTxt.color = Color.white;
        }
        else
        {
            _bestTimeTxt.text = "";
            _level1TimerTxt.color = Color.white;
        }

    }


    public void RunValidation()
    {
        _RunValidation = true;
        GetBestTimePlayer();
    }
    public void Run2()
    {
        _Run1IA = true;
        GetBestTimePlayer();
    }
    public void Run3()
    {
        _Run2IA = true;
        GetBestTimePlayer();
    }



    private void BestTimeAffichageMinutes()
    {
        if (_bestTimeminutes < 10)// ajoute un 0 devant les 10 premiere min
        {
            if (_bestTimeseconds < 10)// ajoute un 0 devant les 10 premiere sec
            {

                if (_bestTimemilliseconds < 100)

                    _bestTimeTxt.text = "0" + _bestTimeminutes + ":" + "0" + _bestTimeseconds + ":" + "0" + _bestTimemilliseconds;

                else
                    _bestTimeTxt.text = "0" + _bestTimeminutes + ":" + "0" + _bestTimeseconds + ":" + _bestTimemilliseconds;
            }
            else
            {
                if (_bestTimemilliseconds < 100)

                    _bestTimeTxt.text = "0" + _bestTimeminutes + ":" + _bestTimeseconds + ":" + "0" + _bestTimemilliseconds;

                else
                    _bestTimeTxt.text = "0" + _bestTimeminutes + ":" + _bestTimeseconds + ":" + _bestTimemilliseconds;
            }
        }
        else
        {
            if (_bestTimeseconds < 10)
            {
                if (_bestTimemilliseconds < 100)
                    _bestTimeTxt.text = _bestTimeminutes + ":" + "0" + _bestTimeseconds + ":" + "0" + _bestTimemilliseconds;
                else
                    _bestTimeTxt.text = _bestTimeminutes + ":" + "0" + _bestTimeseconds + ":" + _bestTimemilliseconds;
            }
            else
            {
                if (_bestTimemilliseconds < 100)
                    _bestTimeTxt.text = _bestTimeminutes + ":" + _bestTimeseconds + ":" + "0" + _bestTimemilliseconds;
                else
                    _bestTimeTxt.text = _bestTimeminutes + ":" + _bestTimeseconds + ":" + _bestTimemilliseconds;

            }

        }
    }

    public void SendTimeChallengeToTimer()         //fonction lancé au passage de la trigger box start de la run 
    {

        ScriptTimer.SetTimerRef(_level1Time, _level2Time);
        _runStart = true;
        //_RunValidation = _Run1IA = _Run2IA = false; 
    }

    public void GetBestTimePlayer()                //fonction lancé au passage de la trigger box stop de la run passage au hub suivant
    {
        if (_runStart)
        {
            _runStart = false;

            float timePlayer = ScriptTimer._timerTime;




            if (timePlayer < _bestTime || _bestTime == 0f)
            {

                _bestTime = timePlayer;

                _bestTimeminutes = ScriptTimer._minutes;
                _bestTimeseconds = ScriptTimer._seconds;
                _bestTimemilliseconds = ScriptTimer._milliseconds;

                /*
                if (_Run1IA && timePlayer <= _level1Time)
                {
                    int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                    int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                    int _leaderboardValueMS = (int)_bestTimemilliseconds;
                    //PlayFabManager.SendLeaderboardRun1(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                }
                else if (_Run2IA && timePlayer <= _level1Time)
                {
                    int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                    int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                    int _leaderboardValueMS = (int)_bestTimemilliseconds;
                    //PlayFabManager.SendLeaderboardRun2(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                }
                else if (_RunValidation && timePlayer <= _level1Time)
                {
                    int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                    int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                    int _leaderboardValueMS = (int)_bestTimemilliseconds;
                    //PlayFabManager.SendLeaderboardRun2(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                }
                
                _RunValidation =_Run1IA = _Run2IA = false;*/
            }



            if (timePlayer < _level1Time)
            {
                    _level1TimerTxt.color = Color.green;
            }
            else
            {
                _runStart = false;

                //ScriptTimer.TimerReset()
                //S_RunCheckPointManagerValidation.FintimerRespawn();
                S_RunCheckPointManagerValidation.checkpointCapsule.position = S_RunCheckPointManagerValidation._spawnRunCapsule.position;
                S_RunCheckPointManagerValidation.DeathPlayer();
            }

        }
    }
}
