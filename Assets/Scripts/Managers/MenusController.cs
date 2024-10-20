using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenusController : MonoBehaviour
{
    public static MenusController instance;

    public enum Screens
    {
        menuScreen,
        gameScreen,
        pauseScreen,
        configScreen,
        creditsScreen
    }

    bool isPaused;
    [HideInInspector] public bool isInGame;

    [Header("Screens Variables")]
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject configScreen;
    [SerializeField] GameObject creditsScreen;

    [Header("Screens Main Buttons")]
    [SerializeField] GameObject menuBtn;
    [SerializeField] GameObject gameBtn;
    [SerializeField] GameObject pauseBtn;
    [SerializeField] GameObject configBtn;
    [SerializeField] GameObject creditsBtn;

    Screens currentScreen;
    Screens lastScreen;

    private void Awake()
    {
        instance = this;
    }

    public void SwitchScreen(int screenIndex)
    {
        lastScreen = currentScreen;
        currentScreen = (Screens)screenIndex;
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        configScreen.SetActive(false);
        creditsScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

        switch (currentScreen)
        {
            case Screens.menuScreen:
                menuScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(menuBtn);
                break;
            case Screens.gameScreen:
                gameScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(configBtn);
                break;
            case Screens.pauseScreen: 
                pauseScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(pauseBtn);
                break;
            case Screens.configScreen:
                configScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(gameBtn);
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
        if (!GameManager.instance.gameStarted) return;
        if (isPaused)
        {
            SwitchScreen((int)Screens.gameScreen);
            Time.timeScale = 1f;
            isInGame = true;
            isPaused = false;
        }
        else
        {
            SwitchScreen((int)Screens.configScreen);
            Time.timeScale = 0f;
            isInGame = false;
            isPaused = true;
        }
    }

    public void SwitchToLastScreen()
    {
        if (isInGame || !GameManager.instance.gameStarted) return;

        if (currentScreen == Screens.pauseScreen) PauseUnpause();
        else SwitchScreen((int)lastScreen);
    }
}
