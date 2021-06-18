/*
* File:			ChangeAudio.cs
* Author:		Jacob Cooper (s200503@students.aie.edu.au)
* Edit Dates:
*	First:		17/06/2021
*	Last:		18/06/2021
* Summary:
*	Used for changing and manipulating audio in the scene.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace NeonDiskVR.Menus
{
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
}