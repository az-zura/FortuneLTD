using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public Sound(string name, AudioClip clip, float vol, float pitch, AudioSource source, bool loop, GameObject onGameObject)
    {
        this.name = name;
        this.clip = clip;
        this.volume = vol;
        this.pitch = pitch;
        this.source = source;
        this.loop = loop;
        this.attachedToGameObject = onGameObject;
    }
    
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;

    public GameObject attachedToGameObject;

    [HideInInspector] public bool currentlyFading = false;
}
