using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Dropdown numberOfPlayerDropdown;
    public Toggle autoTurnCamToggle;
    public Slider volumeSlider;

    void Start()
    {
        if (MainManager.Instance.number_of_player == 2)
        {
            numberOfPlayerDropdown.value = 0;
        }
        else if (MainManager.Instance.number_of_player == 4)
        {
            numberOfPlayerDropdown.value = 1;
        }
        
        autoTurnCamToggle.isOn = MainManager.Instance.autoRotateCam;
        volumeSlider.value = MainManager.Instance.volume;
    } 

    public void SetNumberOfPlayer(int numberIndex)
    {
        if (numberIndex == 0) MainManager.Instance.number_of_player = 2;
        else MainManager.Instance.number_of_player = 4;
    }

    public void SetAutoTurnCam(bool isAutoTurn)
    {
        MainManager.Instance.autoRotateCam = isAutoTurn;
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20 );
        MainManager.Instance.volume = (int)volume;
    }
}
