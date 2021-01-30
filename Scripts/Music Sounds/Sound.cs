using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]


public class Sound
{

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(.1f, 3)]
    public float pitch = 1;

    public bool loop;
    
    public AudioMixerGroup outputAudioMixerGroup;
    
    [HideInInspector]
    public AudioSource source;
}
