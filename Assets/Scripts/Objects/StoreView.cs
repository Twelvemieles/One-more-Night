using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreView : InteractableObject
{
    public override void OnInteraction()
    {
        base.OnInteraction();
        ShowStoreUI();
    }
    private void ShowStoreUI()
    {
        if(canInteract)
        {
            GameManager.inst.UIManager.ShowStoreScreen();
        }
    }
}
