using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public delegate void intDelegate(int value);
    public delegate void floatDelegate(float value);

    public intDelegate OnCoinsChange;
    public intDelegate OnSlimeChange;
    public intDelegate OnMineralChange;
    public floatDelegate OnLanternGasChange;
    public intDelegate OnBulletsChange;

    [SerializeField] private PickeableItem mineralResourcePrefab;
    [SerializeField] private PickeableItem coinResourcePrefab;
    [SerializeField] private PickeableItem slimeResourcePrefab;

    private int _coinsCount = 1;
    private int _slimeCount;
    private int _mineralCount;
    private float _lanternGasValue = 1;
    private int _bulletsCount = 5;
    
    public int CoinsCount => _coinsCount;
    public int SlimeCount => _slimeCount;
    public int MineralCount => _mineralCount;
    public float LanternGasValue => _lanternGasValue;
    public int BulletsCount => _bulletsCount;
    public enum ResourceType
    {
        Slime,
        Mineral,
        Coin,
        Bullet,
        LanternGas
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

        OnCoinsChange.Invoke(_coinsCount);
    }
    private void ModifySlime (int delta)
    {
        _slimeCount += delta;

        OnSlimeChange.Invoke(_slimeCount);
    }
    private void ModifyMineral(int delta)
    {
        _mineralCount += delta;
        OnMineralChange.Invoke(_mineralCount);
    }
    private void ModifyLanternGas(float delta)
    {
        _lanternGasValue += delta;
        _lanternGasValue = Mathf.Clamp01(_lanternGasValue);
        OnLanternGasChange.Invoke(_lanternGasValue);
    }
    private void ModifyBullets(int delta)
    {
        _bulletsCount += delta;
        OnBulletsChange.Invoke(_bulletsCount);
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
    public PickeableItem GetPickeableResourcePrefab(ResourceType resourceType)
    {
        PickeableItem prefab = null;
        switch(resourceType)
        {
            case ResourceType.Mineral:
                prefab = mineralResourcePrefab;
                break;
            case ResourceType.Coin:
                prefab = coinResourcePrefab;
                break;                
            case ResourceType.Slime:
                prefab = slimeResourcePrefab;
                break;
        }
        return prefab;
    }
}
