using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineResourceProducer : InteractableObject
{
    [SerializeField] private ResourcesManager.ResourceType resourceType;
    [SerializeField] private int activationCost;
    [SerializeField] private float cooldownGeneration;
    [SerializeField] private bool isGenerating;
    [SerializeField] private Collider2D collider2D;
    [SerializeField] private Transform spawnResource;

    private void Start()
    {
        canInteract = true;
        collider2D.enabled = true;
    }
    public override void OnInteraction()
    {
        base.OnInteraction();
        if( canInteract)
        {
            StartResourceGeneration();
        }
    }
    public void StartResourceGeneration()
    {
        canInteract = false;
        StartCoroutine(CooldownGeneration());
        collider2D.enabled = false;
    }
    private IEnumerator CooldownGeneration()
    {
        yield return new WaitForSeconds(cooldownGeneration);
        GenerateResource();
    }
    private void GenerateResource()
    {
        PickeableItem pickeableItem = GameManager.inst.ResourcesManager.GetPickeableResourcePrefab(resourceType);
        pickeableItem = Instantiate(pickeableItem, spawnResource);
        collider2D.enabled = true;
        canInteract = true;
    }
}
