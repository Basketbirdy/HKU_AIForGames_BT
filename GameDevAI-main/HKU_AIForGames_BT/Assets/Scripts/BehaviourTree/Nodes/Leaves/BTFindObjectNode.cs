using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTFindObjectNode : BTBaseNode
{
    private float range;
    private LayerMask checkMask;

    public BTFindObjectNode(float _range, LayerMask _checkMask)
    {
        range = _range;
        checkMask = _checkMask;
    }

    protected override TaskStatus OnUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(self.position, range, checkMask);
        Debug.Log($"Checking for object in range");

        if(colliders.Length == 0) 
        {
            blackboard.SetVariable<Transform>(VariableNames.DATA_FOUNDOBJECT, null);
            return TaskStatus.FAILURE; 
        }

        Debug.Log($"Found object in range: {colliders[0].gameObject.name}");
        blackboard.SetVariable<Transform>(VariableNames.DATA_FOUNDOBJECT, colliders[0].transform);
        return TaskStatus.SUCCESS;
    }
}
