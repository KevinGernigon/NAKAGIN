using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_InfoScore : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;

    [SerializeField]private S_Timer ScriptTimer;

    private Transform _playerContent;
    [SerializeField] private Transform _respawnplayer;

    public bool _runStart;
    public bool _Lvl2Win;
    public bool _Lvl1Win;
    [SerializeField] private string _nameRun;

    [SerializeField] private TMP_Text _NameRunTxt;
    [SerializeField] private TMP_Text _level1TimerTxt;
    [SerializeField] private TMP_Text _level2TimerTxt;
    [SerializeField] private TMP_Text _bestTimeTxt;

    public float _level1Time = 10f;
    public float _level2Time = 15f;
    private float _bestTime = 0f;
    private float timePlayer;
    public bool endRun = false;
    public bool _isAnimPlaying = false;

    [Header("Affichage UI")]

    [SerializeField] private GameObject _HUDInfoScore;
    [SerializeField] private Animator _aniamHUDInfoRun;

    [SerializeField] private GameObject _detectionRunBox;
    [SerializeField] private LayerMask _whatIsInformative;
    [SerializeField] private LayerMask Everything;



    private float _level1Timeminutes, _level1Timeseconds, _level1Timemilliseconds;
    private float _level2Timeminutes, _level2Timeseconds, _level2Timemilliseconds;
    private float _bestTimeminutes, _bestTimeseconds, _bestTimemilliseconds;


    private void Awake()
    {
        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerTransform;
       
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


            if (hit.collider.gameObject.layer == whatIsInformative && hit.collider.gameObject == _detectionRunBox)
            {

                ShowTimerChallenge();
                AfficheBesttimeplayer();
            }

            if (hit.collider.gameObject.layer == whatIsInformative)
            {
                _HUDInfoScore.SetActive(true);

                if (_isAnimPlaying)
                {
                    _aniamHUDInfoRun.Rebind();
                    _aniamHUDInfoRun.Play("A_InfoScoreOpen");
                    _isAnimPlaying = false;
                }
   
            }
            else if (!_isAnimPlaying)
            {
                //StartCoroutine(AffichageHUDInfoRun());
                _aniamHUDInfoRun.Rebind();
                _aniamHUDInfoRun.Play("A_InfoScoreClosing");
                _isAnimPlaying = true;
            }
        }
        else if (!_isAnimPlaying)
        {
            //StartCoroutine(AffichageHUDInfoRun());
            _aniamHUDInfoRun.Rebind();
            _aniamHUDInfoRun.Play("A_InfoScoreClosing");
            _isAnimPlaying = true;
        }
    }

    /*
    IEnumerator AffichageHUDInfoRun()
    {
      
        yield return new WaitForSeconds(1f);

        if (!_isAnimPlaying)
        {
            _aniamHUDInfoRun.Rebind();
            _aniamHUDInfoRun.Play("A_InfoScoreClosing");
            _isAnimPlaying = true;
        }
        yield return new WaitForSeconds(1f);

        _HUDInfoScore.SetActive(false);
    }*/

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

    public void SendTimeChallengeToTimer()         //fonction lancé au passage de la trigger box start de la run 
    {

        ScriptTimer.SetTimerRef(_level1Time, _level2Time);
        _runStart = true;
    }

    public void GetBestTimePlayer()                //fonction lancé au passage de la trigger box stop de la run
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
             
        }


        if (timePlayer < _level1Time)
        {
                // ajout chemin vers les recompenses en cas de victoire 
                _Lvl1Win = true;
        }
        /*else
        {
                _runStart = false;

                ScriptTimer.TimerReset();

                S_RunCheckPointManager.FintimerRespawn();

        }*/

        if(timePlayer < _level2Time)
        {
                _Lvl2Win = true;

        }          
    }
            

    }
}
