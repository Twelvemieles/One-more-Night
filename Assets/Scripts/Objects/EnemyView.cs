using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : CreatureView
{
    [SerializeField] protected float visibilityRange;
    [SerializeField] protected NavMeshAgent  _navMeshAgent;
    [SerializeField] protected ResourcesManager.ResourceType  lootType;
    [SerializeField] protected int lootQuantity;
    [SerializeField] protected float secondsBackToPatrol;
    [SerializeField] protected float patrollingRange;
    [SerializeField] protected Collider2D triggerCollider;
    [SerializeField] protected Animator animator;

    protected Transform _target;


    protected EnemyState _actualState;

    public enum EnemyState
    {
        Patrol,
        Follow,
        Attacking,
        Paused
    }
    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {

        if (_actualState == EnemyState.Paused) return;
        switch (_actualState)
        {
            case EnemyState.Follow:
                if (_target != null)
                {
                    _navMeshAgent.SetDestination(_target.position);
                }
                break;
        }
    }
    protected override void SetupInitialValues()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
        GameManager.inst.OnPausedGameplay += OnPaused;
        GameManager.inst.OnResumedGameplay += OnResume;
        GameManager.inst.OnRestartGameplay += DestroyEnemy;
        StartPatrolling();
    }
    #region StateMachine
    protected void StartPatrolling()
    {
        _navMeshAgent.speed = speed / 3f;
        _actualState = EnemyState.Patrol;
        StartCoroutine(DoPatrol());
    }
    protected void StartFollowing()
    {
        _navMeshAgent.speed = speed;
        _actualState = EnemyState.Follow;
    }
    protected void Attack(CreatureView player)
    {
        StopAllCoroutines();
        _actualState = EnemyState.Attacking;
        player.ReceiveDamage(damage);
        StartCoroutine(StartCountDownToPatrol());
    }
    protected IEnumerator StartCountDownToPatrol()
    {
        if (gameObject.activeInHierarchy) yield return null;

        _navMeshAgent.isStopped = true;
        _target = null;
        triggerCollider.enabled = false;

        yield return new WaitForSeconds(secondsBackToPatrol + Random.Range(-0.5f, 0.5f));

        triggerCollider.enabled = true;
        _navMeshAgent.isStopped = false;
        StartPatrolling();
    }
    protected IEnumerator DoPatrol()
    {
        while (_actualState == EnemyState.Patrol)
        {
            GoToRandomClosePosition();
            yield return new WaitForSeconds(secondsBackToPatrol + Random.Range(-0.5f, 0.5f));
            yield return null;
        }
    }
    protected void GoToRandomClosePosition()
    {
        Vector3 vector = new Vector3(transform.position.x + Random.Range(-patrollingRange, patrollingRange), transform.position.y + Random.Range(-patrollingRange, patrollingRange), transform.position.z);
        _navMeshAgent.SetDestination(vector);
    }

    protected void OnPaused()
    {
        _actualState = EnemyState.Paused;
    }
    protected void OnResume()
    {
        _actualState = EnemyState.Patrol;
    }
    protected void SetAnimatorSpeedValue()
    {
        animator.SetFloat("Speed", _navMeshAgent.velocity.magnitude);
    }
    #endregion
    #region Colliders
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            _target = collision.transform;
            StartFollowing();
        }
    }
    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && gameObject.activeInHierarchy)
        {
            StartCoroutine(StartCountDownToPatrol());
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.CompareTag("Player") && _actualState != EnemyState.Attacking)
        {
            Attack(collision.transform.GetComponent<PlayerView>());
        }

    }
    #endregion
    #region Destroy
    public override void Dies()
    {
        StopAllCoroutines();
        base.Dies();
        GenerateLoot();
        DestroyEnemy();
        GameManager.inst.EnemiesManager.OnEnemyDies();
    }
    private void GenerateLoot()
    {
        for (int i = 0; i < lootQuantity; i++)
        {
            PickeableItem pickeableItem = GameManager.inst.ResourcesManager.GetPickeableResourcePrefab(lootType);
            pickeableItem = Instantiate(pickeableItem, transform.position, Quaternion.identity);
        }
    }
    protected void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    protected void OnDestroy()
    {
        GameManager.inst.OnPausedGameplay -= OnPaused;
        GameManager.inst.OnResumedGameplay -= OnResume;
        GameManager.inst.OnRestartGameplay -= DestroyEnemy;
    }
    #endregion

}
