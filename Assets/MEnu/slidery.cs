using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
public class slidery : MonoBehaviour
{
    #region Ustawienia muzyki
    public Slider volumeMusicSlider;
    public AudioMixerGroup musicAudioGroup;
    public Color lowVolumeColor = Color.green;
    public Color mediumVolumeColor = Color.yellow;
    public Color highVolumeColor = Color.red;

    public Image sliderFill;

    [SerializeField] private float musicVolume;
    #endregion


    private void Awake()
    {

        #region Ustawienia muzyki
        volumeMusicSlider.value = 1;// domyslne
        if (PlayerPrefs.HasKey("Volume"))
        {
            musicVolume = PlayerPrefs.GetFloat("Volume");
            musicAudioGroup.audioMixer.SetFloat("Volume", Mathf.Log10(musicVolume) * 20);
            volumeMusicSlider.value = musicVolume;
        }
        else
        {
            musicVolume = 1;
            volumeMusicSlider.value = musicVolume;
        }
        #endregion
    }


    void Start()
    {

        volumeMusicSlider.value = musicVolume;
        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);
        sliderFill.color = highVolumeColor;
        SetMusicVolume(musicVolume);
        ChangeColorByVolume(musicVolume, volumeMusicSlider);
    }

    private void SetMusicVolume(float volume)
    {
        volume = Mathf.Clamp(volume, (float)0.001, 1);
        musicAudioGroup.audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
        musicVolume = volume;
        ChangeColorByVolume(volume, volumeMusicSlider);
        PlayerPrefs.SetFloat("Volume", musicVolume);
        PlayerPrefs.Save();
    }


    void ChangeColorByVolume(float volume1, Slider x)
    {
        float normalizedVolume = Mathf.InverseLerp(x.minValue, x.maxValue, volume1);
        Color newColor = Color.Lerp(lowVolumeColor, highVolumeColor, normalizedVolume);
        x.value = volume1;
        sliderFill.color = newColor;
    }

    public float GetSoundVolume()
    {
        return 1;//return SoundVolume;
    }
    public float GetMusicVolume()
    {
        return 1;//MusicVolume;
    }
}
