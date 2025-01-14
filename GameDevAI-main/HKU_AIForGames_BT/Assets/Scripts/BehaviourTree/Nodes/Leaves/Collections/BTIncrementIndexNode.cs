using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTIncrementIndexNode<T> : BTBaseNode
{
    private string currentIndexVariableName;
    private string collectionVariableName;

    public BTIncrementIndexNode(string _currentIndexVariableName, string _collectionVariableName)
    {
        currentIndexVariableName = _currentIndexVariableName;
        collectionVariableName = _collectionVariableName;
    }

    protected override TaskStatus OnUpdate()
    {
        int index = blackboard.GetVariable<int>(currentIndexVariableName);
        int length = blackboard.GetVariable<T[]>(collectionVariableName).Length;

        int newIndex = (index + 1) % length;
        blackboard.SetVariable<int>(currentIndexVariableName, newIndex);

        return TaskStatus.SUCCESS;
    }
}
