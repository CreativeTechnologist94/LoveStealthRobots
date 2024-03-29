using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixerGroup;
    
    // Start is called before the first frame update
    void Start()
    {
        var slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(SliderValueChanged);

        var val = PlayerPrefs.GetFloat(_mixerGroup, 0.75f);
        slider.value = val;
    }

    private void SliderValueChanged(float newValue)
    {
        _mixer.SetFloat(_mixerGroup, Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat(_mixerGroup, newValue);
        PlayerPrefs.Save();
    }
}
