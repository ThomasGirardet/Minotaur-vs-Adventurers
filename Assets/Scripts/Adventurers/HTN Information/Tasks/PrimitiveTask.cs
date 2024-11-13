using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PrimitiveTask : Task
{
    public abstract string name { get; }
    public virtual void Start(AdventurerBrain adventurer) { }
    public virtual void Post(AdventurerBrain adventurer, WorldState state) { }
    public virtual bool Terminate(AdventurerBrain adventurer, WorldState state) 
    {
        return true;
    }
}
