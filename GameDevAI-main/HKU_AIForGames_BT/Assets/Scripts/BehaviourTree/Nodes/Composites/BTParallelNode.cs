using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTParallelNode : BTCompositeNode
{
    private int currentIndex;

    public BTParallelNode(BTBaseNode[] _children) : base(_children) { }

    protected override void OnEnter()
    {
        currentIndex = 0;
    }

    protected override void OnExit()
    {
        OnReset();
    }

    protected override TaskStatus OnUpdate()
    {
        for(; currentIndex < children.Length; currentIndex++)
        {
            TaskStatus result = children[currentIndex].Tick();

            if(result == TaskStatus.FAILURE) { return TaskStatus.FAILURE; }
            else { continue; }
        }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {
        currentIndex = 0;

        base.OnReset();
        foreach(BTBaseNode node in children)
        {
            node.OnReset();
        }
    }
}
