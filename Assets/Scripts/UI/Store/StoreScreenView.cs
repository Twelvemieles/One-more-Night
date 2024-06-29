using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreenView : MonoBehaviour
{
    public void CloseStorePanel()
    {
        GameManager.inst.UIManager.HideStoreScreen();
    }
    public void TradeItem(string tradeID)
    {
        ItemToTrade itemToTrade = GameManager.inst.ResourcesManager.resourcesConfig.GetItemToTradeConfigByID(tradeID);
        if (GameManager.inst.ResourcesManager.TryToSpendResource(itemToTrade.itemToTrade.resource, itemToTrade.itemToTrade.resourcesQuantity))
        {
            GameManager.inst.ResourcesManager.ModifyResourceValue(itemToTrade.reward.resource, itemToTrade.reward.resourcesQuantity);
        }
    }
    public void BuySkin(string SkinID)
    {
        if (!GameManager.inst.ResourcesManager.HasSkin(SkinID))
        {

            SkinsConfig skinConfig = GameManager.inst.ResourcesManager.resourcesConfig.GetSkinConfigByID(SkinID);
            Dictionary<ResourcesManager.ResourceType, float> resourcesToSpend = new Dictionary<ResourcesManager.ResourceType, float>();

            foreach (ResourceConfig resourceCost in skinConfig.skinCostResources)
            {
                resourcesToSpend.Add(resourceCost.resource, resourceCost.resourcesQuantity);
            }

            if (GameManager.inst.ResourcesManager.TryToSpendVariousResources(resourcesToSpend))
            {
                GameManager.inst.ResourcesManager.UnlockSkin(SkinID);
            }
        }
        else
        {
            GameManager.inst.ResourcesManager.SelectSkin(SkinID);
        }
    }
    public void BuyNextWeaponUpgrade()
    {
        int nextWeaponUpgradeIndex = GameManager.inst.ResourcesManager.ActualWeaponUpgrade +  1;
        WeaponUpgrade nextWeaponUpgrade = GameManager.inst.ResourcesManager.resourcesConfig.GetWeaponUpgradebyIndex(nextWeaponUpgradeIndex);
        if(nextWeaponUpgrade != null)
        {
            Dictionary<ResourcesManager.ResourceType, float> resourcesToSpend = new Dictionary<ResourcesManager.ResourceType, float>();

            foreach(ResourceConfig resourceCost in nextWeaponUpgrade.resourcesCost)
            {
                resourcesToSpend.Add(resourceCost.resource, resourceCost.resourcesQuantity);
            }

            if (GameManager.inst.ResourcesManager.TryToSpendVariousResources(resourcesToSpend))
            {
                GameManager.inst.ResourcesManager.UpgradeWeapon();
            }
        }
    }
} 
