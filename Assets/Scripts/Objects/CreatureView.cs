using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureView : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D rb;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetupInitialValues();
    }
    protected virtual void SetupInitialValues()
    {
    }
    protected virtual void Move(Vector2 movementVector)
    {

    }  
    protected virtual void Attack()
    {

    }
    public virtual void Dies()
    {

    }
    public virtual void ReceiveDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Dies();
        }

        GameManager.inst.AudioManager.PlaySFX("Hit");
    }
}
