using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        AudioInputManager.Instance.OnSpeechResult += Instance_OnSpeechResult;
    }

    private void Instance_OnSpeechResult(object sender, string e)
    {
        if (e.Contains("menu"))
        {
            GameManager.Instance.LoadScene(GameManager.REGISTEREDSCENES.MainMenu);
        }
        else if (e.Contains("quit"))
        {
            Application.Quit();
        }

    }
    private void OnDestroy()
    {
        AudioInputManager.Instance.OnSpeechResult -= Instance_OnSpeechResult;

    }
}
