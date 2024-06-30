using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgameScreenView : MonoBehaviour
{
    [SerializeField]private GameObject GoodEndgameScreen;
    [SerializeField]private GameObject BadEndgameScreen;
    [SerializeField] private List<TextMeshProUGUI> finalScoreTexts;

    public void ShowEndGame(bool hasWin)
    {
        if(hasWin)
        {
            GoodEndgameScreen.SetActive(true);
            BadEndgameScreen.SetActive(false);
        }
        else{
            GoodEndgameScreen.SetActive(false);
            BadEndgameScreen.SetActive(true);
        }
        string finalScoreText = string.Concat("<sprite name=", ResourcesManager.ResourceType.Coin.ToString(), "> ",GameManager.inst.ResourcesManager.CoinsCount.ToString() ) ;
        SetFinalScoreTexts(finalScoreText);
    }
    private void SetFinalScoreTexts(string text)
    {
        foreach(TextMeshProUGUI textComp in finalScoreTexts)
        {
            textComp.text = text;
        }
    }
    public void ClickRestart()
    {
        GameManager.inst.RestartGame();
    }

}
