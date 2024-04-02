using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundFXSlider;
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Button backButton;


    // Start is called before the first frame update
    void Start()
    {
        // masterSlider.onValueChanged.AddListener(SetMasterVolume);
        // musicSlider.onValueChanged.AddListener(SetMusicVolume);
        // soundFXSlider.onValueChanged.AddListener(SetSoundFXVolume);

        // masterSlider.OnSelect(OnBackButtonDown);

        masterSlider.onValueChanged.AddListener((value) => {
            audioMixer.SetFloat("masterVol", value);
        });
        musicSlider.onValueChanged.AddListener((value) => {
            audioMixer.SetFloat("musicVol", value);
        });
        soundFXSlider.onValueChanged.AddListener((value) => {
            audioMixer.SetFloat("soundFXVol", value);
        });

        backButton.onClick.AddListener(()=> { SoundManager.instance.PlaySound("ButtonSFX"); });
    }

    void OnEnable(){
        OnOptionsOpen();
    }

    void OnOptionsOpen(){
        float volHolder;

        audioMixer.GetFloat("masterVol", out volHolder);
        masterSlider.value = volHolder;

        audioMixer.GetFloat("musicVol", out volHolder);
        musicSlider.value = volHolder;

        audioMixer.GetFloat("soundFXVol", out volHolder);
        soundFXSlider.value = volHolder;
    }
}
