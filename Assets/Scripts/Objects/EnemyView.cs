using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : CreatureView
{
    [SerializeField] protected Transform target;
    [SerializeField] protected float visibilityRange;
    [SerializeField] protected int lootQuantity;
    [SerializeField] protected NavMeshAgent  _navMeshAgent;

    protected State actualState;
    protected override void Start()
    {
        base.Start();
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;
    }
    public enum State
    {
        Idle,
        Patrol,
        Follow,
    }
    protected void StartIdle()
    {
        actualState = State.Idle;
    }    
    protected void StartPatrolling()
    {
        actualState = State.Patrol;
    }
    protected void StartFollowing()
    {
        actualState = State.Follow;
    }
    protected void Update()
    {
        _navMeshAgent.SetDestination(target.position);
    }
}
