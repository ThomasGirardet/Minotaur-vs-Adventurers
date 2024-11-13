using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavToLocation : PrimitiveTask
{
    readonly WorldState.EObjectNextTo  objectPathedTo;
    public override string name { get; }

    public NavToLocation(WorldState.EObjectNextTo name)
    {
        this.name = "NavTo" + name;
        objectPathedTo = name;
    }

    public override bool Preview(WorldState preview)
    {
        if(objectPathedTo == WorldState.EObjectNextTo.Chest)
        {
            if(preview.ChosenChestCarrier && !preview.IsHoldingChest)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(objectPathedTo == WorldState.EObjectNextTo.Minotaur)
        {
            if(!preview.ChosenChestCarrier && !preview.IsHoldingChest && !preview.InAttackingRange)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if(objectPathedTo == WorldState.EObjectNextTo.Corner)
        {
            if(preview.IsHoldingChest)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public override void Start(AdventurerBrain adventurer)
    {
        if(objectPathedTo == WorldState.EObjectNextTo.Minotaur)
        {
            adventurer.SetNavAgentGoal(adventurer.GetWorldState().GetMinotaur().transform.position);
        }
        else if(objectPathedTo == WorldState.EObjectNextTo.Chest)
            adventurer.SetNavAgentGoal(adventurer.GetWorldState().GetChest().transform.position);
        else if(objectPathedTo == WorldState.EObjectNextTo.Corner)
        {
            Vector3 closestPoint = new(13, 1, 13); //Arbitrarily assume starting closest corner is (13, 1, 13) to perform vector length comparisons
            if ((adventurer.gameObject.transform.position - new Vector3(13, 1, -13)).sqrMagnitude < closestPoint.sqrMagnitude)
            {
                closestPoint = new(13, 1, -13);
            }
            if ((adventurer.gameObject.transform.position - new Vector3(-13, 1, 13)).sqrMagnitude < closestPoint.sqrMagnitude)
                closestPoint = new(-13, 1, 13);
            if ((adventurer.gameObject.transform.position - new Vector3(-13, 1, -13)).sqrMagnitude < closestPoint.sqrMagnitude)
                closestPoint = new(-13, 1, -13);
            adventurer.SetNavAgentGoal(closestPoint);
        }
        else
        {
            Debug.Log("We have a problem");
        }
    }

    public override bool Terminate(AdventurerBrain adventurer, WorldState worldState)
    {
        if((adventurer.GetNavAgent().destination - adventurer.transform.position).sqrMagnitude < 3f && adventurer.GetNavAgent().velocity.sqrMagnitude < 0.01)
        {
            adventurer.TaskComplete();
            adventurer.GetNavAgent().isStopped = true;
            adventurer.GetNavAgent().ResetPath();
            Debug.Log("Arrived");
            return true;
        }
        return false;
    }

    public override void Post(AdventurerBrain adventurer, WorldState state)
    {
        base.Post(adventurer, state);
        state.ObjectNextTo = objectPathedTo;
    }
}
