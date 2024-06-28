using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : CreatureView
{
    [SerializeField] private float shootSpeed;
    [SerializeField] private float bulletRangeSeconds;
    [SerializeField] private float coinMultiplier;
    [SerializeField] private BulletView bulletViewPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject interactionExclamation;

    private InteractableObject _closeInteractableObject;

    void Update()
    {
        Vector2 movementVector = new Vector2();
        // Get input from the player
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        
        Move(movementVector);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    protected override void Move(Vector2 movementVector)
    {
        base.Move(movementVector);
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);


        if (movementVector != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementVector.y, movementVector.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }
    protected override void Attack()
    {
        base.Attack();
        BulletView bulletView =  Instantiate(bulletViewPrefab, bulletSpawn.position, bulletSpawn.rotation);
        bulletView.init(bulletRangeSeconds);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _closeInteractableObject = collision.gameObject.GetComponent<InteractableObject>();
        if (_closeInteractableObject != null && _closeInteractableObject.CanInteract)
        {
            EnableInteractionExclamation();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            _closeInteractableObject = null;
            DisableInteractionExclamation();
        }
    }
    private void PickItem()
    {

    }

    private void Interact()
    {
        if(_closeInteractableObject != null)
        {
            _closeInteractableObject.OnInteraction();
        }
    }
    private void EnableInteractionExclamation()
    {
        interactionExclamation.SetActive(true);
    }
    private void DisableInteractionExclamation()
    {
        interactionExclamation.SetActive(false);
    }
    public void UseLantern()
    {

    }
}
