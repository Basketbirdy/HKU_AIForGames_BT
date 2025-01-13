using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTBooleanConditionNode : BTConditionDecoratorNode
{
    string variableName;
    bool variable;
    bool compareValue;

    BlackboardType blackboardType;

    public BTBooleanConditionNode(string _variableName, bool _compareValue, BlackboardType _blackboardType, BTBaseNode _child) : base(_child) 
    {
        variableName = _variableName;
        compareValue = _compareValue;

        blackboardType = _blackboardType;
    }

    public BTBooleanConditionNode(bool _variable, bool _compareValue, BlackboardType _blackboardType, BTBaseNode _child) : base(_child)
    {
        variable = _variable;
        compareValue = _compareValue;

        blackboardType= _blackboardType;
    }

    protected override bool TryCondition()
    {
        bool value;
        if(variableName == "") { value = variable; }
        else if(blackboardType != BlackboardType.LOCAL) { value = GlobalBlackboard.instance.GetGlobalVariable<bool>(variableName, blackboardType); }
        else { value = blackboard.GetVariable<bool>(variableName); }

        if(value == compareValue) { return true; }
        return false;
    }
}
