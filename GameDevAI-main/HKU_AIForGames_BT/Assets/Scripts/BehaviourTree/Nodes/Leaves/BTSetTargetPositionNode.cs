using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTargetPositionNode : BTBaseNode
{
    private Vector3 targetPosition;

    public BTSetTargetPositionNode(Vector3 _targetPosition)
    {
        targetPosition = _targetPosition;
    }

    protected override TaskStatus OnUpdate()
    {
        blackboard.SetVariable<Vector3>(VariableNames.PATHING_TARGETPOSITION, targetPosition);
        return TaskStatus.SUCCESS;
    }
}
