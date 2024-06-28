using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject endGameScreen;
    #region SplashScreen
    public void ShowSplashScreen()
    {
        splashScreen.SetActive(true);
        HideMainMenu();
        HideGameplayScreen();
    }   
    public void HideSplashScreen()
    {
        splashScreen.SetActive(false);
    }
    #endregion
    #region MainMenu
    public void ShowMainMenu()
    {
        mainMenuScreen.SetActive(true);
        HideSplashScreen();
        HideGameplayScreen();
    } 
    public void HideMainMenu()
    {
        mainMenuScreen.SetActive(false);
    }
    #endregion
    #region GameplayScreen
    public void ShowGameplayScreen()
    {
        gameplayScreen.SetActive(true);
        HideMainMenu();
        HideEndGameScreen();
    }
    public void HideGameplayScreen()
    {
        gameplayScreen.SetActive(false);
    }
    #endregion
    #region EndGameScreen
    public void ShowEndGameScreen(bool hasWin)
    {
        endGameScreen.SetActive(true);
    }
    public void HideEndGameScreen()
    {
        endGameScreen.SetActive(false);
    }
    #endregion
}
