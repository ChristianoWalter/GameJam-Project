using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public static InterfaceController instance;

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

    [Header("UI Variables")]
    public GameObject[] spiritsCaught;

    [Header("Screens Variables")]
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject configScreen;
    [SerializeField] GameObject creditsScreen;

    [Header("Screens Main Buttons")]
    [SerializeField] GameObject pauseBtn;
    [SerializeField] GameObject configBtn;
    [SerializeField] GameObject creditsBtn;

    Screens currentScreen;
    Screens lastScreen;

    private void Awake()
    {
        instance = this;
    }

    #region Screens Management
    public void SwitchScreen(int screenIndex)
    {
        lastScreen = currentScreen;
        currentScreen = (Screens)screenIndex;
        menuScreen.SetActive(false);
        gameScreen.SetActive(false);
        pauseScreen.SetActive(false);
        configScreen.SetActive(false);
        creditsScreen.SetActive(false);
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(null);

        switch (currentScreen)
        {
            case Screens.menuScreen:
                menuScreen.SetActive(true);
                break;
            case Screens.gameScreen:
                gameScreen.SetActive(true);
                break;
            case Screens.pauseScreen: 
                pauseScreen.SetActive(true);
                Cursor.visible = true;
                EventSystem.current.SetSelectedGameObject(pauseBtn);
                break;
            case Screens.configScreen:
                configScreen.SetActive(true);
                Cursor.visible = true;
                EventSystem.current.SetSelectedGameObject(configBtn);
                break;
            case Screens.creditsScreen:
                creditsScreen.SetActive(true);
                Cursor.visible = true;
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
    #endregion

    #region UI Management
    public void SetSpiritsCaught(int spiritNumber)
    {
        spiritsCaught[spiritNumber - 1].SetActive(true);
    }

    public void DesactiveSpiritsCaught()
    {
        for (int i = 0; i < spiritsCaught.Length; i++)
        {
            spiritsCaught[i].SetActive(false);
        }
    }
    #endregion
}
