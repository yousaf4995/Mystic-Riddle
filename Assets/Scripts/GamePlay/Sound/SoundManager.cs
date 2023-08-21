using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [Space]
    public AudioClip gameStartClip;
    [Space]
    public AudioClip correctClip;
    public AudioClip inCorrectClip;
    public AudioClip btnClip;

    [Space]
    public AudioClip flipClip;

    [Space]
    public AudioClip gameWinClip;
    public AudioClip gameLoosClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayGameStartSound()
    {
        PlayClip(gameStartClip);
    }

    public void PlayButtonClickSound()
    {
        PlayClip(btnClip);
    }

    public void PlayCorrectSound()
    {
        PlayClip(correctClip);
    }
    public void PlayInCorrectSound()
    {
        PlayClip(inCorrectClip);
    }
    public void PlayFlipSound()
    {
        PlayClip(flipClip);
    }
    public void PlayWinSound()
    {
        PlayClip(gameWinClip);
    }
    public void PlayLoosSound()
    {
        PlayClip(gameLoosClip);
    }

    public void PlayClip(AudioClip clip)
    {
         audioSource.PlayOneShot(clip);
     //   Debug.Log("Clip Played : " + clip);
    }

    public void PauseSoundPlayer(bool pasue)
    {
        if (pasue)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }

    public void SetSoundPlayerVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
