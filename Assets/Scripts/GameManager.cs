using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }


    public bool EnableMicDuringAudio = false;

    // Start is called before the first frame update
    void Awake()
    {


        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void LoadScene(REGISTEREDSCENES scene)
    {

        SceneManager.LoadScene((int)scene);
    }


    public void LoadSceneDelayed(REGISTEREDSCENES scene, float delay)
    {
        StartCoroutine(LoadSceneDelayedCoroutine(delay, scene));
    }

    private IEnumerator LoadSceneDelayedCoroutine(float delay, REGISTEREDSCENES r)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene((int)r);
    }

    public enum REGISTEREDSCENES
    {
        MainMenu = 0,
        StartStory = 1,
        GameOver = 3,
        GameWon = 4
    }


}
