using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurStateMachine
{
    public MinotaurState CurrentMinotaurState{ get; set; }

    public void Initialize(MinotaurState startingState)
    {
        CurrentMinotaurState = startingState;
        CurrentMinotaurState.EnterState();
    }

    public void ChangeState(MinotaurState nextState)
    {
        CurrentMinotaurState.ExitState();
        CurrentMinotaurState = nextState;
        CurrentMinotaurState.EnterState();
    }
}
