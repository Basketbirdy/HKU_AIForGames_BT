using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    private bool global;
    private GlobalBlackboardType blackboardType;

    public BTTimerResetNode(string _timerBBVariable, bool _global, GlobalBlackboardType _blackboardType)
    {
        timerBBVariable = _timerBBVariable;

        global = _global;
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
        if (global) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, 0, blackboardType); }
        else { blackboard.SetVariable<float>(timerBBVariable, 0); }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {

    }
}
