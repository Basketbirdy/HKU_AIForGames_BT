using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAddToListNode<T> : BTBaseNode
{
    private string listBBVariable;
    private T value;

    private BlackboardType blackboardType;

    public BTAddToListNode(string _listBBVariable, T _value, BlackboardType _blackboardType)
    {
        listBBVariable = _listBBVariable;
        value = _value;
    } 

    protected override TaskStatus OnUpdate()
    {
        List<T> list;

        if(blackboardType != BlackboardType.LOCAL) { list = GlobalBlackboard.instance.GetGlobalVariable<List<T>>(listBBVariable, blackboardType); }
        else { list = blackboard.GetVariable<List<T>>(listBBVariable); }

        list.Add(value);

        return TaskStatus.SUCCESS;
    }
}
