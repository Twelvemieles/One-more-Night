using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MineralsCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mineralsCountText; 
    private int _mineralsCount;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.inst.ResourcesManager.OnMineralChange += OnMineralsUpdate;
    }
    private void OnMineralsUpdate(int value)
    {
        mineralsCountText.text = value.ToString();
    }
}
