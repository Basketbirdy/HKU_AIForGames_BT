using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite node that re-checks every child node, regardles of wether or not one returned running
/// </summary>
public class BTReactiveSequenceNode : BTCompositeNode
{
    public BTReactiveSequenceNode(params BTBaseNode[] _children) : base(_children) { }

    protected override TaskStatus OnUpdate()
    {
        for (int i = 0; i < children.Length; i++)
        {
            TaskStatus result = children[i].Tick();

            if(result != TaskStatus.SUCCESS)
            {
                return result;
            }

            //switch (result)
            //{
            //    case TaskStatus.SUCCESS: return result;
            //    case TaskStatus.FAILURE: return result;
            //    case TaskStatus.RUNNING: return result;
            //}
        }

        return TaskStatus.SUCCESS;
    }

    protected override void OnEnter()
    {
    }

    protected override void OnExit()
    {
        OnReset();
    }

    public override void OnReset()
    {
        foreach (BTBaseNode child in children)
        {
            child.OnReset();
        }
    }
}
