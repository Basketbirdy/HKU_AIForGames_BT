using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTFloatConditionNode : BTConditionDecoratorNode
{
    private string valueBBVariable;
    private float compareValue;
    private ConditionalCheckType conditionType;

    private bool global;
    private GlobalBlackboardType globalBlackboardType;

    public BTFloatConditionNode(string _valueBBVariable, float _compareValue, ConditionalCheckType _conditionalCheckType, bool _global, GlobalBlackboardType _globalBlackboardType, BTBaseNode _child) : base(_child)
    {
        valueBBVariable = _valueBBVariable;
        compareValue = _compareValue;
        conditionType = _conditionalCheckType;

        global = _global;
        globalBlackboardType = _globalBlackboardType;
    }

    protected override bool TryCondition()
    {
        float value = 0;
        if (global) { value = GlobalBlackboard.instance.GetGlobalVariable<float>(valueBBVariable, globalBlackboardType); }
        else { value = blackboard.GetVariable<float>(valueBBVariable); }

        bool state = false;
        switch (conditionType)
        {
            case ConditionalCheckType.GreaterThan:
                if (value > compareValue) { state = true; }
                break;
            case ConditionalCheckType.GreaterThanOrEqual:
                if (value >= compareValue) { state = true; }
                break;
            case ConditionalCheckType.LessThan:
                if (value < compareValue) { state = true; }
                break;
            case ConditionalCheckType.LessThanOrEqual:
                if (value <= compareValue) { state = true; }
                break;
            case ConditionalCheckType.Equal:
                if (value == compareValue) { state = true; }
                break;
        }

        return state;
    }
}
