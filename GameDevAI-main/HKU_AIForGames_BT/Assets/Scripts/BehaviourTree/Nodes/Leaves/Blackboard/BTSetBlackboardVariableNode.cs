using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetBlackboardVariableNode<T> : BTBaseNode
{
    private bool global;
    private GlobalBlackboardType globalBlackboard;

    private string variableToSetName;
    private T newValue;
    private string altNewValueName;

    public BTSetBlackboardVariableNode(string _variableToSetName, T _newValue, bool _global, GlobalBlackboardType _globalBlackboard = GlobalBlackboardType.GLOBAL)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        newValue = _newValue;

        global = _global;
        globalBlackboard = _globalBlackboard;
    }

    public BTSetBlackboardVariableNode(string _variableToSetName, string _newValueVariableName, bool _global, GlobalBlackboardType _globalBlackboard = GlobalBlackboardType.GLOBAL)
    {
        altNewValueName = default(string);

        variableToSetName = _variableToSetName;
        altNewValueName = _newValueVariableName;

        global = _global;
        globalBlackboard = _globalBlackboard;
    }

    protected override TaskStatus OnUpdate()
    {
        if (global)
        {
            if (altNewValueName == default(string))
            {
                GlobalBlackboard.instance.SetGlobalVariable<T>(variableToSetName, newValue, globalBlackboard);
                Debug.Log($"Setting global variable: {variableToSetName}, to {newValue}");
            }
            else
            {
                GlobalBlackboard.instance.SetGlobalVariable<T>(variableToSetName, GlobalBlackboard.instance.GetGlobalVariable<T>(altNewValueName, globalBlackboard), globalBlackboard);
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
