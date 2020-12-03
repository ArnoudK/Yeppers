using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippleChoice : StoryObjectManager.StoryObject
{
    [SerializeField]
    string keyword1;
    [SerializeField]
    string keyword2;
    [SerializeField]
    string keyword3;

    [SerializeField]
    StoryObjectManager.StoryObject resultOne;
    [SerializeField]
    StoryObjectManager.StoryObject resultTwo;
    [SerializeField]
    StoryObjectManager.StoryObject resultThree;

    public override void TriggerAction()
    {
        string trigger = AudioInputManager.Instance.lastResult;
        if (keyword1.Contains(trigger))
        {
            StoryObjectManager.Instance.activeStoryObjects.Add(resultOne);
        }
        else if (keyword2.Contains(trigger))
        {

            StoryObjectManager.Instance.activeStoryObjects.Add(resultTwo);
        }
        else
        {
            StoryObjectManager.Instance.activeStoryObjects.Add(resultThree);


        }
    }
}


