using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MinotaurChaseState : MinotaurState
{
    private GameObject targetAdventurer;
    public MinotaurChaseState(Minotaur minotaur, MinotaurStateMachine minotaurStateMachine) : base(minotaur, minotaurStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Minotaur.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    { 
        base.EnterState();

        float tempAggro = 0f;
        //Check which adventurer has the largest Aggro Value, then set initial target to that object
        for(int i = 0; i<minotaur.GetAdventurerManager().getAdventurerList().Count; i++)
        {
            if (minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetAggroValue() > tempAggro) 
            {
                tempAggro = minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetAggroValue();
                targetAdventurer = minotaur.GetAdventurerManager().getAdventurerList()[i];
            }
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //Keep Checking if a new higher aggro value target is detected
        float tempAggro = 0f;
        for (int i = 0; i < minotaur.GetAdventurerManager().getAdventurerList().Count; i++)
        {
            if (minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetHoldingChest())
            {
                targetAdventurer = minotaur.GetAdventurerManager().getAdventurerList()[i];
                break;
            }
            if (minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetAggroValue() > tempAggro)
            {
                tempAggro = minotaur.GetAdventurerManager().getAdventurerList()[i].GetComponent<AdventurerBrain>().GetAggroValue();
                targetAdventurer = minotaur.GetAdventurerManager().getAdventurerList()[i];
            }
        }

        //Path to the highest aggro target
        Debug.Log(targetAdventurer.name);
        minotaur.GetNavAgent().destination = targetAdventurer.transform.position;

        //If the minotaur is within striking distance of the adventurer, attack the adventurer
        if(minotaur.IsWithinStrikingDistance)
        {
            minotaurStateMachine.ChangeState(minotaur.AttackState);
        }
    }
}
