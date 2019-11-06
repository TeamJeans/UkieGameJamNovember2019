using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource Wind;
    // Start is called before the first frame update
    void Start()
    {
        Wind = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Wind.Play(1);
        }
    }

    
}
