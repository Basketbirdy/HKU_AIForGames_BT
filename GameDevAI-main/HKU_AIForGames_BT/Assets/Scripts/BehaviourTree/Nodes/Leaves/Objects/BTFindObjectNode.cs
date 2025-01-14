using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BTFindObjectNode : BTBaseNode
{
    private float range;
    private LayerMask checkMask;

    private string storageBBVariable;

    private BlackboardType blackboardType;

    public BTFindObjectNode(float _range, LayerMask _checkMask, string _storageBBVariable = "", BlackboardType _blackboardType = BlackboardType.LOCAL)
    {
        range = _range;
        checkMask = _checkMask;

        if (_storageBBVariable == "") { storageBBVariable = VariableNames.DATA_FOUNDOBJECT; }
        else { storageBBVariable = _storageBBVariable; }

        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(self.position, range, checkMask);
        //Debug.Log($"Checking for object in range, range: {storageBBVariable}");

        string storage = VariableNames.DATA_FOUNDOBJECT;
        if(storageBBVariable != "") { storage = storageBBVariable; }

        if(colliders.Length == 0) 
        {
            return TaskStatus.FAILURE; 
        }

        //Debug.Log($"Found object in range: {colliders[0].gameObject.name}");
        if (blackboardType != BlackboardType.LOCAL) { GlobalBlackboard.instance.SetGlobalVariable<Transform>(storage, colliders[0].transform, blackboardType); }
        else { blackboard.SetVariable<Transform>(storage, colliders[0].transform); }
        return TaskStatus.SUCCESS;
    }
}
