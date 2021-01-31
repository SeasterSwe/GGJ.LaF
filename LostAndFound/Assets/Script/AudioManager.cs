using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public float volume = 1;

    public Sound[] sounds;

    public static AudioManager instance;


    void Awake()
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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.clip;
            s.source.volume = volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Menu");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found!");
            return;
        }

        if (s.source == null)
        {
            var newSource = new GameObject($"AudioSource_{s.name}"); // Fancy sträng
            var audioSource = newSource.AddComponent<AudioSource>();

            audioSource.clip = s.clip;
            audioSource.volume = volume;
            audioSource.pitch = s.pitch;
            audioSource.loop = s.loop;
            s.source = audioSource;

        }

        s.source.Play();

        //if (PauseMenu.GameIsPaused)
        //{
        //    s.source.pitch *= .5f;
        //}
    }

    public void Update()
    {
        foreach (Sound s in sounds)
        {
            if (s.source != null)
            {
                s.source.volume = volume;
            }
        }
    }
}
