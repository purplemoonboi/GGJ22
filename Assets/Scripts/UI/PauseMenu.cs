using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UIManager uiState;

    [Header("Buttons")]
    public Button resumeButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Menu Screens")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject quitMenu;

    // Start is called before the first frame update
    void Start()
    {
        uiState = GameObject.FindObjectOfType<UIManager>();

        resumeButton.onClick.AddListener(CheckPauseMenu);
        settingsButton.onClick.AddListener(Settings);
        quitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CheckPauseMenu();
        }
    }

    private void CheckPauseMenu()
    {
        if (uiState.GetIsPaused())
        {
            ResumeGame();
        }

        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        uiState.SetCurrentActiveMenu(pauseMenu);
    }

    private void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    private void Settings()
    {
        settingsMenu.GetComponent<SettingsMenu>().SetPreviousMenu(pauseMenu);
        uiState.SetCurrentActiveMenu(settingsMenu);
    }

    private void QuitGame()
    {
        quitMenu.GetComponent<QuitMenu>().SetPreviousMenu(pauseMenu);
        uiState.SetCurrentActiveMenu(quitMenu);
    }
}
