using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class soundManager : MonoBehaviour
{
    public static soundManager Instance { get; private set; }

    public AudioSource AudioSource;
    public AudioClip aud;
    public enum Sound
    {
        Strzal,
        Zniszczenie,
        //Poruszanie,
        //EnemyDie,
        //EnemyHit,
        //GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private float volume = .5f;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat("soundVolume", .5f);

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound], volume);
    }
    private void Start()
    {
        invaders.instance.Boss += Instance_Boss1;
    }

    private void Instance_Boss1(object sender, System.EventArgs e)
    {
        AudioSource.clip = aud;
        AudioSource.Play();
    }

   

    public void IncreaseVolume()
    {
        volume += .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public void DecreaseVolume()
    {
        volume -= .1f;
        volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("soundVolume", volume);
    }

    public float GetVolume()
    {
        return volume;
    }
  
}
