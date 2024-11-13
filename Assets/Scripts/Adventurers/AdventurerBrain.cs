using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdventurerBrain : MonoBehaviour
{
    [SerializeField] private GameObject navGoal;
    [SerializeField] private Transform pickingUpChestTransform;

    private static bool isExecutingTask = false;
    private PrimitiveTask currentTask;
    private HTNPlanner planner;
    private List<PrimitiveTask> plan = new List<PrimitiveTask>();
    public static readonly Cooldown cooldown = new Cooldown();
    private WorldState worldState;
    private NavMeshAgent navAgent;


    private Minotaur minotaur;
    //private AdventurerManager manager;

    protected bool isHoldingChest = false;

    private float aggroValue;
    private float distToMinotaur;
    private float distToChest;

    #region Getters/Setters
    public GameObject GetNavGoal()
    {
        return navGoal;
    }
    public NavMeshAgent GetNavAgent()
    {
        return navAgent;
    }
    public void SetNavAgentGoal(Vector3 goal)
    {
        navAgent.destination = goal;
    }
    public Transform GetPickingUpChestTransform()
    {
        return pickingUpChestTransform;
    }
    public float GetAggroValue()
    {
        return aggroValue;
    }
    private void SetAggroValue(float aggroValue)
    {
        this.aggroValue = aggroValue;
    }
    public void SetIsHoldingChest(bool holdingChest)
    {
        isHoldingChest = holdingChest;
    }
    #endregion

    private float CalculateAggroValue()
    {
        float aggroTemp = 0f;
        if (distToMinotaur < minotaur.aggroRange)
        {
            aggroTemp += (minotaur.aggroRange - distToMinotaur + 1);
        }

        if (Vector3.Distance(this.transform.position, navGoal.transform.position) < 2f)
            aggroTemp += 30f;

        return aggroTemp;
    }

    public bool GetHoldingChest()
    {
        return isHoldingChest;
    }

    public WorldState GetWorldState()
    {
        return worldState;
    }

    public virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navGoal = GameObject.FindGameObjectWithTag("Chest");
        minotaur = FindObjectOfType<Minotaur>();
        worldState = new WorldState();
    }

    private void Start()
    {
        var root = new CompoundTask();
        planner = new HTNPlanner(root);
        planner.SetAdventurer(this);

        var NavToChest = new NavToLocation(WorldState.EObjectNextTo.Chest);
        var PickUpChest = new PickUpChest();
        var NavToCorner = new NavToLocation(WorldState.EObjectNextTo.Corner);

        var StealChest = new CompoundTask();
        StealChest.AddMethod(new List<Task>() { NavToChest, PickUpChest, NavToCorner }); //If don't have chest, pick it up, then go to corner
        StealChest.AddMethod(new List<Task>() { NavToCorner }); //If have chest, go to corner

        var NavToMinotaur = new NavToLocation(WorldState.EObjectNextTo.Minotaur);

        var AggroMinotaur = new CompoundTask();
        AggroMinotaur.AddMethod(new List<Task>() { NavToMinotaur });

        root.AddMethod(new List<Task>() { StealChest });
        root.AddMethod(new List<Task>() { AggroMinotaur });
        //manager = FindObjectOfType<AdventurerManager>();
        /*
         * Build an HTN
         * 
         * 
         * 
         * -------------------------------------------------------------
         * Picking Up Chest
         * -------------------------------------------------------------

         * 
         * -------------------------------------------------------------
         * Taking Aggro
         * -------------------------------------------------------------
         * 
         * -------------------------------------------------------------
         * Attacking
         * -------------------------------------------------------------
         * var AttackMinotaur = new AttackMinotaur();
         * var Cooldown = new Cooldown();
         * 
         */
    }

    void Update()
    {
        Vector3 thisPos = gameObject.transform.position;

        worldState.ChosenChestCarrier = Sensors.Instance.chosenChestCarryingAdventurer != null && Sensors.Instance.chosenChestCarryingAdventurer == this;
        worldState.CanHoldChest = worldState.ChosenChestCarrier && Vector3.Distance(thisPos, navGoal.transform.position) < 2.25f;
        worldState.CanAttack = worldState.InAttackingRange && worldState.InLineOfSight;
        worldState.IsHoldingChest = Sensors.Instance.adventurerHoldingChest == this;

        //Debug.Log(this.gameObject.name + ": " + worldState.ChosenChestCarrier + ", " + worldState.CanHoldChest + ", " + worldState.CanAttack + ", " + worldState.IsHoldingChest);
        //Debug.Log("Distance to navGoal: " + Vector3.Distance(thisPos, navGoal.transform.position));

        distToMinotaur = Vector3.Distance(this.transform.position, minotaur.transform.position);
        distToChest = Vector3.Distance(this.transform.position, navGoal.transform.position);

        SetAggroValue(CalculateAggroValue());
        Action();
    }

    void Action()
    {
        
        //Check if the adventurer is currently executing a task
        if (isExecutingTask)
            return;

        if ((currentTask == null || currentTask == cooldown) && (plan == null || plan.Count == 0))
        {
            plan = planner.GetPlan(worldState);
            return;
        }

        if (currentTask == null)
        {
            currentTask = plan[plan.Count - 1];
            plan.RemoveAt(plan.Count - 1);
            if (currentTask.Preview(worldState))
            {
                isExecutingTask = true;
                currentTask.Start(this);
            }
            else
            {
                plan = null;
                currentTask = cooldown;
                isExecutingTask = true;
                currentTask.Start(this);
            }
        }

        foreach(Task task in plan)
            Debug.Log(task.ToString());

        if (currentTask.Terminate(this, worldState))
        {
            if (currentTask == cooldown)
            {
                currentTask = null;
            }
            else
            {
                currentTask = cooldown;
                isExecutingTask = true;
                currentTask.Start(this);
            }
        }
    }

    public void TaskComplete()
    {
        isExecutingTask = false;
    }
}