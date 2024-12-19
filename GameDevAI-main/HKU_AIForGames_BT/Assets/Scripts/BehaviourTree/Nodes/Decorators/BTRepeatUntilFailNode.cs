using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a decorator node that repeats its child node until it returns a fail
/// </summary>
public class BTRepeatUntilFailNode : BTDecoratorNode
{
    private int count;

    public BTRepeatUntilFailNode(BTBaseNode _child) : base(_child) { }

    protected override void OnEnter()
    {
        count++;
    }

    protected override TaskStatus OnUpdate()
    {
        TaskStatus result = child.Tick();

        if(result == TaskStatus.FAILURE)
        {
            return result;
        }

        return TaskStatus.RUNNING;
    }

    protected override void OnExit()
    {
        count = 0;
    }

    public override void OnReset()
    {
        child.OnReset();
    }
}
