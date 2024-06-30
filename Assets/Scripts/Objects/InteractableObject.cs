using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    protected bool canInteract;
    public bool CanInteract => canInteract;

    protected virtual void Start()
    {
        canInteract = true;
    }
    public virtual void OnInteraction()
    {

        GameManager.inst.AudioManager.PlaySFX("ButtonHover");
    }
}
