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

    private int _coinsCount;
    private int _slimeCount;
    private int _mineralCount;
    private float _lanternGasValue;
    private int _bulletsCount;
    public enum ResourceType
    {
        Slime,
        Mineral,
        Coin
    }
    public void ModifyResourceValue(ResourceType resourceType, int delta)
    {
        switch(resourceType)
        {
                case ResourceType.Slime:
                ModifySlime(delta);
                break;
                case ResourceType.Mineral:
                ModifyMineral(delta);
                break;
                case ResourceType.Coin:
                ModifyCoins(delta);
                break;
        }
    }
    public void ModifyCoins (int delta)
    {
        _coinsCount += delta;

        OnCoinsChange.Invoke(_coinsCount);
    }
    public void ModifySlime (int delta)
    {
        _slimeCount += delta;

        OnSlimeChange.Invoke(_slimeCount);
    }
    public void ModifyMineral(int delta)
    {
        _mineralCount += delta;
        OnMineralChange.Invoke(_mineralCount);
    }
    public void ModifyLanternGas(float delta)
    {
        _lanternGasValue += delta;
        OnLanternGasChange.Invoke(_lanternGasValue);
    }
    public void ModifyBullets(int delta)
    {
        _bulletsCount += delta;
        OnBulletsChange.Invoke(_bulletsCount);
    }

    public bool TryToSpendResource(ResourceType resourceType,int cost)
    {
        bool successful = false;
        if(HasEnoughResource(resourceType,cost))
        {
            switch (resourceType)
            {
                case ResourceType.Coin:
                    ModifyCoins(-cost);
                    break;
                case ResourceType.Mineral:
                    ModifyMineral(-cost);
                    break;
                case ResourceType.Slime:
                    ModifySlime(-cost);
                    break;
            }
            successful = true;

        }
        return successful;
    }
    private bool HasEnoughResource(ResourceType resourceType, int value)
    {
        bool result = false;
        switch (resourceType)
        {
            case ResourceType.Coin:
                result = _coinsCount >= value;
                break;
            case ResourceType.Mineral:
                result = _mineralCount >= value;
                break;
            case ResourceType.Slime:
                result = _slimeCount >= value;
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
