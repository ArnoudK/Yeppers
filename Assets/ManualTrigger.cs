using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualTrigger : MonoBehaviour
{

    [SerializeField]
    StoryObjectManager.StoryObject so;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(TriggerDelayed), 1);
    }

    public void TriggerDelayed()
    {
        AudioInputManager.Instance.InvokeOnSPeechResult("a");
    }
}
