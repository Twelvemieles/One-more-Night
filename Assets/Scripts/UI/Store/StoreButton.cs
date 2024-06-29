using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreButton : BaseStoreButton
{
    [SerializeField] private string tradeID;

    private ItemToTrade _itemToTrade;

    private void Start()
    {
        SetInitialValues();
    }
    protected override void SetInitialValues()
    {
        base.SetInitialValues();
        _itemToTrade = GameManager.inst.ResourcesManager.resourcesConfig.GetItemToTradeConfigByID(tradeID);

    }
    public override void OnClick()
    {
        _storeScreenView.TradeItem(tradeID);
    }
    protected override void UpdateUI()
    {
        Sprite sprite = GameManager.inst.ResourcesManager.resourcesConfig.GetInitialResourceByType(_itemToTrade.reward.resource).icon;
        SetImageIcon(sprite);
        SetPriceText(CreatePriceConcatenation());
        SetRewardText(string.Concat("X ",_itemToTrade.reward.resourcesQuantity.ToString()));
    }
    protected override string CreatePriceConcatenation()
    {
        string concat = string.Concat("<sprite name=", _itemToTrade.itemToTrade.resource.ToString(),">", " ", _itemToTrade.itemToTrade.resourcesQuantity.ToString()) ;
        return concat;
    }

}
