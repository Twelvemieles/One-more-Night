using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreScreenView : MonoBehaviour
{
    public ResourcesManager.voidDelegate OnUpdateUI;

    [SerializeField] private Image actualSkinImage;
    private void OnEnable()
    {
        UpdateUI();
    }
    private void Start()
    {
        UpdateUI();
    }
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
        UpdateUI();
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
        UpdateUI();
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
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        OnUpdateUI?.Invoke();
        SetActualSkinImage(GameManager.inst.ResourcesManager.resourcesConfig.GetSkinConfigByID(GameManager.inst.ResourcesManager.ActualSkin).icon);
    }
    private void SetActualSkinImage(Sprite sprite)
    {
        actualSkinImage.sprite = sprite;
    }
} 
