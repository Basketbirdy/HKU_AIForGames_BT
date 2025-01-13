using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBlackboardVariableNode<T> : BTBaseNode
{
    private string variableToSetName;
    private T newValue;
    private string altNewValueName;

    private BlackboardType blackboardType;

    public BTSetBlackboardVariableNode(string _variableToSetName, T _newValue, BlackboardType _blackboardType = BlackboardType.GLOBAL)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        newValue = _newValue;

        blackboardType = _blackboardType;
    }

    public BTSetBlackboardVariableNode(string _variableToSetName, string _newValueVariableName, BlackboardType _blackboardType = BlackboardType.GLOBAL)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        altNewValueName = _newValueVariableName;

        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        if (blackboardType != BlackboardType.LOCAL)
        {
            if (altNewValueName == default(string))
            {
                GlobalBlackboard.instance.SetGlobalVariable<T>(variableToSetName, newValue, blackboardType);
                Debug.Log($"Setting global variable: {variableToSetName}, to {newValue}");
            }
            else
            {
                GlobalBlackboard.instance.SetGlobalVariable<T>(variableToSetName, GlobalBlackboard.instance.GetGlobalVariable<T>(altNewValueName, blackboardType), blackboardType);
            }
        }
        else
        {
            if(altNewValueName == default(string))
            {
                blackboard.SetVariable<T>(variableToSetName, newValue);
            }
            else
            {
                blackboard.SetVariable<T>(variableToSetName, blackboard.GetVariable<T>(altNewValueName));
            }
        }
        return TaskStatus.SUCCESS;
    }
}
