using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinStoreButton : BaseStoreButton
{
    [SerializeField] private string skinID;
    [SerializeField] private TextMeshProUGUI skinName;

    private SkinsConfig _skinConfig;
    

    private void Start()
    {
        SetInitialValues();
    }
    protected override void SetInitialValues()
    {
        base.SetInitialValues();
        _skinConfig = GameManager.inst.ResourcesManager.resourcesConfig.GetSkinConfigByID(skinID);

    }
    public override void OnClick()
    {
        _storeScreenView.BuySkin(skinID);
    }
    protected override void UpdateUI()
    {
        Sprite sprite = _skinConfig.icon;
        SetImageIcon(sprite);
        SetPriceText(CreatePriceConcatenation());
        SetSkinName(_skinConfig.skinName);
    }
    private void SetSkinName(string text)
    {
        skinName.text = text;
    }
    protected override string CreatePriceConcatenation()
    {
        string concat = "";
        if (!GameManager.inst.ResourcesManager.HasSkin(skinID))
        {
            foreach (ResourceConfig resource in _skinConfig.skinCostResources)
            {
                concat += string.Concat("<sprite name=", resource.resource.ToString(), ">", " ", resource.resourcesQuantity.ToString(), " + ");
            }
            if (_skinConfig.skinCostResources.Count > 1)
            {
                concat.Remove(concat.Length - 2, 2);
            }
        }
        return concat;
    }
}
