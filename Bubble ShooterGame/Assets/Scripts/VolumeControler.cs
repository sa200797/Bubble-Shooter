using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class VolumeControler : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixer SFX_mixer;

 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);

       
    }
    public void SetSFXLevel(float sliderValue)
    {
        mixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
}
