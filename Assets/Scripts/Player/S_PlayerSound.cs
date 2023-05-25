using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_PlayerSound : MonoBehaviour
{
    [Header("Reference")]
    private S_PlayerMovement PlayerMovement;

    [Header("Audio")]
    [SerializeField] private AudioSource SoundManager;
    [SerializeField] private AudioSource WalkSoundManager;
    [SerializeField] private AudioSource SlideSoundManager;
    [SerializeField] private AudioSource LandingSoundManager;
    [SerializeField] private AudioSource PlatformSoundManager;
    [SerializeField] private AudioSource WallRunSoundManager;
    [SerializeField] private AudioSource SauvetageSoundManager;
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
    [SerializeField] private AudioClip WallRunClip;
    [SerializeField] private AudioClip AccessDeniedClip;
    [SerializeField] private AudioClip AccessAcceptedClip;
    [SerializeField] private AudioClip SauvetageClip;
    [SerializeField] private AudioClip NoBatteryClip;
    [SerializeField] private AudioClip ValidationConsoleClip;
    [SerializeField] public AudioSource NoiseSource;
    [SerializeField] public AudioSource TutoMusic;


    [Header("Bool")]
    private bool _isPlayingLanding = false;
    private bool _isPlayingWalk = false;
    private bool _isPlayingPlatform = false;
    private bool _isPlayingWallRun = false;
    private bool _isPlayingSauvetage = false;

    [Header("Timer")]
    private float timer = 2.56f;
    private float timerWallRun = 2.56f;
    private float timerSauvetage = 2.56f;
    private float timerBetween = 2.56f;
    private float timerBetweenSauvetage = 1.38f;


    int i;

    private void Start()
    {
        PlayerMovement = GetComponent<S_PlayerMovement>();
    }

    ////////////////////
    /// Sound Effect ///
    ////////////////////
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

    public void DestructionSound()
    {
        SoundManager.PlayOneShot(DestructionClip);
    }

    public void JumppadSound()
    {
        SoundManager.PlayOneShot(JumppadClip);
    }

    public void LandingSound()
    {

        if (_isPlayingLanding == false)
        {
            StartCoroutine(AlreadyPlayingLanding());
        }

    }

    public void AccessDeniedSound()
    {
        SoundManager.PlayOneShot(AccessDeniedClip);
    }
    public void AccessAcceptedSound()
    {
        SoundManager.PlayOneShot(AccessAcceptedClip);
    }
    public void NoBatterySound()
    {
        SoundManager.PlayOneShot(NoBatteryClip);
    }
    public void ValidationConsoleSound()
    {
        SoundManager.PlayOneShot(ValidationConsoleClip);
    }

    /*public void ClickButton()
    {
        SoundManager.PlayOneShot()
    }*/

    public void Noise()
    {
        NoiseSource.Play();
    }

    public void PlayMusic()
    {
        TutoMusic.Play();
    }

    ////////////////////////
    /// End Sound Effect ///
    ////////////////////////



    //////////////////////////////////////////////////////
    /// Walk
    //////////////////////////////////////////////////////
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
    public void EndSoundWalk()
    {
        WalkSoundManager.Stop();
        timer = 2.56f;
    }
    //////////////////////////////////////////////////////
    /// WallRun
    /////////////////////////////////////////////////////////
    public void WallRunSound()
    {
        WallRunSoundManager.volume = 0.2f;
        if (PlayerMovement._isWallRunning)
        {
            timerWallRun += Time.deltaTime;
            if (timerWallRun > timerBetween)
            {
                //WalkSoundManager.PlayOneShot(WalkClip);
                StartCoroutine(WaitingWallRun());
                timerWallRun = 0;
            }
        }
    }
    public void EndWallRunSound()
    {
        WallRunSoundManager.Stop();
        StartCoroutine(FadeAudioSource.StartFade(WallRunSoundManager, 1, 0));
        timerWallRun = 2.56f;
    }
    //////////////////////////////////////////////////////


    //////////////////////////////////////////////////////
    /// Platform
    //////////////////////////////////////////////////////
    
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

    //////////////////////////////////////////////////////
    /// Slide
    //////////////////////////////////////////////////////

    public void SlideSound()
    {
        SlideSoundManager.volume = 0.2f;
        SlideSoundManager.PlayOneShot(SlideClip);
        StartCoroutine(FadeAudioSource.StartFade(SlideSoundManager, 1, 0));
    }
    public void EndSoundSlide()
    {
        SlideSoundManager.Stop();
    }

    //////////////////////////////////////////////////////
    /// SauvetageJetPack
    //////////////////////////////////////////////////////

    public void StartSauvetageSound()
    {
        SauvetageSoundManager.volume = 0.5f;
        if (!_isPlayingSauvetage)
        {
            timerSauvetage += Time.deltaTime;
            if(timerSauvetage > timerBetweenSauvetage)
            {
                StartCoroutine(SauvetageCoroutine());
            }
        }
    }

    public void EndSauvetageSound()
    {
        SauvetageSoundManager.Stop();
        StartCoroutine(FadeAudioSource.StartFade(SauvetageSoundManager, 1, 0));
    }

    public void PauseSound()
    {
        AudioListener.pause = true;
      
    }

    public void UnPauseSound()
    {
        AudioListener.pause = false;
      
    }

    //////////////////////////////////////////////////////
    /// Autre
    //////////////////////////////////////////////////////

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

    IEnumerator WaitingWallRun()
    {
        _isPlayingWallRun = true;
        WallRunSoundManager.Play();
        yield return new WaitUntil(() => WallRunSoundManager.isPlaying);
        _isPlayingWallRun = false;
    }
    IEnumerator SauvetageCoroutine()
    {
        _isPlayingSauvetage = true;
        SauvetageSoundManager.Play();
        yield return new WaitUntil(() => SauvetageSoundManager.isPlaying);
        _isPlayingSauvetage = false;
    }

    //////////////////////////////////////////////////////
}
