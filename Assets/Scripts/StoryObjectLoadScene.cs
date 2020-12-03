using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryObjectLoadScene : StoryObjectManager.StoryObject
{
    [SerializeField]
    GameManager.REGISTEREDSCENES sceneToLoad;

    [SerializeField]
    float sceneLoadDelay;

    private bool activated = false;





    public override void AddedToList()
    {
        if (playOnAddedToList)
        {
            GameManager.Instance.LoadSceneDelayed(sceneToLoad, sceneLoadDelay);

        }
        base.AddedToList();
    }

    public override void TriggerAction()
    {
        if (!activated)
        {
            GameManager.Instance.LoadSceneDelayed(sceneToLoad, sceneLoadDelay);
            activated = true;
        }
    }
}
