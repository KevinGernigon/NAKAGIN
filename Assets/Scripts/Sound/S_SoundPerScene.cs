using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SoundPerScene : MonoBehaviour
{
    private bool _isPlayingNoise;
    private bool _isPlayingMusic;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tuto_Scene")
        {
            if (!_isPlayingNoise)
            {
                Debug.Log("play Noise");
                _isPlayingNoise = true;
                GetComponent<S_PlayerSound>().NoiseSource.Play();
            }
            /*if (!_isPlayingMusic)
            {
                _isPlayingMusic = true;
                GetComponent<S_PlayerSound>().TutoMusic.Play();
            }*/
        }
        else
        {
            if (GetComponent<S_PlayerSound>().NoiseSource.isPlaying)
            {
                GetComponent<S_PlayerSound>().NoiseSource.Stop();
            }
            if (GetComponent<S_PlayerSound>().TutoMusic.isPlaying)
            {
                _isPlayingMusic = false;
                GetComponent<S_PlayerSound>().TutoMusic.Stop();
            }
        }
    }
}
