using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite node that executes all children one by one, if any one of them fails, it returns failure itself
/// </summary>
public class BTSequenceNode : BTCompositeNode
{
    private int currentIndex = 0;

    public BTSequenceNode(params BTBaseNode[] _children) : base(_children) { }

    protected override TaskStatus OnUpdate()
    {
        for(; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus result = children[currentIndex].Tick();

            switch (result)
            {
                case TaskStatus.SUCCESS: continue;
                case TaskStatus.FAILURE: return TaskStatus.FAILURE;
                case TaskStatus.RUNNING: return TaskStatus.RUNNING;
            }
        }

        return TaskStatus.SUCCESS;
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
