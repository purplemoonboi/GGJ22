using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsHelp : MonoBehaviour
{
    private UIManager uiState;

    [Header("Buttons")]
    public Button backButton;

    [Header("Menus")]
    public GameObject helpScreen;
    public GameObject settingsMenu;

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

    public void GoBack()
    {
        uiState.SetCurrentActiveMenu(settingsMenu);
    }
}
