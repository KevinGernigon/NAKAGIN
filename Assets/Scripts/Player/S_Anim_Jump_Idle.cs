using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_Anim_Jump_Idle : MonoBehaviour
{
    [SerializeField]
    private S_PlayerMovement _playerMovement;
    [SerializeField]
    private Animator _arms_AC;
    private bool _coroutineStarted;

    private void OnEnable()
    {
        StartCoroutine(countDownForJumpIdle());
    }

    public IEnumerator countDownForJumpIdle()
    {
        var countdown = 0f;
        while (true)
        {
            if (_playerMovement.state == S_PlayerMovement.MovementState.air)
            {
                countdown += 0.025f;
            }
            else countdown = 0;

            if (countdown >= 0.125f)
            {
                if (!_arms_AC.GetBool("dashing"))
                {
                    _arms_AC.SetBool("isInAir", true);
                    _arms_AC.SetBool("startMoving", false);
                }
                else _arms_AC.SetBool("isInAir", false);
            }
            else _arms_AC.SetBool("isInAir", false);

            yield return new WaitForSeconds(0.05f);
        }
    }
    
}
