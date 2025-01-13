using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that starts a timer by setting the timestamp to current time + delay
/// <br></br> returns failure if delay is 0, otherwise always returns success
/// </summary>
public class BTTimerStartNode : BTBaseNode
{
    private string timerBBVariable;
    private float delay;

    private BlackboardType blackboardType;

    public BTTimerStartNode(string _timerBBVariable, float _delay, BlackboardType _blackboardType)
    {
        timerBBVariable = _timerBBVariable;
        delay = _delay;

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
        if(delay == 0) { return TaskStatus.FAILURE; }

        if (blackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, Time.time + delay, blackboardType); }
        else {  blackboard.SetVariable<float>(timerBBVariable, Time.time + delay); }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {

    }
}
