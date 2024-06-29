using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameplayScreen;
    [SerializeField] private GameObject gameplayUIScreen;
    [SerializeField] private GameObject storeScreen;
    [SerializeField] private GameObject endGameScreen;
    [SerializeField] private StoreScreenView storeScreenView;

    public StoreScreenView  StoreScreenView => storeScreenView;
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
        gameplayUIScreen.SetActive(true);
        HideMainMenu();
        HideEndGameScreen();
        HideStoreScreen();
    }
    public void HideGameplayScreen()
    {
        gameplayScreen.SetActive(false);
        gameplayUIScreen.SetActive(false);
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
    #region Store Screen
    public void ShowStoreScreen()
    {
        storeScreen.SetActive(true);
    }
    public void HideStoreScreen()
    {
        storeScreen.SetActive(false);
    }
    #endregion
}
