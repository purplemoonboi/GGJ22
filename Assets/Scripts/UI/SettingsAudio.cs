using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class SettingsAudio : MonoBehaviour
{
    private UIManager uiState;

    [Header("Buttons")]
    public Button backButton;

    [Header("Menus")]
    public GameObject audioMenu;
    public GameObject settingsMenu;
    public AudioMixer audioMixer;
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        uiState = GameObject.FindObjectOfType<UIManager>();

        backButton.onClick.AddListener(GoBack);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GoBack();
        }
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void GoBack()
    {
        uiState.SetCurrentActiveMenu(settingsMenu);
    }
}
