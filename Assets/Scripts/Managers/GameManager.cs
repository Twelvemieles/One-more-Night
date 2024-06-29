using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public UIManager UIManager;
    public ResourcesManager ResourcesManager;
    public EnemiesManager EnemiesManager;
    public TutorialManager TutorialManager;
    public StoreManager storeManager;
    public AudioManager AudioManager;

    private void Start()
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
        UIManager.ShowGameplayScreen();
    }
    public void EndGame(bool hasWin)
    {
        UIManager.ShowEndGameScreen(hasWin);
    }
    public void RestartGame()
    {
        UIManager.ShowGameplayScreen();
    }
}
