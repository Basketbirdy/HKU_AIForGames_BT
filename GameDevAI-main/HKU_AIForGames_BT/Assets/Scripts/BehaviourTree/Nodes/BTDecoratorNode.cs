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

    public override void SetupSelf(Transform _self)
    {
        base.SetupSelf(_self);
        child.SetupSelf(_self);
    }

    public override void SetupBlackboard(Blackboard _blackboard)
    {
        base.SetupBlackboard(_blackboard);
        child.SetupBlackboard(_blackboard);
    }

    public override void OnReset()
    {
        base.OnReset();
        child.OnReset();
    }
}
