using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundDelayed : MonoBehaviour
{
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    float delay;
    // Start is called before the first frame update
    void Start()
    {

        audio.PlayDelayed(delay);
    }

}
