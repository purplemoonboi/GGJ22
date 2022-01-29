using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    private UIManager uiState;

    [Header("Buttons")]
    public Button yesButton;
    public Button noButton;

    [Header("Menus")]
    public GameObject previousMenu;
    public GameObject quitMenu;

    // Start is called before the first frame update
    void Start()
    {
        uiState = GameObject.FindObjectOfType<UIManager>();

        yesButton.onClick.AddListener(YesQuit);
        noButton.onClick.AddListener(NoQuit);

        quitMenu.SetActive(true);
        previousMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GoBack();
    }

    public void SetPreviousMenu(GameObject menu)
    {
        previousMenu = menu;
    }

    private void YesQuit()
    {
        Debug.Log("Quitting the application...");
        Application.Quit();
    }

    private void NoQuit()
    {
        uiState.SetCurrentActiveMenu(previousMenu);
    }

    public void GoBack()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            previousMenu.SetActive(true);
            quitMenu.SetActive(false);
        }
    }
}
