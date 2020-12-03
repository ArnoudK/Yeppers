using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugHudTextUpdater : MonoBehaviour
{


    [SerializeField]
    Text m_Recognitions;

    [SerializeField]
    Text m_Hypotheses;
    // Start is called before the first frame update
    void Start()
    {
        AudioInputManager.Instance.OnSpeechResult += Instance_OnSpeechResult;
        AudioInputManager.Instance.OnSpeechHypothesis += Instance_OnSpeechHypothesis;
    }

    private void Instance_OnSpeechHypothesis(object sender, string e)
    {
        m_Hypotheses.text = e;
    }

    private void Instance_OnSpeechResult(object sender, string e)
    {
        m_Recognitions.text = e;
    }

    private void OnDestroy()
    {
        if (AudioInputManager.Instance != null)
        {
            AudioInputManager.Instance.OnSpeechResult -= Instance_OnSpeechResult;
            AudioInputManager.Instance.OnSpeechHypothesis -= Instance_OnSpeechHypothesis;
        }
    }
}
