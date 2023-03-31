using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraIA : MonoBehaviour
{
    private Transform target;
    private S_ReferenceInterface S_ReferenceInterface;

    [SerializeField] private Transform _eyes1;
    [SerializeField] private Transform _eyes2;
    [SerializeField] private Transform _eyes3;
    private Transform _setpositionoeil1;
    private Transform _setpositionoeil2;
    private Transform _setpositionoeil3;
    private Transform _actuelpositionoeil1;
  


    [SerializeField] private Animator _animCam;

    public bool _cameraIAisTarget;
    private bool _reset;
    private float timecount = 0.0f;

    private bool _isAnim = false;
    private float _timeAnim;
    [SerializeField] private float _delayBtwAnim = 0f;

    private void Awake()
    {
        S_ReferenceInterface = S_GestionnaireManager.GetManager<S_ReferenceInterface>();
        target = S_ReferenceInterface._playerTransform;

        _setpositionoeil1 = _eyes1;
        _setpositionoeil2 = _eyes2;
        _setpositionoeil3 = _eyes3;

    }

    private void Update()
    {
        timecount = timecount + Time.deltaTime;

        if (_cameraIAisTarget)
        {
            _animCam.enabled = false;
            _eyes1.LookAt(target);
            _eyes2.LookAt(target);
            _eyes3.LookAt(target);

            _reset = true;
            
        }

        if (!_cameraIAisTarget && _reset)
        {
           /* _eyes1.rotation = Quaternion.Lerp(_eyes1.rotation, _setpositionoeil1.rotation, timecount * 0.01f);
            _eyes2.rotation = Quaternion.Lerp(_eyes2.rotation, _setpositionoeil2.rotation, timecount * 0.01f);
            _eyes3.rotation = Quaternion.Lerp(_eyes2.rotation, _setpositionoeil3.rotation, timecount * 0.01f);*/

            _eyes1 = _setpositionoeil1;
            _eyes2 = _setpositionoeil2;
            _eyes3 = _setpositionoeil3;

            _animCam.enabled = true;
            _reset = false;
        }

        

        if (!_isAnim && _delayBtwAnim <= 0f && !_cameraIAisTarget)
        {
            _isAnim = true;
            StartCoroutine(PlayAnimation());
        }

        if (_delayBtwAnim > 0 && !_cameraIAisTarget)
        {
            _delayBtwAnim -= Time.deltaTime;
        }

    }

    IEnumerator PlayAnimation()
    {
        _animCam.Rebind();
        if(this.name == "A_Cam_IA")
            _animCam.Play("A_CameraIA");
        else 
            _animCam.Play("Take 001");
        yield return new WaitForSeconds(3.10f);
        RandomDelayBetweenAnim();
        _isAnim = false;
    }
    private void RandomDelayBetweenAnim()
    {
        _delayBtwAnim = Random.Range(5f, 10.0f);
    }


    public void SwitchTagetplayer()
    {
        _cameraIAisTarget = !_cameraIAisTarget;
        if (_cameraIAisTarget)
        {
            _animCam.Rebind();
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SwitchTagetplayer();
    }
    private void OnTriggerExit(Collider other)
    {
        SwitchTagetplayer();
    }


}
