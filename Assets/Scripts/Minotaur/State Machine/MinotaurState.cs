using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurState
{
    protected Minotaur minotaur;
    protected MinotaurStateMachine minotaurStateMachine;

    public MinotaurState(Minotaur minotaur, MinotaurStateMachine minotaurStateMachine)
    {
        this.minotaur = minotaur;
        this.minotaurStateMachine = minotaurStateMachine;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FrameUpdate() { }
    public virtual void AnimationTriggerEvent(Minotaur.AnimationTriggerType triggerType) { }
}
