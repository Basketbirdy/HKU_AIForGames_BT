using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTBooleanConditionNode : BTConditionDecoratorNode
{
    string variableName;
    bool variable;
    bool compareValue;

    public BTBooleanConditionNode(BTBaseNode _child, string _variableName, bool _compareValue) : base(_child) 
    {
        variableName = _variableName;
        compareValue = _compareValue;
    }

    public BTBooleanConditionNode(BTBaseNode _child, bool _variable, bool _compareValue) : base(_child)
    {
        variable = _variable;
        compareValue = _compareValue;
    }

    protected override bool TryCondition()
    {
        bool value;
        if(variableName == "") { value = variable; }
        else { value = blackboard.GetVariable<bool>(variableName); }

        if(value == compareValue) { return true; }
        return false;
    }
}
