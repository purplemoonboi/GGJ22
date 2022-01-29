using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private UIManager uiState;

    [Header("Buttons")]
    public Button videoButton, audioButton, helpButton, backButton;

    [Header("Menus")]
    public GameObject previousMenu, settingsMenu, videoMenu, audioMenu, helpMenu;

    void Start()
    {
        uiState = GameObject.FindObjectOfType<UIManager>();

        videoButton.onClick.AddListener(Video);
        audioButton.onClick.AddListener(Audio);
        helpButton.onClick.AddListener(Help);
        backButton.onClick.AddListener(GoBack);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            GoBack();
        }
    }

    public void SetPreviousMenu(GameObject menu)
    {
        previousMenu = menu;
    }

    private void Video()
    {
        uiState.SetCurrentActiveMenu(videoMenu);
    }

    private void Audio()
    {
        uiState.SetCurrentActiveMenu(audioMenu);
    }

    private void Help()
    {
        uiState.SetCurrentActiveMenu(helpMenu);
    }

    public void GoBack()
    {
        uiState.SetCurrentActiveMenu(previousMenu);
    }
}
