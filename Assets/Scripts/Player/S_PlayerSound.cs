using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource SoundManager;
    [SerializeField] private AudioSource WalkingSoundManager;
    [SerializeField] private AudioClip DeathClip;
    [SerializeField] private AudioClip DashClip;
    [SerializeField] private AudioClip ImpactHookClip;
    [SerializeField] private AudioClip RopeClip;
    [SerializeField] private AudioClip RewindClip;
    [SerializeField] private AudioClip JetpackClip;
    [SerializeField] private AudioClip WalkClip;

    [Header("Timer")]
    private float timer = 2.56f;
    private float timerBetween = 2.56f;


    int i;
    public void DeathSound()
    {
        SoundManager.PlayOneShot(DeathClip);
    }

    public void DashSound()
    {
        SoundManager.PlayOneShot(DashClip);
    }

    public void ImpactHookSound()
    {
        SoundManager.PlayOneShot(ImpactHookClip);
    }
    public void RopeSound()
    {
        SoundManager.PlayOneShot(RopeClip);
    }
    public void RewindSound()
    {
        SoundManager.PlayOneShot(RewindClip);
    }
    public void JetpackSound()
    {
        SoundManager.PlayOneShot(JetpackClip);
    }

    public void WalkSound()
    {
        timer += Time.deltaTime;
        if(timer > timerBetween)
        {
            WalkingSoundManager.PlayOneShot(WalkClip);
            timer = 0;
        }
    }

    public void EndWalkSound()
    {
        WalkingSoundManager.Stop();
        timer = 2.56f;
    }
    
}
