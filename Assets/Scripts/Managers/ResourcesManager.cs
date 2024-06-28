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

    private int _coinsCount;
    private int _slimeCount;
    private int _mineralCount;
    private float _lanternGasValue;
    private int _bulletsCount;
    public int MineralCount => _mineralCount;
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

    public PickeableItem GetPickeableResourcePrefab(ResourceType resourceType)
    {
        PickeableItem prefab = null;
        switch(resourceType)
        {
            case ResourceType.Mineral:
                prefab = mineralResourcePrefab;
                break;
        }
        return prefab;
    }
}
