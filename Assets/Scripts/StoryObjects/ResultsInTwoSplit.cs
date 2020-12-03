using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsInTwoSplit : StoryObjectManager.StoryObject
{
    [SerializeField]
    string keyword1;
    [SerializeField]
    string keyword2;

    [SerializeField]
    StoryObjectManager.StoryObject resultOne;
    [SerializeField]
    StoryObjectManager.StoryObject resultTwo;

    public override void TriggerAction()
    {
        string trigger = AudioInputManager.Instance.lastResult;
        if (keyword1.Contains(trigger))
        {
            StoryObjectManager.Instance.AddStoryObject(resultOne);
        }
        else
        {

            StoryObjectManager.Instance.AddStoryObject(resultTwo);
        }
        StoryObjectManager.Instance.activeStoryObjects.Remove(this);
        Destroy(gameObject);
    }

}
