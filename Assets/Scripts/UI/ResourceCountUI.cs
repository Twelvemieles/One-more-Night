using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCountUI : MonoBehaviour
{
    [SerializeField] private ResourcesManager.ResourceType resourceType;
    [SerializeField] private TextMeshProUGUI CountText; 
    
    // Start is called before the first frame update
    void Start()
    {
        switch(resourceType)
        {
            case ResourcesManager.ResourceType.Coin:

                GameManager.inst.ResourcesManager.OnCoinsChange += OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.Mineral:

                GameManager.inst.ResourcesManager.OnMineralChange += OnResourceUpdate;
                break;
            case ResourcesManager.ResourceType.Slime:

                GameManager.inst.ResourcesManager.OnSlimeChange += OnResourceUpdate;
                break;

        }
    }
    private void OnResourceUpdate(int value)
    {
        CountText.text = value.ToString();
    }
}
