using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Decorator node that always returns success regardless of the child nodes return value 
/// <br>Success -> Success</br> 
/// <br>Failure -> Success</br>
/// <br>Running -> Success</br>
/// </summary>
public class BTSucceederNode : BTDecoratorNode
{
    public BTSucceederNode(BTBaseNode _child) : base(_child) { }

    protected override TaskStatus OnUpdate()
    {
        TaskStatus result = child.Tick();

        switch (result)
        {
            case TaskStatus.SUCCESS: return TaskStatus.SUCCESS;
            case TaskStatus.FAILURE: return TaskStatus.SUCCESS;
            case TaskStatus.RUNNING: return TaskStatus.SUCCESS;     // TODO - does running also succeed
        }

        return result;
    }

    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {

    }

    public override void OnReset()
    {
        child.OnReset();
    }
}
