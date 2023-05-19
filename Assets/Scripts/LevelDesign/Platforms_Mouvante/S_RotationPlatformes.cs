 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RotationPlatformes : MonoBehaviour
{

    private S_ReferenceInterface _referenceInterface;
    private S_PlayerSound PlayerSoundScript;


    [SerializeField]
    private GameObject _centerPlatforms;
    private GameObject Player;

    [SerializeField]
    private float _degre = 90f;
    private float _alpha = 0.0f;
    public float _alphaSpeed = 0.001f;
    private bool _isTrigger = false;
    private bool _startMoving = false;
    private bool _stopSpam = false;
    private Vector3 _initialRotation;
    int i = 1;
    private void Awake()
    {

        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        Player = _referenceInterface._playerGameObject;
        PlayerSoundScript = Player.GetComponent<S_PlayerSound>();
    }


    

    void Start()
    {
        
    }

    void Update()
    {
        
        if (_isTrigger)
        {
            
            if (_referenceInterface._InputManager._playerInputAction.Player.MoveModuleLeft.triggered && _startMoving == false)
            {
                _initialRotation = _centerPlatforms.transform.eulerAngles;
                _alpha = 0f;
               
                _startMoving = true;
                StartCoroutine(MovePlatformsRight(_centerPlatforms));
            }
            
            if (_referenceInterface._InputManager._playerInputAction.Player.MoveModuleRight.triggered && _startMoving == false)
            {
                _initialRotation = _centerPlatforms.transform.eulerAngles;
                _alpha = 0f;

                _startMoving = true;
                StartCoroutine(MovePlatformsLeft(_centerPlatforms));

            }

        }

    }

    private void FixedUpdate() {
        if (_startMoving == true)
        {
            
            if (_alpha >= 1)
            {
                i = 1;
            }

            if (_alpha >= 0.9f && i == 1)
            {
                i = 0;
                PlayerSoundScript.EndPlatformMovingSound();
                PlayerSoundScript.EndingPlatformMovingSound();
            }
        }

    }

    IEnumerator MovePlatformsRight(GameObject platforms)
    {
        PlayerSoundScript.PlatformMovingSound();
        var random = Random.Range(1, 2);
        //_arms_AC.SetBool("rightArmUse" + random.ToString(), true);
        Player.GetComponent<S_PlayerMovement>().armAnimToPlay("rightArmUse" + random.ToString());
        while (_startMoving == true && _alpha < 1)
        {
            
            _centerPlatforms.transform.Rotate(Vector3.right * _degre / Mathf.Round(1 / _alphaSpeed));
            _alpha += _alphaSpeed;
            yield return new WaitForSeconds(0.01f);
        }
        _startMoving = false;        
    }
    IEnumerator MovePlatformsLeft(GameObject platforms)
    {
        PlayerSoundScript.PlatformMovingSound();
        var random = Random.Range(1, 2);
        //_arms_AC.SetBool("leftArmUse" + random.ToString(), true);
        Player.GetComponent<S_PlayerMovement>().armAnimToPlay("leftArmUse" + random.ToString());
        while (_startMoving == true && _alpha < 1)
        {
            
            _centerPlatforms.transform.Rotate(Vector3.left * _degre / Mathf.Round(1 / _alphaSpeed));
            _alpha += _alphaSpeed ;
            yield return new WaitForSeconds(0.01f);
        }
        _startMoving = false;
    }

    public void OnTriggersEnter()
    {
        _isTrigger = true;
    }
    public void OnTriggersExit()
    {

        _isTrigger = false;
    }

}
