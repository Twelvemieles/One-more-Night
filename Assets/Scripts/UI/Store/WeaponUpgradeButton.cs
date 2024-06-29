using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeButton : BaseStoreButton
{
    [SerializeField] private List<Toggle> upgradeToggles;
    private WeaponUpgrade _weaponUpgradeConfig;
    
    protected override void SetInitialValues()
    {
        base.SetInitialValues();
    }
    protected override void UpdateUI()
    {
        _weaponUpgradeConfig = GameManager.inst.ResourcesManager.resourcesConfig.GetWeaponUpgradebyIndex(GameManager.inst.ResourcesManager.ActualWeaponUpgrade + 1);
        SetPriceText(CreatePriceConcatenation());
        ActivateToggles();
    }
    protected override string CreatePriceConcatenation()
    {
        string concat = "";
        if(_weaponUpgradeConfig != null)
        {
            foreach (ResourceConfig resource in _weaponUpgradeConfig.resourcesCost)
            {
                concat += string.Concat("<sprite name=", resource.resource.ToString(), ">", " ", resource.resourcesQuantity.ToString(), " + ");
            }
            if (_weaponUpgradeConfig.resourcesCost.Count > 1)
            {
                concat.Remove(concat.Length - 2, 2);
            }
        }
        return concat;
    }
    private void ActivateToggles()
    {
        for(int i = 0; i < upgradeToggles.Count; i++)
        {
            upgradeToggles[i].isOn = i <= GameManager.inst.ResourcesManager.ActualWeaponUpgrade;
        }
    }
    protected override void SetPriceText(string text)
    {
        if (priceText != null)
            priceText.text = text;
    }

    public override void OnClick()
    {
        _storeScreenView.BuyNextWeaponUpgrade();
    }
}
