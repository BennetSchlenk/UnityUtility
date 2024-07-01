using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmbientAudio : MonoBehaviour
{
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        var source = GetComponent<AudioSource>();
        source.clip = this.clip;
        source.loop = true;
        source.Play();
    }

}
