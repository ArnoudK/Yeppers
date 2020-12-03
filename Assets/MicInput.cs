using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicInput : MonoBehaviour
{


    public static MicInput Instance { private set; get; }



    public float MicLoudness { private set; get; }
    public float MicLoudnessDb { private set; get; }

    private string _device;

    private bool isInitialized;

    AudioClip recordclip;
    AudioClip recordedclip;
    const int samples = 128;



    public void InitMic()
    {
        if (_device == null)
        {
            _device = Microphone.devices[0];
        }
        recordclip = Microphone.Start(_device, true, 999, 44100);
        isInitialized = true;
    }

    public void StopMicrophone()
    {
        Microphone.End(_device);
        isInitialized = false;
    }



    //get data from microphone into audioclip
    float MicrophoneLevelMax()
    {
        float levelMax = 0;
        float[] waveData = new float[samples];
        int micPosition = Microphone.GetPosition(null) - (samples + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        recordclip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    //get data from microphone into audioclip
    float MicrophoneLevelMaxDecibels()
    {

        float db = 20 * Mathf.Log10(Mathf.Abs(MicLoudness));

        return db;
    }

    public float FloatLinearOfClip(AudioClip clip)
    {
        StopMicrophone();

        recordedclip = clip;

        float levelMax = 0;
        float[] waveData = new float[recordedclip.samples];

        recordedclip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < recordedclip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    public float DecibelsOfClip(AudioClip clip)
    {
        StopMicrophone();

        recordedclip = clip;

        float levelMax = 0;
        float[] waveData = new float[recordedclip.samples];

        recordedclip.GetData(waveData, 0);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < recordedclip.samples; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }

        float db = 20 * Mathf.Log10(Mathf.Abs(levelMax));

        return db;
    }



    void Update()
    {
        // levelMax equals to the highest normalized value power 2, a small number because < 1
        // pass the value to a static var so we can access it from anywhere
        MicLoudness = MicrophoneLevelMax();
        MicLoudnessDb = MicrophoneLevelMaxDecibels();
    }

    private void Awake()
    {
        if (Instance != this)
        {
            DontDestroyOnLoad(gameObject);
            InitMic();
            isInitialized = true;
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //stop mic when loading a new level or quit application

    void OnDestroy()
    {
        if (Instance == this)
        {
            StopMicrophone();
            Instance = null;
        }
    }


    // make sure the mic gets started & stopped when application gets focused
    void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            //Debug.Log("Focus");

            if (!isInitialized)
            {
                //Debug.Log("Init Mic");
                InitMic();
            }
        }
        if (!focus)
        {
            //Debug.Log("Pause");
            StopMicrophone();
            //Debug.Log("Stop Mic");

        }
    }
}