using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreenView : MonoBehaviour
{
    [SerializeField] private int bulletsCost;
    [SerializeField] private int gasLanternCost;
    [SerializeField] private int mineralTradeCost;
    [SerializeField] private int slimeTradeCost;
    public void CloseStorePanel()
    {
        GameManager.inst.UIManager.HideStoreScreen();
    }
    public void BuyBullets()
    {
        if(GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Coin, bulletsCost))
        {
            GameManager.inst.ResourcesManager.ModifyResourceValue(ResourcesManager.ResourceType.Bullet, 10);
        }
    }
    public void BuyLanternGas()
    {
        if(GameManager.inst.ResourcesManager.LanternGasValue < 1f && GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Coin, gasLanternCost))
        {
            GameManager.inst.ResourcesManager.ModifyResourceValue(ResourcesManager.ResourceType.LanternGas, 1f);
        }
    }
    public void TradeMineral()
    {
        if (GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Mineral, mineralTradeCost))
        {
            GameManager.inst.ResourcesManager.ModifyResourceValue(ResourcesManager.ResourceType.Coin, 1);
        }
    }
    public void TradeSlime()
    {
        if (GameManager.inst.ResourcesManager.TryToSpendResource(ResourcesManager.ResourceType.Slime, slimeTradeCost))
        {
            GameManager.inst.ResourcesManager.ModifyResourceValue(ResourcesManager.ResourceType.Coin, 1);
        }
    }
} 
