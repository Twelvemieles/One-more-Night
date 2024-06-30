using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void VoidDelegate();
    public event VoidDelegate OnResumedGameplay;
    public event VoidDelegate OnRestartGameplay;
    public event VoidDelegate OnPausedGameplay;


    public static GameManager inst;

    public UIManager UIManager;
    public ResourcesManager ResourcesManager;
    public EnemiesManager EnemiesManager;
    public TutorialManager TutorialManager;
    public StoreManager storeManager;
    public AudioManager AudioManager;

    private void OnEnable()
    {
        if(inst == null)
        {
            inst = this;
            StartGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void StartGame()
    {
        UIManager.ShowSplashScreen();
        ResourcesManager.SetupInitialResourcesValues();
    }
    public void StartGamePlay()
    {
        UIManager.HideMainMenu();
        UIManager.ShowDialog("Intro", () =>
         {
             UIManager.ShowGameplayScreen();
             EnemiesManager.OnGameStart();
         });
    }
    public void EndGame(bool hasWin)
    {
        UIManager.ShowEndGameScreen(hasWin);
        PauseGameplay();
    }
    public void RestartGame()
    {
        UIManager.ShowGameplayScreen();
        UIManager.HideEndGameScreen();
        OnRestartGameplay?.Invoke();
        ResourcesManager.SetupInitialResourcesValues();
    }
    public void PauseGameplay()
    {
        OnPausedGameplay?.Invoke();
    }
    public void ResumeGameplay()
    {
        OnResumedGameplay?.Invoke();
    }
}
