using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureView : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Rigidbody2D rb;
    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
}
