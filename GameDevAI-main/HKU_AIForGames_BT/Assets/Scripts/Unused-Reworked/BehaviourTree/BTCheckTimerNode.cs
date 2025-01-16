using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class BTCheckTimerNode : BTConditionalDecoratorNode
{
    private string timerBBVariable;
    private float timerDuration;

    private BlackboardType blackboardType;

    public BTCheckTimerNode(string _timerBBVariable, float _timerDuration, BlackboardType _blackboardType, BTBaseNode _child) : base(_child) 
    {
        timerBBVariable = _timerBBVariable;
        timerDuration = _timerDuration;

        blackboardType = _blackboardType;
    }

    protected override bool TryCondition()
    {
        float elapsedTime = 0f;
        if (blackboardType != BlackboardType.LOCAL) { elapsedTime = GlobalBlackboard.instance.GetGlobalVariable<float>(timerBBVariable, blackboardType); }
        else { elapsedTime = blackboard.GetVariable<float>(timerBBVariable); }

        Debug.Log($"elapsedTime: {elapsedTime}");

        if(elapsedTime >= timerDuration) { return false; }
        else 
        {
            elapsedTime += Time.deltaTime;
            if (blackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<float>(timerBBVariable, elapsedTime, blackboardType); }
            else { blackboard.SetVariable<float>(timerBBVariable, elapsedTime); }
            return true; 
        }
    }
}
