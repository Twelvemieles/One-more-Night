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
        GameManager.inst.UIManager.ShowDialog(string.Concat("ShopInteraction_", Random.Range(1,4).ToString()), () =>
         {

             if (canInteract)
             {
                 GameManager.inst.UIManager.ShowStoreScreen();
             }
         });
    }
}
