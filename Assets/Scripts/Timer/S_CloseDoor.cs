using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CloseDoor : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private S_InfoScore ScriptInfoScore;
    [SerializeField] private S_Timer ScriptTimer;

    [Header("Sliding Door Configs")]

    [SerializeField] private Transform _doorLvl1;
    [SerializeField] private Transform _doorLvl2;

    [SerializeField] private Vector3 _slideDirection = Vector3.down;

    [Header("Metrix")]
    [SerializeField] private float _slideAmount = 20f;
    [SerializeField] private float _timeAfterClose = 10f;
    [SerializeField] private float _speedClosing;
    [SerializeField] private float _speedOpening = 0.9f;

    private Vector3 _startPositionDoorLvl1;
    private Vector3 _startPositionDoorLvl2;

    public bool _DoorIsOpen;
    public bool _Doorlvl2Open = false;

    private bool _DoorMooving = false;
    private bool _Doorlvl2Mooving = false;
    

    private Coroutine _animationCoroutinelvl1;
    private Coroutine _animationCoroutinelvl2;


    private void Awake()
    {
        _startPositionDoorLvl1 = _doorLvl1.transform.position;
        _startPositionDoorLvl2 = _doorLvl2.transform.position;
    }



    void Start()
    {


    }


    void Update()
    {

        if (ScriptInfoScore._runStart && !_DoorIsOpen && !_DoorMooving)
        {
            OpenDoorLvl1();
        }

        if (ScriptInfoScore._runStart && (ScriptTimer._timerTime > ScriptInfoScore._level1Time - _timeAfterClose) && !_DoorMooving)
        {

            ClosedDoorLvl1(_speedClosing);

        }

        if (ScriptInfoScore._runStart && (ScriptTimer._timerTime > ScriptInfoScore._level1Time - _timeAfterClose) && !_Doorlvl2Mooving)
        {

            ClosedDoorLvl2(_speedClosing);

        }

        if (ScriptInfoScore._runStart && !_Doorlvl2Open)
        {
            OpenDoorLvl2();
        }

        
    }



    public void OpenDoorLvl1()
    {
        //Debug.Log("Open Door");
        if (!_DoorIsOpen)
        {
            if (_animationCoroutinelvl1 != null)
            {
                StopCoroutine(_animationCoroutinelvl1);
            }

            _animationCoroutinelvl1 = StartCoroutine(DoSlidingOpen());

        }

    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = _startPositionDoorLvl1 + _slideAmount * _slideDirection;
        Vector3 startPosition = _doorLvl1.transform.position;

        _DoorIsOpen = true;

        float time = 0;


        while (time < 1)
        {
            _doorLvl1.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * _speedOpening;
        }
    }



    public void ClosedDoorLvl1(float Speed)
    {
        //Debug.Log("Close Door");
        if (_DoorIsOpen)
        {
            if (_animationCoroutinelvl1 != null)
            {
                StopCoroutine(_animationCoroutinelvl1);
            }

            _animationCoroutinelvl1 = StartCoroutine(DoSlidingClose(Speed));

        }

    }
    public void ClosedDoorLvl2(float Speed)
    {
        //Debug.Log("Close Door");
        if ( _Doorlvl2Open)
        {
            if (_animationCoroutinelvl2 != null)
            {
                StopCoroutine(_animationCoroutinelvl2);
            }

            _animationCoroutinelvl2 = StartCoroutine(DoSlidingCloselvl2(Speed));

        }

    }
    private IEnumerator DoSlidingCloselvl2(float Speed)
    {
        Vector3 endPosition = _startPositionDoorLvl2;
        Vector3 startPosition = _doorLvl2.transform.position;

        float time = 0;


        _Doorlvl2Mooving = true;

        while (time < 1)
        {

            _doorLvl2.transform.position = Vector3.Lerp(startPosition, endPosition, time);

            if (!ScriptInfoScore._runStart)
            {
                Speed = _speedOpening;
            }

            yield return null;
            if (ScriptTimer._timerPlay || (_DoorIsOpen && !ScriptInfoScore._runStart))
                time += Time.deltaTime * Speed;
        }

        /* envoye un signal lorsque la porte est fermer pour tp le joueur si encore en run */
        //Debug.Log("Porte fermer");
        _Doorlvl2Mooving = false;
        _Doorlvl2Open = false;

    }

    private IEnumerator DoSlidingClose(float Speed)
    {
        Vector3 endPosition = _startPositionDoorLvl1;
        Vector3 startPosition = _doorLvl1.transform.position;

        float time = 0;

    
        _DoorMooving = true;

        while (time < 1)
        {

            _doorLvl1.transform.position = Vector3.Lerp(startPosition, endPosition, time);

            if (!ScriptInfoScore._runStart)
            {
                Speed = _speedOpening;
            }

            yield return null;
            if(ScriptTimer._timerPlay || (_DoorIsOpen && !ScriptInfoScore._runStart))
                time += Time.deltaTime * Speed;
        }

        /* envoye un signal lorsque la porte est fermer pour tp le joueur si encore en run */
        //Debug.Log("Porte fermer");
        _DoorMooving = false;
        _DoorIsOpen = false;

    }

    public void ResetRun()
    {
        ScriptInfoScore._runStart = false;
        if (_DoorIsOpen)
        {
            ClosedDoorLvl1(_speedOpening);
        }

    }

    public void OpenDoorLvl2()
    {
        if (!_Doorlvl2Open)
        {
            if (_animationCoroutinelvl2 != null)
            {
                StopCoroutine(_animationCoroutinelvl2);
            }

            _animationCoroutinelvl2 = StartCoroutine(DoSlidingOpenDoorLvl2());

        }
    }

    private IEnumerator DoSlidingOpenDoorLvl2()
    {
        Vector3 endPosition = _startPositionDoorLvl2 + _slideAmount * _slideDirection;
        Vector3 startPosition = _doorLvl2.transform.position;

        _Doorlvl2Open = true;

        float time = 0;
        while (time < 1)
        {
            _doorLvl2.transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * _speedOpening;
        }
    }

}


