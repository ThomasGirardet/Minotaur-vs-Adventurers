using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task
{
    public virtual bool Preview(WorldState preview)
    {
        return true;
    }
}