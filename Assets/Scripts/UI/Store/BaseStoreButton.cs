using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BaseStoreButton : MonoBehaviour
{

    [SerializeField] protected Image iconImage;
    [SerializeField] protected TextMeshProUGUI priceText;
    [SerializeField] protected TextMeshProUGUI rewardText;

    protected StoreScreenView _storeScreenView;
    protected void Start()
    {
        SetInitialValues();
    }
    protected virtual void SetInitialValues()
    {
        _storeScreenView = GameManager.inst.UIManager.StoreScreenView;
        _storeScreenView.OnUpdateUI += UpdateUI;
    }
    protected virtual void SetImageIcon(Sprite sprite)
    {
        if(iconImage != null)
        iconImage.sprite = sprite;

    }
    protected virtual void SetPriceText(string text)
    {
        if (priceText != null)
            priceText.text = text;
    }

    protected virtual  void SetRewardText(string text)
    {
        if (rewardText != null)
            rewardText.text = text;
    }
    protected virtual string CreatePriceConcatenation()
    {
        string concat = "";
        return concat;
    }

    public virtual void OnClick()
    {

    }
    protected virtual void UpdateUI()
    {
    }
}
