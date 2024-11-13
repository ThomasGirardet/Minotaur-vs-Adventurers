using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundTask : Task
{

    public readonly List<Method> methodsList = new List<Method>();

    public void AddMethod(List<Task> taskList)
    {
        methodsList.Add(new Method(taskList));
    }

    public Method GetMethod(WorldState worldState, Method last)
    {
        bool afterLast = last == null || !methodsList.Contains(last);
        foreach (var method in methodsList)
        {
            if (afterLast)
            {
                if (method.Preview(worldState))
                    return method;
            }
            else
            {
                if (method == last)
                    afterLast = true;
            }
        }
        return null;
    }

    public override bool Preview(WorldState prev)
    {
        foreach(var method in methodsList)
        {
            if (method.Preview(prev))
                return true;
        }
        return false;
    }
}