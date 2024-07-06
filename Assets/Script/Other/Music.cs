using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixerBackGr;
    [SerializeField] AudioMixer audioMixerMusic;

    [SerializeField] Slider sliderBackGr;
    [SerializeField] Slider sliderMusic;


     void Start() {
        SetVolumeBackGr();
        SetVolumeMusic();
    }
    public void SetVolumeBackGr(){
        float volume = sliderBackGr.value;
        audioMixerBackGr.SetFloat("volume",volume);
    }
    public void SetVolumeMusic(){
        float volume = sliderMusic.value;
        audioMixerMusic.SetFloat("volume",volume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
