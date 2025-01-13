using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Composite node that runs all children
/// <br></br> returns result of specified priority node
/// </summary>
public class BTParallelNode : BTCompositeNode
{
    private int currentIndex;
    private int priorityIndex;

    public BTParallelNode(int _priorityIndex, BTBaseNode[] _children) : base(_children) 
    {
        priorityIndex = _priorityIndex;
        if(priorityIndex == 0) { priorityIndex = children.Length - 1; }
    }

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

            if(currentIndex != priorityIndex) { continue; }

            return result;
        }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {
        currentIndex = 0;

        foreach(BTBaseNode node in children)
        {
            node.OnReset();
        }
    }
}
