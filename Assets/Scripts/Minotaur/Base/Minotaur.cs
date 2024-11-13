using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Minotaur : MonoBehaviour, ITriggerCheckable
{
    public int numberInAttackRange = 0;
    public float aggroRange;
    public float attackRange;
    private float attackTimer = 0;
    private AdventurerManager manager;
    private NavMeshAgent navAgent;
    private bool someoneHasChest;
    public GameObject chestHolder;

    #region State Machine Variables
    public MinotaurStateMachine StateMachine { get; private set; }
    public MinotaurIdleState IdleState { get; private set; }
    public MinotaurPatrolState PatrolState { get; private set; }
    public MinotaurChaseState ChaseState { get; private set; }
    public MinotaurAttackState AttackState { get; private set; }
    public bool IsIdling { get; set; }
    public bool IsPatrolling { get; set; }
    public bool IsAggroed { get; set; }
    public bool IsWithinStrikingDistance { get; set; }
    public bool IsWithinView { get; set; }
    #endregion

    #region PatrolPoints
    public Vector3 patrolPoint1 = new Vector3(0, 3, -2.5f);
    public Vector3 patrolPoint2 = new Vector3(2.5f, 3, 0);
    public Vector3 patrolPoint3 = new Vector3(0, 3, 2.5f);
    public Vector3 patrolPoint4 = new Vector3(-2.5f, 3, 0);
    #endregion

    public NavMeshAgent GetNavAgent()
    {
        return navAgent;
    }
    public AdventurerManager GetAdventurerManager()
    {
        return manager;
    }
    void Awake()
    {
        manager = FindObjectOfType<AdventurerManager>();

        StateMachine = new MinotaurStateMachine();
        IdleState = new MinotaurIdleState(this, StateMachine);
        PatrolState = new MinotaurPatrolState(this, StateMachine);
        AttackState = new MinotaurAttackState(this, StateMachine);
        ChaseState = new MinotaurChaseState(this, StateMachine);

    }

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        StateMachine.Initialize(PatrolState);
    }
    private void AnimationTriggerEvent(AnimationTriggerType type)
    {
        StateMachine.CurrentMinotaurState.AnimationTriggerEvent(type);
    }
    public enum AnimationTriggerType
    {
        StrikingRange
    }

    #region Idle/Patrol Checks
    public void SetIdlingStatus(bool isIdling)
    {
        IsIdling = isIdling;
    }
    public void SetPatrollingStatus(bool isPatrolling)
    {
        IsPatrolling = isPatrolling;
    }
    #endregion

    #region Agression Checks
    public void SetAggro(bool isAggroed)
    { 
        IsAggroed = isAggroed;
    }
    public void SetWithinStrikingDistance(bool isWithinStrikingDistance)
    {
        IsWithinStrikingDistance = isWithinStrikingDistance;
    }

    public void SetWithinView(bool isWithinView)
    {
        IsWithinView = isWithinView;
    }
    public void SetAttackTimer(float timer)
    {
        attackTimer = timer;
    }
    public float GetAttackTimer()
    {
        return attackTimer;
    }
    public bool GetSomeoneHasChest()
    {
        return someoneHasChest;
    }
    public void SetSomeoneHasChest(bool pickedUp)
    {
        someoneHasChest = pickedUp;
    }
    #endregion

    void Update()
    {
        StateMachine.CurrentMinotaurState.FrameUpdate();
    }
}