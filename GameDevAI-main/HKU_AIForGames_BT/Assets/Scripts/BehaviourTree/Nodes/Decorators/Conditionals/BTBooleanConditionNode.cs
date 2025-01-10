using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTBooleanConditionNode : BTConditionDecoratorNode
{
    string variableName;
    bool variable;
    bool compareValue;

    bool global;
    GlobalBlackboardType globalBlackboardType;

    public BTBooleanConditionNode(string _variableName, bool _compareValue, bool _global, GlobalBlackboardType _globalBlackboardType, BTBaseNode _child) : base(_child) 
    {
        variableName = _variableName;
        compareValue = _compareValue;

        global = _global;
        globalBlackboardType = _globalBlackboardType;
    }

    public BTBooleanConditionNode(bool _variable, bool _compareValue, bool _global, GlobalBlackboardType _globalBlackboardType, BTBaseNode _child) : base(_child)
    {
        variable = _variable;
        compareValue = _compareValue;

        global = _global;
        globalBlackboardType= _globalBlackboardType;
    }

    protected override bool TryCondition()
    {
        bool value;
        if(variableName == "") { value = variable; }
        else if(global) { value = GlobalBlackboard.instance.GetGlobalVariable<bool>(variableName, globalBlackboardType); }
        else { value = blackboard.GetVariable<bool>(variableName); }

        if(value == compareValue) { return true; }
        return false;
    }
}
