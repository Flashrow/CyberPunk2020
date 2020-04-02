using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;

    [Range (0f, 1f)]
    public float volume = 1f;

    public bool loop = false;

    public AudioClip clip;
    private AudioSource source;
    //dipa
    public void setAudioSource (AudioSource _source) {
        source = _source;
        source.clip = clip;
    }

    public void play () {
        source.volume = volume;
        source.Play ();
    }
}