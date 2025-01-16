using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTCheckFloatNode : BTConditionalDecoratorNode
{
    private string floatBBVariable;
    private float compareValue;
    private ConditionalCheckType conditionType;

    private BlackboardType blackboardType;

    public BTCheckFloatNode(string _floatBBVariable, float _compareValue, ConditionalCheckType _conditionalCheckType, BlackboardType _blackboardType, BTBaseNode _child) : base(_child)
    {
        floatBBVariable = _floatBBVariable;
        compareValue = _compareValue;
        conditionType = _conditionalCheckType;

        blackboardType = _blackboardType;
    }

    protected override bool TryCondition()
    {
        float value = 0;
        if (blackboardType != BlackboardType.LOCAL) { value = GlobalBlackboard.instance.GetGlobalVariable<float>(floatBBVariable, blackboardType); }
        else { value = blackboard.GetVariable<float>(floatBBVariable); }

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
