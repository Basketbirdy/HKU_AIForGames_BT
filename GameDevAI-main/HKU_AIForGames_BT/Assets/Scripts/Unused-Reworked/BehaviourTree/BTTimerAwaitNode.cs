using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that waits for a timer to end before returning
/// <br></br> returns success if it has ended, running if not
/// </summary>
public class BTTimerAwaitNode : BTBaseNode
{
    private string timerBBVariable;

    private BlackboardType blackboardType;

    public BTTimerAwaitNode(string _timerBBVariable, bool _global, BlackboardType _blackboardType)
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
        float timer = 0;
        if (blackboardType != BlackboardType.LOCAL) { timer = GlobalBlackboard.instance.GetGlobalVariable<float>(timerBBVariable, blackboardType); }
        else { timer = blackboard.GetVariable<float>(timerBBVariable); }

        if(Time.time >= timer) { return TaskStatus.SUCCESS; }
        else { return TaskStatus.RUNNING; }
    }

    public override void OnReset()
    {

    }
}
