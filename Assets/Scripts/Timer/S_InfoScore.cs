using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class S_InfoScore : MonoBehaviour
{
    [SerializeField]private S_Timer ScriptTimer;

    [SerializeField] private Transform _player;
    [SerializeField] private Transform _respawnplayer;


    public bool _runStart;
    public bool _Lvl2Win;

    [SerializeField] private TMP_Text _level1TimerTxt;
    [SerializeField] private TMP_Text _level2TimerTxt;
    [SerializeField] private TMP_Text _bestTimeTxt;

    public float _level1Time = 10f;
    public float _level2Time = 15f;
    
    private float _bestTime = 0f;
    

    private float _level1Timeminutes, _level1Timeseconds, _level1Timemilliseconds;
    private float _level2Timeminutes, _level2Timeseconds, _level2Timemilliseconds;
    private float _bestTimeminutes, _bestTimeseconds, _bestTimemilliseconds;




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

     
    }


    private void ShowTimerChallenge()
    {

        _level1Timeminutes = (int)(_level1Time / 60f) % 60;
        _level1Timeseconds = (int)(_level1Time % 60f);
        _level1Timemilliseconds = (int)(_level1Time * 1000f) % 1000;

        _level1TimerTxt.text = "0" + _level1Timeminutes + ":" + _level1Timeseconds + ":" + "00" + _level1Timemilliseconds;



        _level2Timeminutes = (int)(_level2Time / 60f) % 60;
        _level2Timeseconds = (int)(_level2Time % 60f);
        _level2Timemilliseconds = (int)(_level2Time * 1000f) % 1000;

        _level2TimerTxt.text = "0" + _level2Timeminutes + ":" + _level2Timeseconds + ":" + "00" + _level2Timemilliseconds;

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
            _runStart = false;

            float timePlayer = ScriptTimer._timerTime;


            if (timePlayer < _level1Time)
            {

                // ajout chemin vers les recompenses en cas de victoire 

                if (timePlayer < _bestTime || _bestTime == 0f)
                {
                    _level1TimerTxt.color = Color.green;
                    _bestTime = timePlayer;

                    _bestTimeminutes = ScriptTimer._minutes;
                    _bestTimeseconds = ScriptTimer._seconds;
                    _bestTimemilliseconds = ScriptTimer._milliseconds;

                    _bestTimeTxt.text = _bestTimeminutes + ":" + _bestTimeseconds + ":" + _bestTimemilliseconds;

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

            }
            else
            {
                _runStart = false;

                ScriptTimer.TimerReset();

                _player.transform.position = _respawnplayer.transform.position;
                Physics.SyncTransforms();

                //Concequence de défaite
                // temps ecouler 
                // tp le jouer au check point devant le niveau 
                // Attention si le joueur tombe et respawn en dehors du circuit la run est toujour en cours donc a la fin il y a tp
                //TimerReset() lorsque joueur tombe dans le death area

            }

            if (timePlayer < _level2Time)
            {
                _level2TimerTxt.color = Color.green;
                _Lvl2Win = true;

            }
        }
            

    }
}
