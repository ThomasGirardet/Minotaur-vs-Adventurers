using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MinotaurAttackState : MinotaurState
{
    private float timer = 0f;
    private float timeBetweenAttacks = 1f;

    public MinotaurAttackState(Minotaur minotaur, MinotaurStateMachine minotaurStateMachine) : base(minotaur, minotaurStateMachine)
    {

    }

    public override void AnimationTriggerEvent(Minotaur.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entering Attack State");

        minotaur.GetNavAgent().velocity = Vector3.zero;
        Attack();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private void Attack()
    {
        foreach (GameObject adventurer in minotaur.GetAdventurerManager().getAdventurerList())
        {
            if (Vector3.SqrMagnitude(minotaur.transform.position - adventurer.transform.position) < (minotaur.attackRange) * (minotaur.attackRange))
            {
                //Debug.Log("Adventurer " + adventurer.name + " Health -1");
            }
        }
    }

    public override void FrameUpdate()
    {
        minotaur.GetNavAgent().velocity = Vector3.zero;

        base.FrameUpdate();

        bool stayInAttack = false;

        //If someone has the chest and minotaur is within range, then keep attacking
        //Otherwise if someone has the chest but minotaur isn't within range, stayInAttack does not equal true and break out once the attack timer expires (later)
        //Else if no one has the chest, if someone is still within range, then continue attacking
        if(minotaur.GetSomeoneHasChest())
        {
            if(Vector3.SqrMagnitude(minotaur.transform.position - minotaur.chestHolder.transform.position) < (minotaur.attackRange) * (minotaur.attackRange))
            {
                stayInAttack = true;
            }
        }
        else 
        {
            foreach (GameObject adventurer in minotaur.GetAdventurerManager().getAdventurerList())
            {
                if (Vector3.SqrMagnitude(minotaur.transform.position - adventurer.transform.position) < (minotaur.attackRange) * (minotaur.attackRange))
                {
                    stayInAttack = true;
                    break;
                }
            }
        }

        if (timer > timeBetweenAttacks && !stayInAttack) 
        {
            minotaur.SetWithinStrikingDistance(false);
            minotaur.StateMachine.ChangeState(minotaur.ChaseState);
        }

        if (timer > timeBetweenAttacks)
        {
            timer = 0f;

            Attack();
        }

        timer += Time.deltaTime;
    }
}