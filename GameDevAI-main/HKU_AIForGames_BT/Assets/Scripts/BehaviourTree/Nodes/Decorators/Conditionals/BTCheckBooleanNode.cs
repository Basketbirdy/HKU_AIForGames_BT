using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTCheckBooleanNode : BTConditionalDecoratorNode
{
    string booleanBBVariable;
    bool boolean;
    bool compareValue;

    BlackboardType blackboardType;

    public BTCheckBooleanNode(string _booleanBBVariable, bool _compareValue, BlackboardType _blackboardType, BTBaseNode _child) : base(_child) 
    {
        booleanBBVariable = _booleanBBVariable;
        compareValue = _compareValue;

        blackboardType = _blackboardType;
    }

    public BTCheckBooleanNode(bool _boolean, bool _compareValue, BlackboardType _blackboardType, BTBaseNode _child) : base(_child)
    {
        boolean = _boolean;
        compareValue = _compareValue;

        blackboardType= _blackboardType;
    }

    protected override bool TryCondition()
    {
        bool value;
        if(booleanBBVariable == "") { value = boolean; }
        else if(blackboardType != BlackboardType.LOCAL) { value = GlobalBlackboard.instance.GetGlobalVariable<bool>(booleanBBVariable, blackboardType); }
        else { value = blackboard.GetVariable<bool>(booleanBBVariable); }

        if(value == compareValue) { return true; }
        return false;
    }
}
