using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckObjectInRangeNode : BTBaseNode
{
    private Transform transform;
    private LayerMask checkMask;

    public BTCheckObjectInRangeNode(Transform _transform, LayerMask _checkMask)
    {
        transform = _transform;
        checkMask = _checkMask;
    }

    protected override TaskStatus OnUpdate()
    {

        float range = blackboard.GetVariable<float>(VariableNames.CHECK_CURRENTRANGE);
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, checkMask);
        Debug.Log($"Checking for object in range");

        if(colliders.Length == 0) { return TaskStatus.FAILURE; }

        return TaskStatus.SUCCESS;
    }
}
