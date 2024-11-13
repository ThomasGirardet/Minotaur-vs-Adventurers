using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTNPlanner
{
    private AdventurerBrain adventurer;

    private CompoundTask root;

    private Stack<(Stack<Task> taskList, List<PrimitiveTask> plan, Method method, WorldState worldState)> history;

    public HTNPlanner(CompoundTask t)
    {
        this.root = t;
    }

    #region Getter/Setter
    public void SetAdventurer(AdventurerBrain adventurer)
    {
        this.adventurer = adventurer;
    }
    #endregion

    public List<PrimitiveTask> GetPlan(WorldState worldState)
    {
        history = new Stack<(Stack<Task> taskList, List<PrimitiveTask> plan, Method method, WorldState worldState)>();
        List<PrimitiveTask> plan = new List<PrimitiveTask>();
        WorldState state = worldState.Clone();

        Stack<Task> tasks = new Stack<Task>();
        tasks.Push(root);
        Method method = null;
        while(tasks.Count > 0)
        {
            Task task = tasks.Peek();
            //Debug.Log(task);
            if(task is CompoundTask compoundTask)
            {
                method = compoundTask.GetMethod(state, method);
                if(method == null)
                {
                    //Debug.Log(adventurer + ": " + state.ChosenChestCarrier + ", " + state.CanHoldChest + ", " + state.CanAttack + ", " + state.IsHoldingChest);
                    //If the method found in the compound task is null, go back to the most recent addition to history and grab that gamestate
                    if (history.Count == 0)
                        break;
                    (tasks, plan, method, state) = history.Pop();
                }
                else //If the method found is found, push the gamestate to the history stack and pop the method
                {
                    history.Push((new Stack<Task>(tasks), new List<PrimitiveTask>(plan), method, state.Clone()));
                    tasks.Pop();
                    method.AddSubtasksToHTNStack(tasks);
                }
            }
            else if(task is PrimitiveTask primitiveTask)
            {
                if(task.Preview(state))
                {
                    tasks.Pop();
                    primitiveTask.Post(adventurer, state);
                    plan.Add(primitiveTask);
                }
                else
                {
                    (tasks, plan, method, state) = history.Pop();
                }
            }
        }
        plan.Reverse();
        //Debug.Log(adventurer + ": ");
        return plan;
    }
}
