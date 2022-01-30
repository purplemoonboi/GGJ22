using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private controller playerController;

    public bool inMainMenu, isPaused, inMenu, cursorActive;
    public GameObject hud;
    public GameObject currentMenu;
    public GameObject[] menus;

    void Start()
    {
        playerController = GameObject.FindObjectOfType<controller>();

        isPaused = false;
        inMenu = false;
        cursorActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inMainMenu)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isPaused = !isPaused;
            }
        }

        else
        {
            inMenu = true;
            currentMenu.SetActive(true);
        }

        UpdateMenus();

        if (inMenu)
        {
            SetCursorActive(true);
        }

        else
        {
            SetCursorActive(false);
        }

        if (!inMainMenu)
        {
            if (isPaused)
            {
                hud.SetActive(false);
                playerController.enabled = false;
            }

            else
            {
                hud.SetActive(true);
                playerController.enabled = true;
            }
        }
    }

    public void UpdateMenus()
    {
        int totalActiveMenus = 0;

        /* If there is at least one active menu, the game timer will stop and remain in a 'menu state.'
         * If the total number of active menus is none, the in-game time will resume.
         */

        if(inMainMenu)
        {
            return;
        }

        foreach (GameObject current in menus)
        {
            if (current.activeSelf)
            {
                totalActiveMenus++;

                inMenu = true;
                isPaused = true;
                Time.timeScale = 0.0f;
            }

            else if (!current.activeSelf && totalActiveMenus == 0)
            {
                inMenu = false;
                isPaused = false;
                Time.timeScale = 1.0f;
            }
        }
    }

    public void SetCurrentActiveMenu(GameObject activeMenu)
    {
        currentMenu = activeMenu;

        for (int counter = 0; counter < menus.Length; counter++)
        {
            if (menus[counter] == currentMenu)
            {
                menus[counter].SetActive(true);
            }

            else if (menus[counter] != currentMenu)
            {
                menus[counter].SetActive(false);
            }
        }
    }

    public void SetCursorActive(bool active)
    {
        cursorActive = active;

        if (cursorActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SetInMainMenu(bool menuStatus)
    {
        inMainMenu = menuStatus;
    }

    public void SetInMenu(bool menuStatus)
    {
        inMenu = menuStatus;
    }

    public bool GetInMenu()
    {
        return inMenu;
    }

    public void SetIsPaused(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
