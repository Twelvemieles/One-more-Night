using System;
using System.Collections.Generic;
using UnityEngine;

#region Initial Resources
[Serializable]
public class InitialResourceConfig
{
    public ResourceConfig initialresource;
    public Sprite icon;
    public PickeableItem pickeableItemPrefab;
}

#endregion

#region Weapon

[Serializable]
public class WeaponUpgrade
{
    public float cadence;
    public float damage;
    public List<ResourceConfig> resourcesCost;
}
[Serializable]
public class WeaponConfig
{
    public List<WeaponUpgrade> weaponUpgrades;
}

#endregion
#region Items Store

[Serializable]
public class ItemToTrade
{
    public string tradeID;
    public ResourceConfig itemToTrade;
    public ResourceConfig reward;
}
[Serializable]
public class StoreConfig
{
    public List<ItemToTrade> itemsToTrade;
}

#endregion

#region Skins
[Serializable]
public class PlayerBooster
{
    public enum PlayerBoosterType
    {
        SpeedMultiplierBooster,
        MoneyMultiplierBooster
    }
    public PlayerBoosterType boosterType;
    public float multiplierValue;
}
[Serializable]
public class SkinsConfig
{
    public string skinID;
    public PlayerSkin playerSkinPrefab;
    public List<ResourceConfig> skinCostResources;
    public PlayerBooster playerBooster;
    public Sprite icon;
    public string skinName;
    public string skinBoosterDescription;
}
#endregion


[Serializable]
public class ResourceConfig
{
    public ResourcesManager.ResourceType resource;
    public int resourcesQuantity;
}

    [CreateAssetMenu(fileName = "ResourcesConfig", menuName = "ScriptableObjects/ResourcesConfigScriptableObject", order = 2)]
public class ResourcesConfigScriptableObject : ScriptableObject
{
    public List<InitialResourceConfig> initialResourceConfigs;
    public StoreConfig storeConfig;
    public List<SkinsConfig> skinsConfig;
    public WeaponConfig weaponConfig;

    public InitialResourceConfig GetInitialResourceByType(ResourcesManager.ResourceType resourceType)
    {
        InitialResourceConfig initialResourceConfig = null;

        initialResourceConfig = initialResourceConfigs.Find(x => x.initialresource.resource == resourceType);
        return initialResourceConfig;
    }
    public ItemToTrade GetItemToTradeConfigByID(string tradeID)
    {
        ItemToTrade itemToTrade = null;
        itemToTrade = storeConfig.itemsToTrade.Find(x => x.tradeID == tradeID);
        return itemToTrade;
    }
    public SkinsConfig GetSkinConfigByID(string id)
    {
        SkinsConfig _skinsConfig = null;
        _skinsConfig = skinsConfig.Find(x => x.skinID == id);
        return _skinsConfig;
    }
    public WeaponUpgrade GetWeaponUpgradebyIndex(int index)
    {
        WeaponUpgrade weaponUpgrade = null;

        if(index < weaponConfig.weaponUpgrades.Count)
        {
            weaponUpgrade = weaponConfig.weaponUpgrades[index];
        }

        return weaponUpgrade;
    }

}
