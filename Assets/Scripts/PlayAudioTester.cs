using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioTester : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    string keyWordToActivateSound;
    private AudioSource playSound;

    void Start()
    {
        AudioInputManager.Instance.OnSpeechResult += Instance_OnSpeechResult;
        playSound = GetComponent<AudioSource>();
        if (playSound == null)
        {
            Debug.LogError("This script needs to be attachted to a gameobject that contains an AudioSource");
        }
    }

    private void Instance_OnSpeechResult(object sender, string e)
    {
        if (e.Contains("play") && e.Contains(keyWordToActivateSound))
        {
            Debug.Log("Playing object with: " + keyWordToActivateSound);
            playSound.Play();
        }
    }
    private void OnDestroy()
    {
        AudioInputManager.Instance.OnSpeechResult -= Instance_OnSpeechResult;
    }

}
