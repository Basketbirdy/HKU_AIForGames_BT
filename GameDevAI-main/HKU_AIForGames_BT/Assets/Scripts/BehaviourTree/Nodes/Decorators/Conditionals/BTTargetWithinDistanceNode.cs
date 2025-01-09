using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ConditionalCheckType
{
    GreaterThan,
    GreaterThanOrEqual,
    LessThan,
    LessThanOrEqual,
    Equal,
}
public class BTTargetWithinDistanceNode : BTConditionDecoratorNode
{
    private string targetBBVariable;
    private float range;
    private ConditionalCheckType checkType;

    public BTTargetWithinDistanceNode(string _targetBBVariable, float _distance, ConditionalCheckType _checkType, BTBaseNode _child) : base(_child)
    {
        targetBBVariable = _targetBBVariable;
        checkType = _checkType;
        range = _distance;
    }

    protected override bool TryCondition()
    {
        Vector3 targetPosition = blackboard.GetVariable<Transform>(targetBBVariable).position;
        float distance = Vector3.Distance(self.position, targetPosition);

        bool state = false;

        switch (checkType)
        {
            case ConditionalCheckType.GreaterThan:
                if(distance > range) { state = true; }
                break;
            case ConditionalCheckType.GreaterThanOrEqual:
                if(distance >= range) { state = true; }
                break;
            case ConditionalCheckType.LessThan:
                if(distance < range) { state = true; }
                break;
            case ConditionalCheckType.LessThanOrEqual:
                if(distance <= range) { state = true; }
                break;
            case ConditionalCheckType.Equal:
                if(distance == range) { state = true; }
                break;
        }

        //Debug.Log($"Condition distance: {distance}");
        //Debug.Log($"Tried condition: {state}");
        return state;
    }
}
