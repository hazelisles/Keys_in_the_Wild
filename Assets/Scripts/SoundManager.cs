using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    static public SoundManager Instance { get; private set; } = null;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioMixer mixer;

    private float sfxVolume = 1.0f;
    private float musicVolume = 1.0f;

    // a property to get/set sfx volume
    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = Mathf.Clamp(value, 0.0f, 1.0f); // limit vol range
            mixer.SetFloat("SfxVolume", LinearToLog(sfxVolume));     // set mixer sfx vol
        }
    }
    // a property to get/set sfx volume
    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = Mathf.Clamp(value, 0.0f, 1.0f);
            mixer.SetFloat("MusicVolume", LinearToLog(musicVolume));
        }
    }

    // For save and restore volume
    const string MUSIC_VOL = "MusicVol";    // PlayerPrefs key for saving music volume
    const string SFX_VOL = "SfxVol";        // PlayerPrefs key for saving sfx volume

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        // Restore volume slider values from PlayerPrefs
        MusicVolume = PlayerPrefs.GetFloat(MUSIC_VOL, 1f);
        SfxVolume = PlayerPrefs.GetFloat(SFX_VOL, 1f);      // if not found, use 1f
    }

    private void OnDestroy()
    {
        // Save volume slider values to PlayerPrefs
        PlayerPrefs.SetFloat(MUSIC_VOL, musicVolume);
        PlayerPrefs.SetFloat(SFX_VOL, sfxVolume);
    }

    // Play a sfx clip (fire & forget & one shot sound effect)
    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        sfxSource.PlayOneShot(clip, volume);
    }

    // Play a music clip
    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // convert from linear to logarithmic scale (0.0-1.0 to decibels)
    private float LinearToLog(float value)
    {
        return Mathf.Log10(value) * 20;
    }
}
