using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    [SerializeField] private float bulletRangeSeconds;
    [SerializeField] private float speedMovement;

    [SerializeField] private Rigidbody2D _rb;

    private float _damage;
    public void init( Vector2 direction)
    {
        _damage = GameManager.inst.ResourcesManager.GetActualWeaponUpgrade().damage ;
        _rb.velocity = direction * speedMovement;
        StartCoroutine(CountDownDestroy());
    }
    private IEnumerator CountDownDestroy()
    {
        yield return new WaitForSeconds(bulletRangeSeconds);
        DestroyBullet();

    }
    private void DestroyBullet()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyView enemy = collision.transform.GetComponent<EnemyView>();
        if(enemy != null)
        {
            enemy.ReceiveDamage(_damage);
        }
        DestroyBullet();
    }

}
