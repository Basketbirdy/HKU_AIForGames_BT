using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckObjectInRangeNode : BTBaseNode
{
    private Transform transform;
    private float range;
    private LayerMask checkMask;

    public BTCheckObjectInRangeNode(float _range, Transform _transform, LayerMask _checkMask)
    {
        range = _range;
        transform = _transform;
        checkMask = _checkMask;
    }

    protected override TaskStatus OnUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, checkMask);
        Debug.Log($"Checking for object in range");

        if(colliders.Length == 0) { return TaskStatus.FAILURE; }

        return TaskStatus.SUCCESS;
    }
}
