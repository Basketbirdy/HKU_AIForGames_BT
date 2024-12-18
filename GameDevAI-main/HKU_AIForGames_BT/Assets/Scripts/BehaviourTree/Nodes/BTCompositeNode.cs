using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// abstract composite node class, capable of having multiple child nodes
/// </summary>
public abstract class BTCompositeNode : BTBaseNode
{
    protected BTBaseNode[] children;
    public BTCompositeNode(params BTBaseNode[] _children)
    {
        children = _children;
    }

    public override void SetupBlackboard(Blackboard _blackboard)
    {
        base.SetupBlackboard(_blackboard);
        foreach(BTBaseNode node in children)
        {
            node.SetupBlackboard(_blackboard);
        }
    }
}
