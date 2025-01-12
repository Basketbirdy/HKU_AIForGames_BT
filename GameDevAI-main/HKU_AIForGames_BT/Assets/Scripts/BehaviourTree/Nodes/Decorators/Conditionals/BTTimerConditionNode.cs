using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BTTimerConditionNode : BTConditionDecoratorNode
{
    private string timerBBVariable;
    private float timerDuration;
    private bool global;
    private GlobalBlackboardType globalBlackboard;

    public BTTimerConditionNode(string _timerBBVariable, float _timerDuration, bool _global, GlobalBlackboardType _globalBlackboard, BTBaseNode _child) : base(_child) 
    {
        timerBBVariable = _timerBBVariable;
        timerDuration = _timerDuration;
        global = _global;
        globalBlackboard = _globalBlackboard;
    }

    protected override bool TryCondition()
    {
        float elapsedTime = 0f;
        if (global) { elapsedTime = GlobalBlackboard.instance.GetGlobalVariable<float>(timerBBVariable, globalBlackboard); }
        else { elapsedTime = blackboard.GetVariable<float>(timerBBVariable); }

        Debug.Log($"elapsedTime: {elapsedTime}");

        if(elapsedTime >= timerDuration) { return false; }
        else 
        {
            elapsedTime += Time.deltaTime;
            if (global) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, elapsedTime, globalBlackboard); }
            else { blackboard.SetVariable<float>(timerBBVariable, elapsedTime); }
            return true; 
        }
    }

    public override void OnReset()
    {
        //if (global) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, 0, globalBlackboard); }
        //else { blackboard.SetVariable<float>(timerBBVariable, 0); }
    }
}
