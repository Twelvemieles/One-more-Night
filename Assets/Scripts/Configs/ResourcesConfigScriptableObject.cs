using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
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
public class SkinsCost
{
    public ResourcesManager.ResourceType resources;
    public int resourcesQuantity;
}
    [Serializable]
public class SkinsConfig
{
    public string skinID;
    public PlayerSkin playerSkinPrefab;
    public List<SkinsCost> skinCostResources;
    public PlayerBooster playerBooster;
}
    [CreateAssetMenu(fileName = "ResourcesConfig", menuName = "ScriptableObjects/ResourcesConfigScriptableObject", order = 2)]
public class ResourcesConfigScriptableObject : ScriptableObject
{
    public List<SkinsConfig> skinsConfig;

}
