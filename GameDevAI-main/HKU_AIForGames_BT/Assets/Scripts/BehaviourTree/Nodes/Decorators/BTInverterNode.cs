using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

/// <summary>
/// Decorator node that inverses the returned status of its child node, 
/// <br>Success -> Failure</br> 
/// <br>Failure -> Success</br>
/// <br>Running -> Running</br>
/// </summary>
public class BTInverterNode : BTDecoratorNode
{
    public BTInverterNode(BTBaseNode _child) : base(_child) { }

    protected override TaskStatus OnUpdate()
    {
        TaskStatus result = child.Tick();

        switch(result)
        {
            case TaskStatus.SUCCESS: return TaskStatus.FAILURE;
            case TaskStatus.FAILURE: return TaskStatus.SUCCESS;
            case TaskStatus.RUNNING: return TaskStatus.RUNNING;
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
