using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite node that 'selects' one child node to execute one by one, returns success itself as soon as any child returns success 
/// </summary>
public class BTSelectorNode : BTCompositeNode
{
    private int currentIndex = 0;

    public BTSelectorNode(params BTBaseNode[] _children) : base(_children) { }

    protected override TaskStatus OnUpdate()
    {
        for(; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus result = children[currentIndex].Tick();

            switch (result)
            {
                case TaskStatus.SUCCESS: return TaskStatus.SUCCESS;
                case TaskStatus.FAILURE: continue;
                case TaskStatus.RUNNING: return TaskStatus.RUNNING;
            }
        }

        return TaskStatus.FAILURE;
    }

    protected override void OnEnter()
    {
        currentIndex = 0;
    }

    protected override void OnExit()
    {
        currentIndex = 0;
    }


    public override void OnReset()
    {
        currentIndex = 0;
        foreach (BTBaseNode child in children)
        {
            child.OnReset();
        }
    }
}
