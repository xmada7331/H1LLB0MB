using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameManager gameManager;
    public bool isTrackPlaying;
    public bool wasPaused;
    public AudioSource mainAudioSource;
    public AudioSource railGrindSource;

    public AudioClip track01;
    public AudioClip track02;
    public AudioClip track03;
    public AudioClip track04;
    public AudioClip cassetteChange;
    public AudioClip coinCollect;
    public AudioClip railGrind;
    public AudioClip railHit;
    public AudioClip ollieJump;
    public AudioClip goingDownhill;

    public TMP_Text trackName;
    public GameObject cassette;

    public Animator cassetteAnimator;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    void Start()
    {
        //PlayRandomTrack();
    }

    void Update()
    {
        if (!isTrackPlaying && !mainAudioSource.isPlaying && !wasPaused && !gameManager.firstStart)
        {
            PlayRandomTrack();
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && mainAudioSource.isPlaying)
        {
            wasPaused = true;
        }
        if (hasFocus)
        {
            wasPaused = false;
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && mainAudioSource.isPlaying)
        {
            wasPaused = true;
        }
        if (!pauseStatus)
        {
            wasPaused = false;
        }
    }

    private void PlayRandomTrack()
    {
        int randomTrack = Random.Range(1, 5);
        switch (randomTrack)
        {
            case 1:
                mainAudioSource.clip = track01;
                trackName.text = "Track 01";
                isTrackPlaying = true;
                cassetteAnimator.SetTrigger("Play");
                break;
            case 2:
                mainAudioSource.clip = track02;
                trackName.text = "Track 02";
                isTrackPlaying = true;
                cassetteAnimator.SetTrigger("Play");
                break;
            case 3:
                mainAudioSource.clip = track03;
                trackName.text = "Track 03";
                isTrackPlaying = true;
                cassetteAnimator.SetTrigger("Play");
                break;
            case 4:
                mainAudioSource.clip = track04;
                trackName.text = "Track 04";
                isTrackPlaying = true;
                cassetteAnimator.SetTrigger("Play");
                break;
            default:
                mainAudioSource.clip = track01;
                trackName.text = "Track 01";
                isTrackPlaying = true;
                cassetteAnimator.SetTrigger("Play");
                break;
        }
        isTrackPlaying = false;
        mainAudioSource.Play();
    }

    public void PlayCoinCollect()
    {
        mainAudioSource.PlayOneShot(coinCollect);
    }
    public void PlayRailGrind()
    {
        if (!railGrindSource.isPlaying)
        {
            railGrindSource.clip = railGrind;
            railGrindSource.loop = true;
            railGrindSource.Play();
        }
    }
    public void StopRailGrindLoop()
    {
        if (railGrindSource.isPlaying)
        {
            railGrindSource.Stop();
        }
    }
    public void PlayRailHit()
    {
        mainAudioSource.PlayOneShot(railHit);
    }
    public void PlayOllieJump()
    {
        mainAudioSource.PlayOneShot(ollieJump);
    }
    public void PlayGoingDownhill()
    {
        mainAudioSource.PlayOneShot(goingDownhill);
    }

}
