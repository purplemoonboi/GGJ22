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

    public Slider masterSlider;
    public Slider effectsSlider;
    public Slider musicSlider;

    [Header("Menus")]
    public GameObject audioMenu;
    public GameObject settingsMenu;
    public AudioMixer audioMixer;
    public static float masterVolume = 1.0f;
    public static float effectsVolume = 1.0f;
    public static float musicVolume = 1.0f;

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

    private void OnEnable()
    {
        //masterVolume = PlayerPrefs.GetFloat("MasterVolume");
        //musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        //effectsVolume = PlayerPrefs.GetFloat("EffectsVolume");
        //
        //masterSlider.value = masterVolume;
        //musicSlider.value = musicVolume;
        //effectsSlider.value = effectsVolume;
    }

    private void OnDisable()
    {
        //PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        //PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        //PlayerPrefs.SetFloat("EffectsVolume", effectsVolume);
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
