using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCountUI : MonoBehaviour
{
    [SerializeField] protected ResourcesManager.ResourceType resourceType;
    [SerializeField] private TextMeshProUGUI CountText; 
    
    // Start is called before the first frame update
    protected void Start()
    {
        switch(resourceType)
        {
            case ResourcesManager.ResourceType.Coin:

                GameManager.inst.ResourcesManager.OnCoinsChange += OnResourceUpdate;
                OnResourceUpdate(GameManager.inst.ResourcesManager.CoinsCount);
                break;
            case ResourcesManager.ResourceType.Mineral:

                GameManager.inst.ResourcesManager.OnMineralChange += OnResourceUpdate;
                OnResourceUpdate(GameManager.inst.ResourcesManager.MineralCount);
                break;
            case ResourcesManager.ResourceType.Slime:

                GameManager.inst.ResourcesManager.OnSlimeChange += OnResourceUpdate;
                OnResourceUpdate(GameManager.inst.ResourcesManager.SlimeCount);
                break;
            case ResourcesManager.ResourceType.Bullet:

                GameManager.inst.ResourcesManager.OnBulletsChange += OnResourceUpdate;
                OnResourceUpdate(GameManager.inst.ResourcesManager.BulletsCount);
                break;
            case ResourcesManager.ResourceType.LanternGas:

                GameManager.inst.ResourcesManager.OnLanternGasChange += OnResourceUpdate;
                OnResourceUpdate(GameManager.inst.ResourcesManager.LanternGasValue);
                break;

        }
    }
    protected virtual void OnResourceUpdate(int value)
    {
        if(CountText != null) CountText.text = value.ToString();
    }
    protected virtual void OnResourceUpdate(float value)
    {
        if (CountText != null) CountText.text = value.ToString();
    }
    private void OnDestroy()
    {
        switch (resourceType)
        {
            case ResourcesManager.ResourceType.Coin:

                GameManager.inst.ResourcesManager.OnCoinsChange -= OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.Mineral:

                GameManager.inst.ResourcesManager.OnMineralChange -= OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.Slime:

                GameManager.inst.ResourcesManager.OnSlimeChange -= OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.Bullet:

                GameManager.inst.ResourcesManager.OnBulletsChange -= OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.LanternGas:

                GameManager.inst.ResourcesManager.OnLanternGasChange -= OnResourceUpdate;
                break;

        }
    }
}
