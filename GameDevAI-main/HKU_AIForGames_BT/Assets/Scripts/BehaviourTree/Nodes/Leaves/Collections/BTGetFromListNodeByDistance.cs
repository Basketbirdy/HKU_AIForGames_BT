using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class BTGetFromListByDistanceNode : BTBaseNode
{
    private string listBBVariable;
    private string valueBBVariable;

    private BlackboardType blackboardType;

    public BTGetFromListByDistanceNode(string _listBBVariable, BlackboardType _blackboardType)
    {
        listBBVariable = _listBBVariable;

        blackboardType = _blackboardType;
    }

    protected override TaskStatus OnUpdate()
    {
        List<Transform> list;

        if (blackboardType != BlackboardType.LOCAL) { 
            list = GlobalBlackboard.instance.GetGlobalVariable<List<Transform>>(listBBVariable, blackboardType); 
        }
        else 
        { 
            list = blackboard.GetVariable<List<Transform>>(listBBVariable);
        }

        int closestIndex = 0;
        float closestDistance = 0;
        for (int i = 0; i < list.Count; i++)
        {
            float distance = Vector3.Distance(list[i].position, self.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
        }

        if (blackboardType != BlackboardType.LOCAL)
        {
            GlobalBlackboard.instance.SetGlobalVariable<Transform>(valueBBVariable, list[closestIndex] , blackboardType);
        }
        else
        {
            blackboard.SetVariable<Transform>(listBBVariable, list[closestIndex]);
        }

        return TaskStatus.SUCCESS;
    }
}
