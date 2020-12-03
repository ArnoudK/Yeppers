using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryObjectManager : MonoBehaviour
{

    [SerializeField]
    public StoryObject[] storyObjects;


    // Start is called before the first frame update
    void Start()
    {
        AudioInputManager.Instance.OnSpeechResult += Instance_OnSpeechResult;
    }

    private void Instance_OnSpeechResult(object sender, string e)
    {
        foreach (StoryObject so in storyObjects)
        {
            foreach (string triggerKeyWord in so.triggerKeyWords)
                if (e.Contains(triggerKeyWord))
                {
                    so.Triggered();
                }
        }


    }


    public abstract class StoryObject : MonoBehaviour
    {

        [SerializeField]
        AudioSource playSound;
        [SerializeField]
        float delay;

        public string[] triggerKeyWords;

        public void Triggered()
        {
            if (playSound != null)
            {
                playSound.PlayDelayed(delay);
            }
            TriggerAction();


        }

        public abstract void TriggerAction();

    }

}
