using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AudioInputManager : MonoBehaviour
{



    private DictationRecognizer m_DictationRecognizer;

    private MicInput mi;
    public static AudioInputManager Instance { private set; get; }

    public string lastResult { private set; get; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mi = MicInput.Instance;
        }
        else
        {
            Destroy(gameObject);
            return;

        }

    }

    public event EventHandler<string> OnSpeechResult;
    public event EventHandler<string> OnSpeechHypothesis;


    public void InvokeOnSPeechResult(string e)
    {
        lastResult = e;
        OnSpeechResult?.Invoke(this, e);
    }

    private void Start()
    {

        SetupDictation();
        mi = MicInput.Instance;
    }

    private void SetupDictation()
    {
        m_DictationRecognizer = new DictationRecognizer();
        m_DictationRecognizer.DictationResult += (text, confidence) =>
        {
            lastResult = text;
            OnSpeechResult?.Invoke(this, text);
        };

        m_DictationRecognizer.DictationHypothesis += (text) =>
        {
            OnSpeechHypothesis?.Invoke(this, text);
        };

        m_DictationRecognizer.DictationComplete += (completionCause) =>
        {
            if (completionCause != DictationCompletionCause.Complete)
                Debug.LogErrorFormat("Dictation completed unsuccessfully: {0}.", completionCause);
        };

        m_DictationRecognizer.DictationError += (error, hresult) =>
        {
            Debug.LogErrorFormat("Dictation error: {0}; HResult = {1}.", error, hresult);
            m_DictationRecognizer = null;
        };

        m_DictationRecognizer.Start();
    }

    private bool keepActive = true;
    float lastDicStart;
    //keep it active
    private void FixedUpdate()
    {
        if (m_DictationRecognizer != null)
        {
            if (m_DictationRecognizer.Status != SpeechSystemStatus.Running && keepActive)
            {
                if (mi.MicLoudness > 0.0005)
                {
                    if (Time.time - lastDicStart > 2)
                        SetupDictation();
                    Debug.Log("set up new dictation last one was: " + (Time.time - lastDicStart) + "seconds ago");
                    lastDicStart = Time.time;
                }
            }
        }
    }


    public void DisableMicForSeconds(float time)
    {
        StopListening();
        StartCoroutine(EnableMicDelayedCoroutine(time));
    }
    private IEnumerator EnableMicDelayedCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ForceStart();
    }


    public void StopListening()
    {
        if (m_DictationRecognizer != null)
        {
            keepActive = false;
            m_DictationRecognizer.Stop();

        }

    }

    public void ForceStart()
    {
        SetupDictation();
        keepActive = true;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }


}




