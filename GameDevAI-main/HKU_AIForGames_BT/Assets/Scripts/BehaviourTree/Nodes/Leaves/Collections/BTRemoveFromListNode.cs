using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BTRemoveFromListNode<T> : BTBaseNode
{
    private string listBBVariable;
    private T value;

    private BlackboardType blackboardType;

    public BTRemoveFromListNode(string _listBBVariable, T _value, BlackboardType _blackboardType)
    {
        listBBVariable = _listBBVariable;
        value = _value;
    }

    protected override TaskStatus OnUpdate()
    {
        List<T> list;

        if (blackboardType != BlackboardType.LOCAL) { list = GlobalBlackboard.instance.GetGlobalVariable<List<T>>(listBBVariable, blackboardType); }
        else { list = blackboard.GetVariable<List<T>>(listBBVariable); }

        if(!list.Contains(value)) { return TaskStatus.FAILURE; }

        list.Remove(value);
        return TaskStatus.SUCCESS;
    }
}
