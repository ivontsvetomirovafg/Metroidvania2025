using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource musicSource;
    private AudioSource ambientSource;
    private AudioSource[] sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        musicSource = gameObject.AddComponent <AudioSource>();
        musicSource.loop = true;
        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.loop = true;
        sfxSource = new AudioSource[5];
        for(int i = 0; i < sfxSource.Length; i++)
        {
            sfxSource[i] = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayMusic(AudioClip _music, float _volume=1)
    {
        musicSource.clip = _music;
        musicSource.volume = _volume;
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void FadeOutMusic(float _speed)
    {
        StartCoroutine(FadeOutAudio(musicSource, _speed));

    }
    IEnumerator FadeOutAudio (AudioSource source, float _speed)
    {
        float volume = source.volume;
        while(volume>0)
        {
            volume -= Time.deltaTime * _speed;
            source.volume = volume;
            yield return null;
        }
    }

    public void PlayAmbient(AudioClip _ambient, float _volume = 1)
    {
        ambientSource.clip = _ambient;
        ambientSource.volume = _volume;
        ambientSource.Play();
    }
    public void StopAmbient()
    {
        ambientSource.Stop();
    }
    public void FadeOutAmbient(float _speed)
    {
        StartCoroutine(FadeOutAudio(ambientSource, _speed));
    }
    public void PlaySFX(AudioClip _sfx, float _volume = 1)
    {
        for(int i = 0; i<sfxSource.Length; i++)
        {
            if (sfxSource[i].isPlaying==false)
            {
                sfxSource[i].clip = _sfx;
                sfxSource[i].volume = _volume;
                sfxSource[i].Play();
                break;
            }
        }
    }
}
