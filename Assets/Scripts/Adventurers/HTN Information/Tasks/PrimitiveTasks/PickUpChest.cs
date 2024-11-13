using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpChest : PrimitiveTask
{
    private float pickUpStartTime;
    private float pickUpDuration = 3f;
    public override string name => "Pickup Chest";

    public PickUpChest() 
    {
        
    }

    public override void Start(AdventurerBrain adventurer)
    {
        adventurer.GetNavAgent().velocity = Vector3.zero;
        pickUpStartTime = Time.time;
    }

    public override bool Terminate(AdventurerBrain adventurer, WorldState worldState)
    {
        //If the adventurer is not the chosen chest carrier, return false and roll back
        //if (!worldState.ChosenChestCarrier)
         //   return false;
        if(Time.time > pickUpStartTime + pickUpDuration)
        {
            adventurer.TaskComplete();
            return true;
        }
        return false;
    }

    public override void Post(AdventurerBrain adventurer, WorldState state)
    {
        base.Post(adventurer, state);
        adventurer.SetIsHoldingChest(true);
        adventurer.GetNavGoal().GetComponent<NavMeshObstacle>().enabled = false;
        adventurer.GetNavGoal().transform.parent = adventurer.GetPickingUpChestTransform();
        adventurer.GetNavGoal().transform.localPosition = new Vector3(0, 0, 0);
        adventurer.GetNavGoal().transform.localRotation = Quaternion.identity;

        state.GetMinotaur().GetComponent<Minotaur>().SetSomeoneHasChest(true);
        state.GetMinotaur().GetComponent<Minotaur>().chestHolder = adventurer.gameObject;
    }
}
