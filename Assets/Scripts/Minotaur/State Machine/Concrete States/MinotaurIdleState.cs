using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MinotaurIdleState : MinotaurState
{
    float idleTimer = 0;

    public MinotaurIdleState(Minotaur minotaur, MinotaurStateMachine minotaurStateMachine) : base(minotaur, minotaurStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Minotaur.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        //Turn Minotaur to face staircase
        /*if ((Mathf.Abs(minotaur.transform.position.x - minotaur.patrolPoint1.x) < 0.0001) && (Mathf.Abs(minotaur.transform.position.z - minotaur.patrolPoint1.z) < 0.0001))
        {
            //Vector with relation to minotaur's position for rotation
            Vector3 lookAtPos = new Vector3(0, 3, -1);
            Debug.DrawRay(minotaur.transform.position, lookAtPos, Color.red);
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos);
            minotaur.transform.rotation = Quaternion.Slerp(minotaur.transform.rotation, newRotation, Time.deltaTime * 8);

        }
        else if ((Mathf.Abs(minotaur.transform.position.x - minotaur.patrolPoint2.x) < 0.0001) && (Mathf.Abs(minotaur.transform.position.z - minotaur.patrolPoint2.z) < 0.0001))
        {
            //Vector with relation to minotaur's position for rotation
            Vector3 lookAtPos = new Vector3(1, 3, 0);
            Debug.DrawRay(minotaur.transform.position, lookAtPos, Color.red);
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos);
            minotaur.transform.rotation = Quaternion.Slerp(minotaur.transform.rotation, newRotation, Time.deltaTime * 8);
        }
        else if ((Mathf.Abs(minotaur.transform.position.x - minotaur.patrolPoint3.x) < 0.0001) && (Mathf.Abs(minotaur.transform.position.z - minotaur.patrolPoint3.z) < 0.0001))
        {
            //Vector with relation to minotaur's position for rotation
            Vector3 lookAtPos = new Vector3(0, 3, 1);
            Debug.DrawRay(minotaur.transform.position, lookAtPos, Color.red);
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos);
            minotaur.transform.rotation = Quaternion.Slerp(minotaur.transform.rotation, newRotation, Time.deltaTime * 8);
        }
        else if((Mathf.Abs(minotaur.transform.position.x - minotaur.patrolPoint4.x) < 0.0001) && (Mathf.Abs(minotaur.transform.position.z - minotaur.patrolPoint4.z) < 0.0001))
        {
            //Vector with relation to minotaur's position for rotation
            Vector3 lookAtPos = new Vector3(-1, 3, 0);
            Debug.DrawRay(minotaur.transform.position, lookAtPos, Color.red);
            Quaternion newRotation = Quaternion.LookRotation(lookAtPos);
            minotaur.transform.rotation = Quaternion.Slerp(minotaur.transform.rotation, newRotation, Time.deltaTime * 8);
        }*/
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        //If minotaur is aggroed while in idle, change to chase state
        if(minotaur.IsAggroed)
        {
            idleTimer = 0f;
            minotaur.SetIdlingStatus(false);
            minotaur.SetAggro(true);

            minotaur.StateMachine.ChangeState(minotaur.ChaseState);
        }

        //If idle timer expires, change to patrol state
        if (idleTimer > 3)
        {
            idleTimer = 0f;
            minotaur.SetIdlingStatus(false);
            minotaur.SetPatrollingStatus(true);

            minotaur.StateMachine.ChangeState(minotaur.PatrolState);
        }

        //Idle for 3 seconds
        idleTimer += Time.deltaTime;
    }
}
