using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// Leaf node that checks if a timer has ended or not, 
/// <br></br> returns success if it has, failure if not
/// </summary>
public class BTTimerCheckNode : BTBaseNode
{
    private string timerBBVariable;

    private bool global;
    private GlobalBlackboardType blackboardType;

    public BTTimerCheckNode(string _timerBBVariable, bool _global, GlobalBlackboardType _blackboardType)
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
        float timer = 0;
        if (global) { timer = GlobalBlackboard.instance.GetGlobalVariable<float>(timerBBVariable, blackboardType); }
        else { timer = blackboard.GetVariable<float>(timerBBVariable); }

        if (Time.time >= timer) { return TaskStatus.SUCCESS; }
        else { return TaskStatus.FAILURE; }
    }

    public override void OnReset()
    {

    }
}
