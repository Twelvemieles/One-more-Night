using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickeableItem : MonoBehaviour
{
    [SerializeField] protected ResourcesManager.ResourceType ResourceType;
    [SerializeField] protected int resourceQuantity;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Picked(collision.transform);
        }
    }
    protected void Picked(Transform playerTransform)
    {
        StartPickAnimation(playerTransform);
    }
    protected void StartPickAnimation(Transform playerTransform)
    {
        LeanTween.move(gameObject, playerTransform, 0.2f).setOnComplete(() =>
         {
             GiveResources();
         }
        );
    }
    protected void GiveResources()
    {
        GameManager.inst.ResourcesManager.ModifyResourceValue(ResourceType, resourceQuantity);
        DestroyItem();
    }
    protected void DestroyItem()
    {
        Destroy(gameObject);
    }
}
