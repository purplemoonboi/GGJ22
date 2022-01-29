using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private UIManager uiState;

    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject quitMenu;
    public FadeTransition fadeTransition;

    void Start()
    {
        uiState = GameObject.FindObjectOfType<UIManager>();
    }

    private void OnEnable()
    {
        uiState.SetInMainMenu(true);
    }

    private void OnDisable()
    {
        uiState.SetInMainMenu(false);
    }

    public void PlayGame()
    {
        fadeTransition.fadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToSettings()
    {
        settingsMenu.GetComponent<SettingsMenu>().SetPreviousMenu(mainMenu);
        uiState.SetCurrentActiveMenu(settingsMenu);
    }

    public void QuitGame()
    {
        quitMenu.GetComponent<QuitMenu>().SetPreviousMenu(mainMenu);
        uiState.SetCurrentActiveMenu(quitMenu);
    }
}
