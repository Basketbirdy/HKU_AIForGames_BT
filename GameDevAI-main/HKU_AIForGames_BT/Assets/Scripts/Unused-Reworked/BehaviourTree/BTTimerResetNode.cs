using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that resets a timers timestamp to 0 
/// <br></br> A timer will return success after checking/awaiting when reset
/// <br></br> always returns success
/// </summary>
public class BTTimerResetNode : BTBaseNode
{
    private string timerBBVariable;
    private float delay;

    private BlackboardType blackboardType;

    public BTTimerResetNode(string _timerBBVariable, BlackboardType _blackboardType)
    {
        timerBBVariable = _timerBBVariable;

        blackboardType = _blackboardType;
    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    protected override TaskStatus OnUpdate()
    {
        if (blackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, 0, blackboardType); }
        else { blackboard.SetVariable<float>(timerBBVariable, 0); }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {

    }
}
