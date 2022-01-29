using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsVideo : MonoBehaviour
{
    private UIManager uiState;
    private int defaultResolution;
    private Resolution[] resolutions;
    private List<string> resolutionOptions;

    private bool checkIsFullscreen;
    public Resolution currentResolution;
    public int currentGraphicsQualityLevel;

    [Header("Buttons")]
    public Button lowQuality;
    public Button mediumQuality;
    public Button highQuality;
    public Button backButton;

    [Header("Menus")]
    public GameObject videoMenu;
    public GameObject settingsMenu;
    public Dropdown resolutionDropdown;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionOptions = new List<string>();

        defaultResolution = 0;
        for (int counter = 0; counter < resolutions.Length; counter++)
        {
            string option = resolutions[counter].width + " x " + resolutions[counter].height;
            resolutionOptions.Add(option);

            if (resolutions[counter].width == Screen.currentResolution.width && resolutions[counter].height == Screen.currentResolution.height)
            {
                defaultResolution = counter;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = defaultResolution;
        resolutionDropdown.RefreshShownValue();

        uiState = GameObject.FindObjectOfType<UIManager>();

        lowQuality.onClick.AddListener(SetQualityLow);
        mediumQuality.onClick.AddListener(SetQualityMedium);
        highQuality.onClick.AddListener(SetQualityHigh);

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

    public void SetFullscreen(bool isFullscreen)
    {
        checkIsFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int newResolution)
    {
        Resolution resolution = resolutions[newResolution];
        currentResolution = resolution;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQualityLow()
    {
        currentGraphicsQualityLevel = 0;
        QualitySettings.SetQualityLevel(0);
    }

    public void SetQualityMedium()
    {
        currentGraphicsQualityLevel = 1;
        QualitySettings.SetQualityLevel(1);
    }

    public void SetQualityHigh()
    {
        currentGraphicsQualityLevel = 2;
        QualitySettings.SetQualityLevel(2);
    }

    public void GoBack()
    {
        uiState.SetCurrentActiveMenu(settingsMenu);
    }
}
