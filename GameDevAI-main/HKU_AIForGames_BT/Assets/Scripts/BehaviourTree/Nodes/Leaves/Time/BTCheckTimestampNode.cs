using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckTimestampNode : BTBaseNode
{
    private string timestampBBVariable;
    private float delay;

    private BlackboardType blackboardType;

    public BTCheckTimestampNode(string _timestampBBVariable, float _delay, BlackboardType _blackboardType)
    {
        timestampBBVariable = _timestampBBVariable;
        delay = _delay;

        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        float timestamp;
        if (blackboardType != BlackboardType.LOCAL) { timestamp = GlobalBlackboard.instance.GetGlobalVariable<float>(timestampBBVariable, blackboardType); }
        else { timestamp = blackboard.GetVariable<float>(timestampBBVariable); }

        if (Time.time >= timestamp + delay) { return TaskStatus.SUCCESS; }
        else { return TaskStatus.FAILURE; }
    }
}
