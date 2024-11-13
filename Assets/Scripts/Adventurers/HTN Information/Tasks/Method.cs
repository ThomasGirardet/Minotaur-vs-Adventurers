using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Method : CompoundTask
{
    public readonly List<Task> subtasks;

    public Method(List<Task> subtasks)
    {
        this.subtasks = subtasks;
    }

    public override bool Preview(WorldState worldState)
    {
        return subtasks[0].Preview(worldState);
    }

    public void AddSubtasksToHTNStack(Stack<Task> tasks)
    {
        Debug.Log("Entering Subtasks: " + subtasks[0]);
        for(int i = subtasks.Count - 1; i >= 0; i--)
        {
            Debug.Log("Subtask: " + subtasks[i]); 
            tasks.Push(subtasks[i]);
        }
        Debug.Log("Exiting Subtasks");
    }
}
