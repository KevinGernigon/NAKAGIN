using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_JumpPadV2 : MonoBehaviour
{
    [Range(0, 10000)] public float _BounceHight;
    [Range(0, 10000)] public float _BounceFront;

    private S_PlayerMovement _pm;
    private S_Jetpack _jetpackScript;
    private S_ReferenceInterface _referenceInterface;
    private S_PlayerSound PlayerSoundScript;

    private Transform _orientationPlayer = null;
    private Rigidbody _playerContent = null;

    public bool _isPadUsed = false;

    [SerializeField] private float _globalPowerDivision = 25f;



    private void Awake()
    {

        _referenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        _playerContent = _referenceInterface._playerRigidbody;
        _orientationPlayer = _referenceInterface._orientationTransform;
        _pm = _playerContent.GetComponent<S_PlayerMovement>();
        _jetpackScript = _playerContent.GetComponent<S_Jetpack>();
        PlayerSoundScript = _playerContent.GetComponent<S_PlayerSound>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _jetpackScript._isPadUsed = true;
            PlayerSoundScript.JumppadSound();
            //GameObject _bouncer = other.gameObject;
            //Rigidbody _rb = _bouncer.GetComponent<Rigidbody>();
            Rigidbody _rb = _playerContent;

            if (_pm._isSliding || _pm._isDashing)
            {
                _rb.AddForce(Vector3.up * _BounceHight * 0.7f / _globalPowerDivision, ForceMode.Impulse);
                _rb.AddForce(_orientationPlayer.forward * _BounceFront * 0.7f / _globalPowerDivision, ForceMode.Impulse);
            }
            else
            {
                _rb.AddForce(Vector3.up * _BounceHight / _globalPowerDivision, ForceMode.Impulse);
                _rb.AddForce(_orientationPlayer.forward * _BounceFront / _globalPowerDivision, ForceMode.Impulse);
            }

            StartCoroutine(JumpPadTimer());
        }

        IEnumerator JumpPadTimer()
        {
            yield return new WaitForSeconds(3f);
            _jetpackScript._isPadUsed = false;
        }
    }
}
