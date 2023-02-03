using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

public class UtilSound : Singleton<UtilSound>
{
    [SerializeField]
    private AudioMixerGroup _audioMixer = null;

    public AudioClip[] clips;

    private List<GameObject> sounds = null;
    private Dictionary<string, AudioClip> _clipsDictionary;

    private bool _focus = true;

    private void Start()
    {
        sounds = new List<GameObject>();
        _clipsDictionary = new Dictionary<string, AudioClip>();
        foreach (AudioClip ac in clips)
        {
            _clipsDictionary.Add(ac.name, ac);
        }
        _focus = true;
    }

    private void Update()
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        {
            if (!sounds[i].GetComponent<AudioSource>().isPlaying && _focus)
            {
                Destroy(sounds[i]); // Destroy the AudioSource
                sounds.RemoveAt(i); // Remove from the list
            }
        }
    }

    private AudioClip GetFamilyClip(string name)
    {
        List<AudioClip> list = new List<AudioClip>();
        foreach (KeyValuePair<string, AudioClip> pair in _clipsDictionary)
        {
            if (pair.Key.Contains(name))
            {
                list.Add(pair.Value);
            }
        }
        if (list.Count == 0)
        {
            Debug.LogError("[UtilSound] Error. No clips found from " + name + " family");
            return null;
        }
        int rand = Random.Range(0, list.Count);
        return list[rand];
    }

    private AudioClip CreateClip(string name)
    {
        AudioClip clip = null;
        if (_clipsDictionary.ContainsKey(name))
        {
            clip = _clipsDictionary[name];
        }

        // Try getting a family clip
        if (clip == null)
        {
            clip = GetFamilyClip(name);
        }

        return clip;
    }

    private AudioSource CreateAndPlayNewAudioSource(AudioClip clip, float volume, bool loop)
    {
        GameObject newObject = new GameObject(); // New scene object
        AudioSource newSource = newObject.AddComponent<AudioSource>(); // Create a new AudioSouce and set it to the new object
        newObject.name = name; // Assign the given clip name
        newSource.clip = clip; // Assign clip to new AudioSource
        newSource.volume = volume;
        newSource.loop = loop; // Assign given loop property
        newSource.spatialBlend = 0.0f;
        newSource.outputAudioMixerGroup = _audioMixer;
        newSource.transform.parent = gameObject.transform; // UtilSound is the parent of the new object

        return newSource;
    }

    public void PlaySound(string name, float volume = 1.0f, bool loop = false)
    {
        AudioClip clip = CreateClip(name);
        AudioSource newSource = CreateAndPlayNewAudioSource(clip, volume, loop);
        newSource.Play(); // Play the sound
        sounds.Add(newSource.gameObject); // Store the new AudioSource
    }

    public void StopSound(string name)
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        {
            if (sounds[i].name == name)
            {
                Destroy(sounds[i]); // Destroy the AudioSource
                sounds.RemoveAt(i); // Remove from the list
                break; // Just the oldest sound with that name
            }
        }
    }

    public void StopAllSounds()
    {
        if (sounds == null) { return; }
        for (int i = 0; i < sounds.Count; ++i)
        {
            Destroy(sounds[i]); // Destroy the AudioSource
            sounds.RemoveAt(i); // Remove from the list
        }
    }

    public bool IsPlaying(string name)
    {
        if (sounds == null) { return false; }
        for (int i = 0; i < sounds.Count; ++i)
        {
            if (sounds[i].name == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsPlayingFamilySound(string name)
    {
        if (sounds == null) { return false; }
        for (int i = 0; i < sounds.Count; ++i)
        {
            if (sounds[i].name.Contains(name))
            {
                return true;
            }
        }
        return false;
    }

    public float GetClipLength(string name)
    {
        if (_clipsDictionary.ContainsKey(name))
        {
            AudioClip clip = _clipsDictionary[name];
            return clip.length;
        }
        return 0.0f;
    }

    void OnApplicationFocus(bool focus)
    {
        _focus = focus;
    }
}