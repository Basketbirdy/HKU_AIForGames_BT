using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBlackboardVariableNode<T> : BTBaseNode
{
    private string variableToSet;
    private T value;

    public BTSetBlackboardVariableNode(string _variableToSet, T _value)
    {
        variableToSet = _variableToSet;
        value = _value;
    }

    protected override TaskStatus OnUpdate()
    {
        blackboard.SetVariable<T>(variableToSet, value);
        return TaskStatus.SUCCESS;
    }
}
