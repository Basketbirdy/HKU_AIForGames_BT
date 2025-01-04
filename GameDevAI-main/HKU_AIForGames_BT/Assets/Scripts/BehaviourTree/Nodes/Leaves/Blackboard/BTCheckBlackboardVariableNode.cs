using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckBlackboardVariableNode<T> : BTBaseNode where T : struct
{
    private string variableToCheckName;
    private T desiredValue;

    public BTCheckBlackboardVariableNode(string _variableToCheckName, T _desiredValue)
    {
        variableToCheckName = _variableToCheckName;
        desiredValue = _desiredValue;
    }

    protected override TaskStatus OnUpdate()
    {
        T value = blackboard.GetVariable<T>(variableToCheckName);

        if(value.Equals(desiredValue))
        {
            Debug.Log($"value: {value} is equal to desiredValue: {desiredValue}");
            return TaskStatus.SUCCESS;
        }

        Debug.Log($"value: {value} is NOT equal to desiredValue: {desiredValue}");
        return TaskStatus.FAILURE;
    }
}
