using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource SoundManager;
    [SerializeField] private AudioSource WalkSoundManager;
    [SerializeField] private AudioSource SlideSoundManager;
    [SerializeField] private AudioSource LandingSoundManager;
    [SerializeField] private AudioSource PlatformSoundManager;
    [SerializeField] private AudioClip DeathClip;
    [SerializeField] private AudioClip DashClip;
    [SerializeField] private AudioClip ImpactHookClip;
    [SerializeField] private AudioClip RopeClip;
    [SerializeField] private AudioClip RewindClip;
    [SerializeField] private AudioClip JetpackClip;
    [SerializeField] private AudioClip WalkClip;
    [SerializeField] private AudioClip JumpClip;
    [SerializeField] private AudioClip SlideClip;
    [SerializeField] private AudioClip LandingClip;
    [SerializeField] private AudioClip DestructionClip;
    [SerializeField] private AudioClip JumppadClip;
    [SerializeField] private AudioClip EndPlatformMovingClip;


    [Header("Bool")]
    private bool _isPlayingLanding = false;
    private bool _isPlayingWalk = false;
    private bool _isPlayingPlatform = false;

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
    
    public void JumpSound()
    {
        SoundManager.PlayOneShot(JumpClip);
    }
    public void SlideSound()
    {
        SlideSoundManager.volume = 0.2f;
        SlideSoundManager.PlayOneShot(SlideClip);
        StartCoroutine(FadeAudioSource.StartFade(SlideSoundManager, 1, 0));
    }

    public void WalkSound()
    {

        if (_isPlayingLanding == false)
        {
            timer += Time.deltaTime;
            if(timer > timerBetween)
            {
                //WalkSoundManager.PlayOneShot(WalkClip);
                StartCoroutine(AlreadyPlayingWalk());
                timer = 0;
            }
        }
    }
    public void LandingSound()
    {

        if (_isPlayingLanding == false)
        {
            StartCoroutine(AlreadyPlayingLanding());
        }

    }
    public void DestructionSound()
    {
        SoundManager.PlayOneShot(DestructionClip);
    }
    
    public void JumppadSound()
    {
        SoundManager.PlayOneShot(JumppadClip);
    }

    public void PlatformMovingSound()
    {
        if (_isPlayingPlatform == false)
        {
            StartCoroutine(AlreadyPlayingMovingPlatform());
        }
        
    }

    public void EndPlatformMovingSound()
    {
        PlatformSoundManager.Stop();
    }
    public void EndingPlatformMovingSound()
    {

        SoundManager.PlayOneShot(EndPlatformMovingClip);
    }
    public void EndSoundWalk()
    {
        WalkSoundManager.Stop();
        timer = 2.56f;
    }
    public void EndSoundSlide()
    {
        SlideSoundManager.Stop();
    }
    public static class FadeAudioSource
    {
        public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }
            yield break;
        }
    }

    IEnumerator AlreadyPlayingLanding()
    {
        _isPlayingLanding = true;
        LandingSoundManager.Play(0);
        yield return new WaitUntil(() => LandingSoundManager.isPlaying);
        _isPlayingLanding = false;
    }

    IEnumerator AlreadyPlayingWalk()
    {
        _isPlayingWalk = true;
        WalkSoundManager.Play(0);
        yield return new WaitUntil(() => WalkSoundManager.isPlaying);
        _isPlayingWalk = false;
    }
    IEnumerator AlreadyPlayingMovingPlatform()
    {
        _isPlayingPlatform = true;
        PlatformSoundManager.Play();
        yield return new WaitUntil(() => PlatformSoundManager.isPlaying);
        _isPlayingPlatform = false;
    }
}
