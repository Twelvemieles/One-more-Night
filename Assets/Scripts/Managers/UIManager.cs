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
    [SerializeField] private EndgameScreenView endGameScreen;
    [SerializeField] private StoreScreenView storeScreenView;
    [SerializeField] private HealthSliderView healthSliderView;

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
        endGameScreen.gameObject.SetActive(true);
        endGameScreen.ShowEndGame(hasWin);
    }
    public void HideEndGameScreen()
    {
        endGameScreen.gameObject.SetActive(false);
    }
    #endregion
    #region Store Screen
    public void ShowStoreScreen()
    {
        GameManager.inst.PauseGameplay();
        storeScreen.SetActive(true);
    }
    public void HideStoreScreen()
    {
        GameManager.inst.ResumeGameplay();
        storeScreen.SetActive(false);
    }
    #endregion
    #region sliderValue
    public void SetHealthSliderValue(float value)
    {
        healthSliderView.setSliderValue(value);
    }
    #endregion
}
