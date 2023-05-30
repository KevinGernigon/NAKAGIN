using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_InfoScore : MonoBehaviour
{
    [Header("Ref Script")]
    private S_ReferenceInterface _referenceInterface;
    private Transform _playerContent;
    [SerializeField]private S_Timer ScriptTimer;
    [SerializeField] private Transform _respawnplayer;

    [Header("Console Generateur")]
    [SerializeField] private S_ConsoleFinRun _consoleFinRun;

    [Header("Info Run")]
    public bool _runStart;
    public bool _Lvl2Win;
    public bool _Lvl1Win;
    [SerializeField] private string _nameRun;
    [SerializeField] private TMP_Text _NameRunTxt;
    [SerializeField] private TMP_Text _level1TimerTxt;
    [SerializeField] private TMP_Text _level2TimerTxt;
    [SerializeField] private TMP_Text _bestTimeTxt;

    private bool _Run1;
    private bool _Run2;
    private bool _Run3;
    private S_PlayFabManager PlayFabManager;

    public float _level1Time = 10f;
    public float _level2Time = 15f;
    private float _bestTime = 0f;
    private float timePlayer;
    public bool endRun = false;
    private S_PauseMenuV2 S_PauseMenuV2;

    [SerializeField] private GameObject _detectionRunBox;
    [SerializeField] private LayerMask _whatIsInformative;
    [SerializeField] private LayerMask Everything;

    [Header("Affichage UI")]

    [SerializeField] private GameObject _HUDInfoScoreRun;
    [SerializeField] private Animator _animOpenCLoseInfo;


    private float _level1Timeminutes, _level1Timeseconds, _level1Timemilliseconds;
    private float _level2Timeminutes, _level2Timeseconds, _level2Timemilliseconds;
    private float _bestTimeminutes, _bestTimeseconds, _bestTimemilliseconds;

    private int _testleaderboard = 10;

    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
        S_PauseMenuV2 = _referenceInterface.EventSystem.GetComponent<S_PauseMenuV2>();
        PlayFabManager = _referenceInterface.PlayFabManager;
    }

    private void Start()
    {
        
        
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
            int whatIsInformative = LayerMask.NameToLayer("WhatIsInformative");
            //Debug.Log(hit.collider.gameObject);

            if (S_PauseMenuV2._isPaused)
            {
                _HUDInfoScoreRun.SetActive(false);
            }
            else
            {
                _HUDInfoScoreRun.SetActive(true);

                if (hit.collider.gameObject.layer == whatIsInformative && hit.collider.gameObject == _detectionRunBox)
                {
                        ShowTimerChallenge();
                        AfficheBesttimeplayer();
                }

                if (hit.collider.gameObject.layer == whatIsInformative)
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

        _NameRunTxt.text = _nameRun ;


        _level1Timeminutes = (int)(_level1Time / 60f) % 60;
        _level1Timeseconds = (int)(_level1Time % 60f);
        _level1Timemilliseconds = (int)(_level1Time * 1000f) % 1000;

        if (_level1Timeseconds < 10)      
            _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + "0" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;
        else
            _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;




        _level2Timeminutes = (int)(_level2Time / 60f) % 60;
        _level2Timeseconds = (int)(_level2Time % 60f);
        _level2Timemilliseconds = (int)(_level2Time * 1000f) % 1000;

        if (_level2Timeseconds < 10)
            _level2TimerTxt.text = "0" + _level2Timeminutes + ":" + "0" + _level2Timeseconds + ":" + "00" + _level2Timemilliseconds;
        else
            _level2TimerTxt.text = "0" + _level2Timeminutes + ":" + _level2Timeseconds + ":" + "00" + _level2Timemilliseconds;

    }


   private void AfficheBesttimeplayer()
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

            if (_bestTime < _level2Time)
            {
                _level2TimerTxt.color = Color.green;
            }
            else
                _level2TimerTxt.color = Color.white;
        }
        else
        {
            _bestTimeTxt.text = "";
            _level1TimerTxt.color = Color.white;
            _level2TimerTxt.color = Color.white;
        }
           
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

    public void SendTimeChallengeToTimer()         //fonction lanc� au passage de la trigger box start de la run 
    {

        ScriptTimer.SetTimerRef(_level1Time, _level2Time);
        _runStart = true;
        _Run1 = _Run2 = _Run3 = false;
    }

    public void Run1()
    {
        _Run1 = true;
        GetBestTimePlayer();
    }
    public void Run2()
    {
        _Run2 = true;
        GetBestTimePlayer();
    }
    public void Run3()
    {
        _Run3 = true;
        GetBestTimePlayer();
    }


    public void GetBestTimePlayer()                //fonction lanc� au passage de la trigger box stop de la run
    {
        if (_runStart)
        {
            //_runStart = false;
            timePlayer = ScriptTimer._timerTime;

            if (timePlayer < _bestTime || _bestTime == 0f)
                {

                 _bestTime = timePlayer;

                 _bestTimeminutes = ScriptTimer._minutes;
                 _bestTimeseconds = ScriptTimer._seconds;
                 _bestTimemilliseconds = ScriptTimer._milliseconds;


                 if (_Run1 && timePlayer <= _level2Time)
                 {
                        int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                        int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                        int _leaderboardValueMS = (int)_bestTimemilliseconds;
                        PlayFabManager.SendLeaderboardRun1(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                 }
                 else if (_Run2 && timePlayer <= _level2Time)
                 {
                        int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                        int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                        int _leaderboardValueMS = (int)_bestTimemilliseconds;
                        PlayFabManager.SendLeaderboardRun2(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                 }
                 else if (_Run3 && timePlayer <= _level2Time)
                 {
                        int _leaderboardValueMinutes = (int)_bestTimeminutes * 60000;
                        int _leaderboardValueSeconds = (int)_bestTimeseconds * 1000;
                        int _leaderboardValueMS = (int)_bestTimemilliseconds;
                        PlayFabManager.SendLeaderboardRun3(_leaderboardValueMinutes + _leaderboardValueSeconds + _leaderboardValueMS);
                 }
                 _Run1 = _Run2 = _Run3 = false;
            }

            if (timePlayer < _level1Time)
            {
                    // ajout chemin vers les recompenses en cas de victoire 
                    _Lvl1Win = true;
                    _consoleFinRun.consoleOpen = true;
            }
           
            if(timePlayer < _level2Time)
            {
                    _Lvl2Win = true;

            }          
        }
    }
    public void ChargeSave(float timeSave)
    {
        timePlayer = timeSave;

        if (timePlayer < _bestTime || _bestTime == 0f)
        {

            _bestTime = timePlayer;

            _bestTimeminutes = (int)(timeSave / 60f) % 60;
            _bestTimeseconds = (int)(timeSave % 60f);
            _bestTimemilliseconds = (int)(timeSave * 1000f) % 1000;
        }

        if (timePlayer < _level1Time)
        {
            // ajout chemin vers les recompenses en cas de victoire 
            _Lvl1Win = true;
            _consoleFinRun.consoleOpen = true;

        }

        if (timePlayer < _level2Time)
        {
            _Lvl2Win = true;

        }
    }
}
