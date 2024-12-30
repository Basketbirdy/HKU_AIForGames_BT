using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTargetNode : BTBaseNode
{
    private Transform target;

    public BTSetTargetNode(Transform _target)
    {
        target = _target;
    }

    protected override TaskStatus OnUpdate()
    {
        blackboard.SetVariable<Transform>(VariableNames.PATHING_TARGETTRANSFORM, target);
        return TaskStatus.SUCCESS;
    }
}
