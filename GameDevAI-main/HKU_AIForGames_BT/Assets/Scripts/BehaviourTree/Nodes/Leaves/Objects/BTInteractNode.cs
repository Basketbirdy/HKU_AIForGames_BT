using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that calls the interact function on the provided Transform.gameObject
/// <br></br> !! Requires specified object to contain an IInteractable interface !!
/// </summary>
public class BTInteractNode : BTBaseNode
{
    private string interactableBBVariable;
    BlackboardType blackboardType;

    public BTInteractNode(string _interactableBBVariable, BlackboardType _blackboardType = BlackboardType.LOCAL)
    {
        interactableBBVariable = _interactableBBVariable;
        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        IInteractable interactable;
        if(blackboardType != BlackboardType.LOCAL) { interactable = GlobalBlackboard.instance.GetGlobalVariable<Transform>(interactableBBVariable, blackboardType).GetComponent<IInteractable>(); }
        else { interactable = blackboard.GetVariable<Transform>(interactableBBVariable).GetComponent<IInteractable>(); }

        if(interactable == null) { return TaskStatus.FAILURE; }

        interactable.Interact();
        return TaskStatus.SUCCESS;
    }
}
