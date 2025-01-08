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
    private Vector3 selfPos;
    private Vector3 targetPos;
    private ConditionalCheckType checkType;

    private float range;

    public BTTargetWithinDistanceNode(Transform _self, Transform _target, float _distance, ConditionalCheckType _checkType, BTBaseNode _child) : base(_child)
    {
        selfPos = _self.position;
        targetPos = _target.position;
        checkType = _checkType;
        range = _distance;
    }

    public BTTargetWithinDistanceNode(Transform _self, Vector3 _target, float _range, ConditionalCheckType _checkType, BTBaseNode _child) : base(_child)
    {
        selfPos = _self.position;
        targetPos = _target;
        checkType = _checkType;
        range = _range;
    }

    protected override bool TryCondition()
    {
        float distance = Vector3.Distance(selfPos, targetPos);

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

        return state;
    }
}
