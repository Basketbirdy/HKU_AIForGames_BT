using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTargetToWaypointNode : BTBaseNode
{
    private string currentWayPointBBVariable;
    private string waypointsBBVariable;
    private string indexBBVariable;

    public BTSetTargetToWaypointNode(string _currentWaypointBBVariable, string _waypointsBBVariable, string _indexBBVariable)
    {
        currentWayPointBBVariable = _currentWaypointBBVariable;
        waypointsBBVariable = _waypointsBBVariable;
        indexBBVariable = _indexBBVariable;
    }

    protected override TaskStatus OnUpdate()
    {
        Transform[] waypoints = blackboard.GetVariable<Transform[]>(waypointsBBVariable);
        int newValue = blackboard.GetVariable<int>(indexBBVariable);

        if(waypoints.Length == 0) { return TaskStatus.FAILURE; }

        blackboard.SetVariable<Transform>(currentWayPointBBVariable, waypoints[newValue]);

        return TaskStatus.SUCCESS;
    }
}
