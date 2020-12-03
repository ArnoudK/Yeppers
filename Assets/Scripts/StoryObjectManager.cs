using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryObjectManager : MonoBehaviour
{

    public static StoryObjectManager Instance { get; private set; }

    public List<StoryObject> activeStoryObjects = new List<StoryObject>();




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Can only be 1 storyobject managager in the scene");
        }
    }
    private void OnDestroy()
    {
        Instance = null;
    }

    public void AddStoryObject(StoryObject storyObject)
    {
        activeStoryObjects.Add(storyObject);
        storyObject.AddedToList();
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioInputManager.Instance.OnSpeechResult += Instance_OnSpeechResult;
    }



    private void Instance_OnSpeechResult(object sender, string e)
    {
        foreach (StoryObject so in activeStoryObjects)
        {
            foreach (string triggerKeyWord in so.triggerKeyWords)
                if (e.Contains(triggerKeyWord))
                {
                    so.Triggered();
                    return;
                }
        }


    }


    public abstract class StoryObject : MonoBehaviour
    {

        public bool playOnAddedToList = false;
        [SerializeField]
        AudioSource playSound;
        [SerializeField]
        float delay;

        public string[] triggerKeyWords;

        public void Triggered()
        {
            if (playSound != null && !playOnAddedToList)
            {
                PlaySound();
            }
            TriggerAction();


        }

        public virtual void AddedToList()
        {
            if (playOnAddedToList)
            {
                PlaySound();
            }
        }

        private void PlaySound()
        {
            if (!GameManager.Instance.EnableMicDuringAudio)
            {
                Debug.Log("Disabling mic for:" + playSound.clip.length);
                AudioInputManager.Instance.DisableMicForSeconds(playSound.clip.length);
            }
            playSound.PlayDelayed(delay);

        }

        public abstract void TriggerAction();

    }

}
