using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CloseDoorValidation : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private S_InfoScoreValidation ScriptInfoScore;
    [SerializeField] private S_Timer ScriptTimer;

    [Header("Sliding Door Configs")]

    [SerializeField] private Transform _doorLvl1;

    [SerializeField] private Vector3 _slideDirection = Vector3.down;

    [Header("Metrix")]
    [SerializeField] private float _slideAmount = 3.7f;
    [SerializeField] private float _timeAfterClose = 10f;
    [SerializeField] private float _speedClosing = 0.1f;
    [SerializeField] private float _speedOpening = 0.9f;

    private Vector3 _startPositionDoorLvl1;

    public bool _DoorIsOpen;
   

    private bool _DoorMooving = false;


    private Coroutine _animationCoroutine;



    private void Awake()
    {
        _startPositionDoorLvl1 = _doorLvl1.transform.position;
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

    }



    public void OpenDoorLvl1()
    {
        //Debug.Log("Open Door");
        if (!_DoorIsOpen)
        {
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(DoSlidingOpen());

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
            if (_animationCoroutine != null)
            {
                StopCoroutine(_animationCoroutine);
            }

            _animationCoroutine = StartCoroutine(DoSlidingClose(Speed));

        }

    }

    private IEnumerator DoSlidingClose(float Speed)
    {
        Vector3 endPosition = _startPositionDoorLvl1;
        Vector3 startPosition = _doorLvl1.transform.position;

        float time = 0;

        _DoorIsOpen = false;

        _DoorMooving = true;

        while (time < 1)
        {

            _doorLvl1.transform.position = Vector3.Lerp(startPosition, endPosition, time);

            if (!ScriptInfoScore._runStart)
            {
                Speed = _speedOpening;
            }

            yield return null;
            if(ScriptTimer._timerPlay)
                time += Time.deltaTime * Speed;
        }
        _DoorMooving = false;
    }

    public void ResetRun()
    {
        ScriptInfoScore._runStart = false;
        if (_DoorIsOpen)
        {
            ClosedDoorLvl1(_speedOpening);
        }

    }

}


