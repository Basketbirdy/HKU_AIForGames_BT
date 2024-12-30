using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBlackboardVariableNode<T> : BTBaseNode
{
    private string variableToSetName;
    private T newValue;
    private string altNewValueName;

    public BTSetBlackboardVariableNode(string _variableToSetName, T _newValue)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        newValue = _newValue;
    }

    public BTSetBlackboardVariableNode(string _variableToSetName, string _newValueVariableName)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        altNewValueName = _newValueVariableName;
    }

    protected override TaskStatus OnUpdate()
    {
        if(altNewValueName == default(string))
        {
            blackboard.SetVariable<T>(variableToSetName, newValue);
        }
        else
        {
            blackboard.SetVariable<T>(variableToSetName, blackboard.GetVariable<T>(altNewValueName));
        }
        return TaskStatus.SUCCESS;
    }
}
