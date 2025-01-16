using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIncrementIndexNode<T> : BTBaseNode
{
    private string currentIndexBBVariable;
    private string collectionBbVariable;

    public BTIncrementIndexNode(string _currentIndexVariableName, string _collectionVariableName)
    {
        currentIndexBBVariable = _currentIndexVariableName;
        collectionBbVariable = _collectionVariableName;
    }

    protected override TaskStatus OnUpdate()
    {
        int index = blackboard.GetVariable<int>(currentIndexBBVariable);
        int length = blackboard.GetVariable<T[]>(collectionBbVariable).Length;

        int newIndex = (index + 1) % length;
        blackboard.SetVariable<int>(currentIndexBBVariable, newIndex);

        return TaskStatus.SUCCESS;
    }
}
