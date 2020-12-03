using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class OpenSettings : MonoBehaviour
{
    [SerializeField]
    GameObject otherHudToHide;
    [SerializeField]
    GameObject canvasToShow;

    [SerializeField]
    AudioSource playFXeffect;

    [SerializeField]
    AudioMixer am;




    public void ToggleOpenSettings()
    {
        otherHudToHide.SetActive(!otherHudToHide.activeSelf);
        canvasToShow.SetActive(!canvasToShow.activeSelf);
    }

    public void SetFXVolume(float vol)
    {
        am.SetFloat("FXVolumeExp", vol);
        playFXeffect.Play();
    }


    public void SetMusicVolume(float music)
    {
        am.SetFloat("MusicVolumeExp", music);
    }
    public void SetInputDuringAudio(bool val)
    {
        GameManager.Instance.EnableMicDuringAudio = val;
    }



}

