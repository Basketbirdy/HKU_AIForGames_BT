using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTimestampNode : BTBaseNode
{
    private string timestampBBVariable;
    private BlackboardType blackboardType;

    public BTSetTimestampNode(string _timestampBBVariable, BlackboardType _blackboardType)
    {
        timestampBBVariable = _timestampBBVariable;
        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        float timestamp = Time.time;
        
        if(blackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<float>(timestampBBVariable, timestamp, blackboardType); }
        else { blackboard.SetVariable<float>(timestampBBVariable, timestamp); }
        //Debug.Log($"[BTSetTimestampNode; {self.gameObject.name}] setting '{timestampBBVariable}' to {timestamp}");

        return TaskStatus.SUCCESS;
    }
}
