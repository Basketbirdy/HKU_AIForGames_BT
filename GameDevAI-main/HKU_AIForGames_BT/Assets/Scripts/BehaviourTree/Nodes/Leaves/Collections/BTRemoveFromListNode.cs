using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BTRemoveFromListNode<T> : BTBaseNode
{
    private string listBBVariable;
    private string valueBBVariable;

    private BlackboardType listBlackboardType;
    private BlackboardType valueBlackboardType;

    public BTRemoveFromListNode(string _listBBVariable, string _valueBBVariable, BlackboardType _listBlackboardType, BlackboardType _valueBlackboardType)
    {
        listBBVariable = _listBBVariable;
        valueBBVariable = _valueBBVariable;
        
        listBlackboardType = _listBlackboardType;
        valueBlackboardType = _valueBlackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        List<T> list;
        T value;

        if (listBlackboardType != BlackboardType.LOCAL) { list = GlobalBlackboard.instance.GetGlobalVariable<List<T>>(listBBVariable, listBlackboardType); }
        else { list = blackboard.GetVariable<List<T>>(listBBVariable); }

        if(list == null) { return TaskStatus.FAILURE; }

        if (valueBlackboardType != BlackboardType.LOCAL) { value = GlobalBlackboard.instance.GetGlobalVariable<T>(valueBBVariable, valueBlackboardType); }
        else { value = blackboard.GetVariable<T>(valueBBVariable); }

        if (!list.Contains(value)) { return TaskStatus.FAILURE; }

        list.Remove(value);
        if (valueBlackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<List<T>>(valueBBVariable, list, valueBlackboardType); }
        else { blackboard.SetVariable<List<T>>(valueBBVariable, list); }

        return TaskStatus.SUCCESS;
    }
}
