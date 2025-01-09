using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTConditionDecoratorNode : BTDecoratorNode
{
    public BTConditionDecoratorNode(BTBaseNode _child) : base(_child) { }

    protected override TaskStatus OnUpdate()
    {
        bool condition = TryCondition();
        if (!condition) 
        {
            OnReset();
            return TaskStatus.FAILURE; 
        }

        TaskStatus result = child.Tick();
        return result;
    }

    protected abstract bool TryCondition();
}
