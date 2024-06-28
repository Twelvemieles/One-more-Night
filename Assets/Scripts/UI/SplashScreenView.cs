using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenView : MonoBehaviour
{
  public void ShowMainMenuScreen()
    {
        GameManager.inst.UIManager.ShowMainMenu();
    }
}
