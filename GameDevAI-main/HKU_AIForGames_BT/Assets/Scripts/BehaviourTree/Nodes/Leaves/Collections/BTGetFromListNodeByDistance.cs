using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that gets an object from a list based on distance
/// <br></br> stores object located closest to self
/// <br></br> Can only be used on lists of type: Transform
/// </summary>
public class BTGetFromListByDistanceNode : BTBaseNode
{
    private string listBBVariable;
    private string valueBBVariable;

    private BlackboardType listBlackboardType;
    private BlackboardType valueBlackboardType;

    public BTGetFromListByDistanceNode(string _listBBVariable, string _valueBBVariable, BlackboardType _listBlackboardType, BlackboardType _valueBlackboardType)
    {
        listBBVariable = _listBBVariable;
        valueBBVariable = _valueBBVariable;

        listBlackboardType = _listBlackboardType;
        valueBlackboardType = _valueBlackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        List<Transform> list;

        if (listBlackboardType != BlackboardType.LOCAL) { 
            list = GlobalBlackboard.instance.GetGlobalVariable<List<Transform>>(listBBVariable, listBlackboardType); 
        }
        else 
        { 
            list = blackboard.GetVariable<List<Transform>>(listBBVariable);
        }

        if(list == null || list.Count == 0) 
        {   
            OnReset();
            return TaskStatus.FAILURE; 
        }

        int closestIndex = 0;
        float closestDistance = 9999;
        for (int i = 0; i < list.Count; i++)
        {
            float distance = Vector3.Distance(list[i].position, self.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        if (valueBlackboardType != BlackboardType.LOCAL)
        {
            GlobalBlackboard.instance.SetGlobalVariable<Transform>(valueBBVariable, list[closestIndex] , valueBlackboardType);
        }
        else
        {
            blackboard.SetVariable<Transform>(valueBBVariable, list[closestIndex]);
        }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {
        if (valueBlackboardType != BlackboardType.LOCAL)
        {
            GlobalBlackboard.instance.SetGlobalVariable<Transform>(valueBBVariable, null, valueBlackboardType);
        }
        else
        {
            blackboard.SetVariable<Transform>(valueBBVariable, null);
        }
    }
}
