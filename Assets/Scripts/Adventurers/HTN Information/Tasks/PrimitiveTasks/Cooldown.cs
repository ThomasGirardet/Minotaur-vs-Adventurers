using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : PrimitiveTask
{
    float end;
    public override string name => "Cooldown";

    public override void Start(AdventurerBrain adventurer)
    {
        end = Time.time + 1;
    }

    public override bool Terminate(AdventurerBrain adventurer, WorldState worldState)
    {
        return Time.time > end;
    }
}
