using UnityEngine.Audio;
using UnityEngine;
using System;
public class ManagerAudio : MonoBehaviour {

    public Sound[] sounds;
    public Sound[] themeSounds;
    public static ManagerAudio instance;

    public AudioMixerGroup mixerGroup;

    // Use this for initialization
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
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixerGroup;
            s.source.spatialBlend = s.spatialBlend;
            //s.source.spatialBlend = 1;
        }
        foreach (Sound s in themeSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.name = s.name;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixerGroup;
        }

    }
    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));
        return s.source.isPlaying;
        
    }
    public void Start()
    {
        
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name.Equals(name));

        if (s == null)
            Debug.LogWarning("Error, sound: " + name + " not found");

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Play();
            
        
    }

    public void PlayTheme(string name)
    {
        Sound s = Array.Find(themeSounds, sound => sound.name.Equals(name));

        if (s == null)
            Debug.LogWarning("Error, sound: " + name + " not found");
        else
        {
            s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            s.source.Play();
        }
    }
    public void StopTheme(string name)
    {
        Sound s = Array.Find(themeSounds, sound => sound.name.Equals(name));

        if (s == null)
            Debug.LogWarning("Error, sound: " + name + " not found");
        else
        {
            s.source.Stop();
            Debug.Log("Stopping " + name);
        }
    }
    // Update is called once per frame
    void Update () {
    }
}
