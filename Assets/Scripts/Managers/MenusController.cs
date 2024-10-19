using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{
    public enum Screens
    {
        menuScreen,
        gameScreen,
        configScreen,
        creditsScreen
    }

    bool isPaused;

    [Header("Screens Variables")]
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject configScreen;
    [SerializeField] GameObject creditsScreen;

    [Header("Screens Main Buttons")]
    [SerializeField] GameObject menuBtn;
    [SerializeField] GameObject gameBtn;
    [SerializeField] GameObject configBtn;
    [SerializeField] GameObject creditsBtn;

    Screens currentScreen;
    Screens lastScreen;

    public void SwitchScreen(int screenIndex)
    {
        lastScreen = currentScreen;
        currentScreen = (Screens)screenIndex;
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        configScreen.SetActive(false);
        creditsScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

        switch (currentScreen)
        {
            case Screens.menuScreen:
                menuScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(menuBtn);
                break;
            case Screens.configScreen:
                configScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(gameBtn);
                break;
            case Screens.gameScreen:
                gameScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(configBtn);
                break;
            case Screens.creditsScreen:
                creditsScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(creditsBtn);
                break;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if (isPaused)
        {
            SwitchScreen((int)Screens.gameScreen);
            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            SwitchScreen((int)Screens.configScreen);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void SwitchToLastScreen()
    {
        SwitchScreen((int)lastScreen);
    }
}
