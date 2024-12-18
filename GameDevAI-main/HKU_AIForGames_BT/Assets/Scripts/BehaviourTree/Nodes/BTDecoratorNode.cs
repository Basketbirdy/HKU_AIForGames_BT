using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// abstract decorator node class, is only capable of holding a single child node
/// </summary>
public abstract class BTDecoratorNode : BTBaseNode
{
    protected BTBaseNode child;
    public BTDecoratorNode(BTBaseNode _child)
    {
        child = _child;
    }

    public override void SetupBlackboard(Blackboard _blackboard)
    {
        base.SetupBlackboard(blackboard);
        child.SetupBlackboard(blackboard);
    }
}
