using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeAudio : MonoBehaviour
{
    public string volumeParameter = "Effects";
    public AudioMixer audioMixer;
    public Slider slider;
    public int multiplier = 1;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        audioMixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }
}
