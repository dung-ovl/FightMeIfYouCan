using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : GameMonoBehaviour
{

    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioSource _musicSource;

    public List<Sound> sounds;
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        foreach (Sound sound in sounds)
        {
            sound.source =  gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;

            sound.source.pitch = sound.pitch;
        }
    }
    public void PlaySound(string name)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.Play();
    }

    public void PlaySoundOneShot(string name, float volumeScale)
    {
        Sound s = sounds.Find(sound => sound.name == name);
        s.source.PlayOneShot(s.clip, volumeScale);
    }

    public void TurnOffMusic()
    {
        _musicSource.Stop();
    }

    public void TurnOnMusic()
    {
        _musicSource.Play();
    }
}
