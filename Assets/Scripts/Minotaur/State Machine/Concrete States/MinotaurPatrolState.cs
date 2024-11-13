using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurPatrolState : MinotaurState
{

    private int patrolDest = 0;


    public MinotaurPatrolState(Minotaur minotaur, MinotaurStateMachine minotaurStateMachine) : base(minotaur, minotaurStateMachine)
    {
    }

    public override void AnimationTriggerEvent(Minotaur.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (minotaur.IsAggroed)
        {
            minotaur.SetPatrollingStatus(false);
            minotaur.StateMachine.ChangeState(minotaur.ChaseState);
        }

        Patrol();

    }

    private void Patrol()
    {
        switch (patrolDest)
        {
            case 0:
                if (Mathf.Abs(minotaur.patrolPoint1.x - minotaur.transform.position.x)<0.0001  && Mathf.Abs(minotaur.patrolPoint1.z - minotaur.transform.position.z) < 0.0001)
                {
                    minotaur.SetPatrollingStatus(false);
                    minotaur.SetIdlingStatus(true);
                    patrolDest++;

                    minotaur.StateMachine.ChangeState(minotaur.IdleState);
                }
                else
                    minotaur.GetNavAgent().destination = minotaur.patrolPoint1;
                break;
            case 1:
                if (Mathf.Abs(minotaur.patrolPoint2.x - minotaur.transform.position.x) < 0.0001 && Mathf.Abs(minotaur.patrolPoint2.z - minotaur.transform.position.z) < 0.0001)
                {
                    minotaur.SetPatrollingStatus(false);
                    minotaur.SetIdlingStatus(true);
                    patrolDest++;

                    minotaur.StateMachine.ChangeState(minotaur.IdleState);
                }
                else
                    minotaur.GetNavAgent().destination = minotaur.patrolPoint2;
                break;
            case 2:
                if (Mathf.Abs(minotaur.patrolPoint3.x - minotaur.transform.position.x) < 0.0001 && Mathf.Abs(minotaur.patrolPoint3.z - minotaur.transform.position.z) < 0.0001)
                {
                    minotaur.SetPatrollingStatus(false);
                    minotaur.SetIdlingStatus(true);
                    patrolDest++;

                    minotaur.StateMachine.ChangeState(minotaur.IdleState);
                }
                else
                    minotaur.GetNavAgent().destination = minotaur.patrolPoint3;
                break;
            case 3:
                if (Mathf.Abs(minotaur.patrolPoint4.x - minotaur.transform.position.x) < 0.0001 && Mathf.Abs(minotaur.patrolPoint4.z - minotaur.transform.position.z) < 0.0001)
                {
                    minotaur.SetPatrollingStatus(false);
                    minotaur.SetIdlingStatus(true);
                    patrolDest = 0;

                    minotaur.StateMachine.ChangeState(minotaur.IdleState);
                }
                else
                    minotaur.GetNavAgent().destination = minotaur.patrolPoint4;
                break;
        }
    }
}
