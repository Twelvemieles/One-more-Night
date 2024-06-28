using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : CreatureView
{
    [SerializeField] public float shootSpeed;
    [SerializeField] private float coinMultiplier;

    void Update()
    {
        Vector2 movementVector = new Vector2();
        // Get input from the player
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");
        
        Move(movementVector);
    }
    protected override void Move(Vector2 movementVector)
    {
        base.Move(movementVector);
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);
    }
    private void PickItem()
    {

    }

    private void Interact()
    {

    }

    public void UseLantern()
    {

    }
}
