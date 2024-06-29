using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreScreenView : MonoBehaviour
{
    public void CloseStorePanel()
    {
        GameManager.inst.UIManager.HideStoreScreen();
    }
} 
