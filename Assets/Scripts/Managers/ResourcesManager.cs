using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public delegate void intDelegate(int value);
    public delegate void voidDelegate();
    public delegate void floatDelegate(float value);
    public delegate void stringDelegate(string value);

    public intDelegate OnCoinsChange;
    public intDelegate OnSlimeChange;
    public intDelegate OnMineralChange;
    public floatDelegate OnLanternGasChange;
    public intDelegate OnBulletsChange;
    public stringDelegate OnSkinSelected;

    public ResourcesConfigScriptableObject resourcesConfig;

    private int _coinsCount ;
    private int _slimeCount;
    private int _mineralCount;
    private float _lanternGasValue ;
    private int _bulletsCount ;
    private int _actualWeaponUpgrade;
    private List<string> _earnedSkins = new List<string>();
    private string _actualSkin;
    
    public int CoinsCount => _coinsCount;
    public int SlimeCount => _slimeCount;
    public int MineralCount => _mineralCount;
    public float LanternGasValue => _lanternGasValue;
    public int BulletsCount => _bulletsCount;
    public int ActualWeaponUpgrade => _actualWeaponUpgrade;
    public string ActualSkin => _actualSkin;

    public enum ResourceType
    {
        Slime,
        Mineral,
        Coin,
        Bullet,
        LanternGas
    }
    public void SetupInitialResourcesValues()
    {
        _coinsCount = resourcesConfig.GetInitialResourceByType(ResourceType.Coin).initialresource.resourcesQuantity;
        _slimeCount = resourcesConfig.GetInitialResourceByType(ResourceType.Slime).initialresource.resourcesQuantity;
        _mineralCount = resourcesConfig.GetInitialResourceByType(ResourceType.Mineral).initialresource.resourcesQuantity;
        _lanternGasValue = resourcesConfig.GetInitialResourceByType(ResourceType.LanternGas).initialresource.resourcesQuantity;
        _bulletsCount = resourcesConfig.GetInitialResourceByType(ResourceType.Bullet).initialresource.resourcesQuantity;

        _actualWeaponUpgrade = 0;

        _earnedSkins.Add(resourcesConfig.skinsConfig[0].skinID);
        SelectSkin( _earnedSkins[0]);
    }
    public void ModifyResourceValue(ResourceType resourceType, float delta)
    {
        switch(resourceType)
        {
                case ResourceType.Slime:
                ModifySlime((int)delta);
                break;
                case ResourceType.Mineral:
                ModifyMineral((int)delta);
                break;
                case ResourceType.Coin:
                ModifyCoins((int)delta);
                break;
                case ResourceType.Bullet:
                ModifyBullets((int)delta);
                break;
                case ResourceType.LanternGas:
                ModifyLanternGas(delta);
                break;
        }
    }
    private void ModifyCoins (int delta)
    {
        _coinsCount += delta;

        OnCoinsChange?.Invoke(_coinsCount);
    }
    private void ModifySlime (int delta)
    {
        _slimeCount += delta;

        OnSlimeChange?.Invoke(_slimeCount);
    }
    private void ModifyMineral(int delta)
    {
        _mineralCount += delta;
        OnMineralChange?.Invoke(_mineralCount);
    }
    private void ModifyLanternGas(float delta)
    {
        _lanternGasValue += delta;
        _lanternGasValue = Mathf.Clamp01(_lanternGasValue);
        OnLanternGasChange?.Invoke(_lanternGasValue);
    }
    private void ModifyBullets(int delta)
    {
        _bulletsCount += delta;
        OnBulletsChange?.Invoke(_bulletsCount);
    }
    public bool TryToSpendResource(ResourceType resourceType,float cost)
    {
        bool successful = false;
        if(HasEnoughResource(resourceType,cost))
        {
            switch (resourceType)
            {
                case ResourceType.Coin:
                    ModifyCoins((int)-cost);
                    break;
                case ResourceType.Mineral:
                    ModifyMineral((int)-cost);
                    break;
                case ResourceType.Slime:
                    ModifySlime((int)-cost);
                    break;
                case ResourceType.Bullet:
                    ModifyBullets((int)-cost);
                    break;
                case ResourceType.LanternGas:
                    ModifyLanternGas(-cost);
                    break;
            }
            successful = true;

        }
        return successful;
    }
    public bool TryToSpendVariousResources(Dictionary<ResourceType,float> resourcesDictionary)
    {
        foreach (KeyValuePair<ResourceType, float> resource in resourcesDictionary)
        {
            if (!HasEnoughResource(resource.Key, resource.Value))
            {
                return false;
            }
        }
        foreach (KeyValuePair<ResourceType, float> resource in resourcesDictionary)
        {
            TryToSpendResource(resource.Key, resource.Value);
        }
        return true;
    }
    private bool HasEnoughResource(ResourceType resourceType, float value)
    {
        bool result = false;
        switch (resourceType)
        {
            case ResourceType.Coin:
                result = _coinsCount >= (int)value;
                break;
            case ResourceType.Mineral:
                result = _mineralCount >= (int)value;
                break;
            case ResourceType.Slime:
                result = _slimeCount >= (int)value;
                break;
            case ResourceType.Bullet:
                result = _bulletsCount >= (int) value;
                break;
            case ResourceType.LanternGas:
                result = _lanternGasValue >= value;
                break;
        }
        return result;
    }
    public void UpgradeWeapon()
    {
        _actualWeaponUpgrade++;
        _actualWeaponUpgrade = Mathf.Clamp(_actualWeaponUpgrade,0, resourcesConfig.weaponConfig.weaponUpgrades.Count - 1);
    }
    public PickeableItem GetPickeableResourcePrefab(ResourceType resourceType)
    {
        PickeableItem prefab = null;
        switch(resourceType)
        {
            case ResourceType.Mineral:
                prefab = resourcesConfig.GetInitialResourceByType(ResourceType.Mineral).pickeableItemPrefab;
                break;
            case ResourceType.Coin:
                prefab = resourcesConfig.GetInitialResourceByType(ResourceType.Coin).pickeableItemPrefab;
                break;                
            case ResourceType.Slime:
                prefab = resourcesConfig.GetInitialResourceByType(ResourceType.Slime).pickeableItemPrefab;
                break;
        }
        return prefab;
    }
    public bool HasSkin(string SkinID)
    {
        return _earnedSkins.Contains(SkinID);
    }
    public void UnlockSkin(string SkinID)
    {
        _earnedSkins.Add(SkinID);
        SelectSkin(SkinID);
    }
    public void SelectSkin(string skinID)
    {
        _actualSkin = skinID;
        OnSkinSelected?.Invoke(skinID);
    }
}
