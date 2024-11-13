using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCharacter : AdventurerBrain
{
    /*private int health = 4;
    private WorldState worldState;

    public override void Awake()
    {
        base.Awake();
        worldState = new WorldState();
    }

    private void Update()
    {
        Vector3 thisPos = transform.position;

        //Updating the worldState variables for this adventurer
        worldState.ChestClaimed = Sensors.Instance.adventurerClaimingChest != null;
        worldState.CanHoldChest = worldState.ChestClaimed && Sensors.Instance.adventurerClaimingChest == this && Vector3.Distance(thisPos, navGoal.transform.position) < 2f;
        worldState.CanAttack = true;
        worldState.IsHoldingChest = Sensors.Instance.adventurerHoldingChest == this;

        if (manager != null && manager.getAdventurerList()[1] == this.gameObject)
            Debug.Log(this.gameObject.name + ", " + worldState.ChestClaimed + ", " + worldState.CanHoldChest + ", " + worldState.CanAttack + ", " + worldState.IsHoldingChest);
    }*/
}
