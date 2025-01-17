using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnableGameObjectNode : BTBaseNode
{
    private string objectBBVariable;
    private bool state;

    private BlackboardType blackboardType;

    public BTEnableGameObjectNode(string _objectBBVariable, bool _state, BlackboardType _blackboardType)
    {
        objectBBVariable = _objectBBVariable;
        state = _state;
    }

    protected override TaskStatus OnUpdate()
    {
        GameObject obj;
        if(blackboardType != BlackboardType.LOCAL) { obj = GlobalBlackboard.instance.GetGlobalVariable<GameObject>(objectBBVariable, blackboardType); }
        else { obj = blackboard.GetVariable<GameObject>(objectBBVariable); }

        if (obj == null) { return TaskStatus.FAILURE; }

        obj.SetActive(state);

        return TaskStatus.SUCCESS;
    }
}
