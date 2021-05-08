using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Script to Manage all In Game Audio

    public static AudioManager Instance;


    public float MasterVolume;

    [SerializeField] private AudioMixer MasterMixer;
    [SerializeField] private AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    //[Header("Arrays for Random Sounds")]
    //[SerializeField] private AudioClip[] randomBounceSounds;
    //[SerializeField] private AudioClip[] randomUpgradeSounds;

    private void Awake()
    {
        if (AudioManager.Instance == null)
        {
            AudioManager.Instance = this;
        }
        else if (AudioManager.Instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this);

        // Initializes the array of Sounds with the Different Values of the Inspector
        foreach (Sound s in sounds)
        {
            s.Source = this.gameObject.AddComponent<AudioSource>();
            s.Source.outputAudioMixerGroup = mixerGroup;

            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;

            s.Source.playOnAwake = false;
        }
    }

    private void Start()
    {
        // Sets the Volume of the Mixer with the MasterVolume float
        MasterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20);
    }

    /// <summary>
    /// Plays a Sound in the Sound array by Index
    /// </summary>
    /// <param name="_audioname"></param>
    public void Play(string _audioname)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);
        s.Source.Play();
    }

    ///// <summary>
    ///// Plays a Random AudioClip in the given Source by Index
    ///// </summary>
    ///// <param name="_audioname"></param>
    ///// <param name="_audioClipArraySound"></param>
    //public void PlayRandom(string _audioname, string _audioClipArraySound)
    //{
    //    Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);

    //    AudioClip clip = null;

    //    //Choses the Array with the Random Sound given by a String
    //    //if (_audioClipArraySound == "Bounce")
    //    //{
    //    //    clip = randomBounceSounds[Random.Range(0, randomBounceSounds.Length)];
    //    //}
    //    //else if (_audioClipArraySound == "Upgrade")
    //    //{
    //    //    clip = randomUpgradeSounds[Random.Range(0, randomUpgradeSounds.Length)];
    //    //}


    //    // Replaces the audioClip in the Souce with the Random one that has been chosen 
    //    if (clip != null)
    //    {
    //        s.Source.clip = clip;
    //    }

    //    s.Source.Play();
    //}

    /// <summary>
    /// Stop the Audio from a Source by String Index
    /// </summary>
    /// <param name="_audioname"></param>
    public void Stop(string _audioname)
    {
        Sound s = System.Array.Find(sounds, sound => sound.Name == _audioname);
        s.Source.Stop();
    }

    /// <summary>
    /// Set the Master Volume and the value in the Mixer
    /// </summary>
    /// <param name="_volume"></param>
    public void SetMasterVolume(float _volume)
    {
        MasterVolume = _volume;
        MasterMixer.SetFloat("MasterVolume", Mathf.Log10(MasterVolume) * 20);
    }

    /// <summary>
    /// Stops all Playing Sounds
    /// </summary>
    public void StopAllSounds()
    {
        foreach (Sound mySound in sounds)
        {
            Stop(mySound.Name);
        }
    }
}
