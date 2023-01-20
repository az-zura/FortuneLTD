using UnityEngine.Audio;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // this script is on the AudioManagerPrefab, which should be in every scene !!!!!!!!!!

    public List<Sound> sounds;
    public Sound[] themes;

    [SerializeField][Range(0.01f, 1f)] private float defaultMusicVol = 1f;
    [SerializeField][Range(0.01f, 1f)] private float defaultSoundVol = 1f;
    
    [SerializeField] private AudioMixerGroup musicMixer;
    [SerializeField] private AudioMixerGroup soundMixer;

    [HideInInspector] public Sound currentTheme;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            if (!s.attachedToGameObject)
            {
                s.attachedToGameObject = gameObject;
            }
            s.source = s.attachedToGameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = soundMixer;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }

        foreach (Sound s in themes)
        {
            s.attachedToGameObject = gameObject; // Themes aer always on the AudioMixer
            s.source = s.attachedToGameObject.AddComponent<AudioSource>();
            
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = musicMixer;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        currentTheme = themes[0];
        SetMusicVol(PlayerPrefs.GetFloat("musicVol", defaultMusicVol));
        SetSoundVol(PlayerPrefs.GetFloat("soundVol", defaultSoundVol));

        PlayTheme();
    }

    /// <summary>
    ///    <para>Sets the volume of the music with vol between 0.0001 and 1. -- vol should never be 0!!!</para>
    /// </summary>
    /// <param name="vol"></param>
    public void SetMusicVol(float vol)
    {
        PlayerPrefs.SetFloat("musicVol", vol);
        musicMixer.audioMixer.SetFloat("musicVol", Mathf.Log10(vol) * 20);
    }

    /// <summary>
    ///    <para>Sets the volume of the sounds with vol between 0.0001 and 1. -- vol should never be 0!!!</para>
    /// </summary>
    /// <param name="vol"></param>
    public void SetSoundVol(float vol)
    {
        PlayerPrefs.SetFloat("soundVol", vol);
        soundMixer.audioMixer.SetFloat("soundVol", Mathf.Log10(vol) * 20);
    }

    public void PlaySound(string soundName, GameObject onObject = null, GameObject replaceOnGO = null)
    {
        Sound[] snds = Array.FindAll(sounds.ToArray(), sound => sound.name == soundName);
        Debug.Log(snds[0].name);
        if (snds.Length == 0)
        {
            Debug.LogWarning("Sound called \"" + soundName + "\" not found.");
            return;
        }
        
        if (!onObject)
        {
            onObject = gameObject;
        }

        Sound s = Array.Find(snds, sound => sound.attachedToGameObject == onObject); // Sound on the onObject

        bool replace = replaceOnGO != null;
        if (replace && s != null)
        {
            Destroy(s.source);
            s.source = replaceOnGO.AddComponent<AudioSource>();
        } else if (replace && s == null)
        {
            // Add new sound on replace
            s = new Sound(snds[0].name, snds[0].clip, snds[0].volume, snds[0].pitch,
                replaceOnGO.AddComponent<AudioSource>(), snds[0].loop, replaceOnGO);
            sounds.Add(s);
        } else if (!replace)
        {
            if (s == null)
            {
                s = new Sound(snds[0].name, snds[0].clip, snds[0].volume, snds[0].pitch, onObject.AddComponent<AudioSource>(), snds[0].loop, onObject);
                sounds.Add(s);
                
                if (!s.attachedToGameObject)
                {
                    s.attachedToGameObject = gameObject;
                }
                s.source = s.attachedToGameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.outputAudioMixerGroup = soundMixer;

                s.source.volume = s.volume;
                s.source.pitch = s.pitch;

                s.source.loop = s.loop;
            }
        }

        if (PlayerPrefs.GetInt("sound", 1) != 0)
        {
            s.source.Play();
        }
        else
        {
            s.source.Stop();
        }
    }

    public void PlayTheme(string soundName = "")
    {
        bool musicOn = PlayerPrefs.GetInt("music", 1) != 0;

        if (soundName == "")
        {
            soundName = currentTheme.name;
        }
        
        Sound s = currentTheme;
        bool themeChanged = soundName != currentTheme.name;
        if (themeChanged)
        {
            //Change Theme
            s = Array.Find(sounds.ToArray(), sound => sound.name == soundName);
            if (s == null)
            {
                Debug.LogWarning("Sound called \"" + soundName + "\" not found.");
                return;
            }
        }

        if (musicOn)
        {
            if (!s.source.isPlaying)
            {
                StartCoroutine(FadeMusic(s, duration: 1f, 0f));
            }

            if (currentTheme.source.isPlaying && themeChanged)
            {
                // Turn off old theme
                StartCoroutine(FadeMusic(currentTheme, fadeIn: false, duration: 1f, delay: 0));
            }
        }
        else
        {
            //Turn off music
            if (s.source.isPlaying)
            {
                StartCoroutine(FadeMusic(s, fadeIn: false, duration: 1f, delay: 0));
            }
            else
            {
                s.source.Stop();
            }

            if (themeChanged)
            {
                if (currentTheme.source.isPlaying)
                {
                    StartCoroutine(FadeMusic(currentTheme, fadeIn: false, duration: 1f, delay: 0));
                }
                else
                {
                    currentTheme.source.Stop();
                }
            }
        }

        currentTheme = s;
    }

    IEnumerator FadeMusic(Sound m, float duration, float delay, bool fadeIn = true)
    {
        float time = 0;
        float from = fadeIn ? 0 : m.volume;
        float to = fadeIn ? m.volume : 0;

        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }

        if (fadeIn)
        {
            m.source.volume = from;
            m.source.Play();
        }

        while (time < duration)
        {
            //this creates a smooth step function combined with Lerp to smoothly fade
            float t = time / duration;
            t = t * t * (3f - 2f * t);

            m.source.volume = Mathf.Lerp(from, to, t);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        m.source.volume = to;

        if (!fadeIn)
        {
            m.source.Stop();
        }
    }

    public void PlayTestSound(GameObject onObject)
    {
        PlaySound(sounds[0].name, onObject);
    }
}