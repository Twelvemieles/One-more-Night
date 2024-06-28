using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    [SerializeField] private float _speedMovement;
    private float _lifeTimeDuration;
    private Rigidbody2D _rb;
    public void init( float lifeTimeDuration)
    {
        _lifeTimeDuration = lifeTimeDuration;
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(CountDownDestroy());
        _rb.AddForce((Vector2)transform.up * _speedMovement ,ForceMode2D.Impulse);
    }
    private IEnumerator CountDownDestroy()
    {
        yield return new WaitForSeconds(_lifeTimeDuration);
        DestroyBullet();

    }
    private void DestroyBullet()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

}
