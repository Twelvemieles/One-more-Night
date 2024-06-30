using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    [SerializeField] private DialogPanel dialogPanel;

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
        gameplayUIScreen.SetActive(false);
        HideMainMenu();
        HideEndGameScreen();
        Invoke("ShowFirstDialogTutorial", 0.2f);
    }
    private void ShowFirstDialogTutorial()
    {

        GameManager.inst.PauseGameplay();
        ShowDialog("Tutorial_1", () =>
        {

            gameplayUIScreen.SetActive(true);
            GameManager.inst.ResumeGameplay();
            GameManager.inst.EnemiesManager.OnGameStart();
        }
        );
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
    #region DialogPanel
    public void ShowDialog(string dialogID, Action OnFinishDialog)
    {
        GameManager.inst.PauseGameplay();
        dialogPanel.Init(dialogID, OnFinishDialog);
    }
    #endregion
}
