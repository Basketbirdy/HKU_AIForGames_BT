using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSetTargetToWaypointNode : BTBaseNode
{
    private string waypointsVariableName;
    private string indexVariableName;

    public BTSetTargetToWaypointNode(string _waypointsVariableName, string _indexVariableName)
    {
        waypointsVariableName = _waypointsVariableName;
        indexVariableName = _indexVariableName;
    }

    protected override TaskStatus OnUpdate()
    {
        Transform[] waypoints = blackboard.GetVariable<Transform[]>(waypointsVariableName);
        int newValue = blackboard.GetVariable<int>(indexVariableName);

        if(waypoints == null) { return TaskStatus.FAILURE; }

        blackboard.SetVariable<Transform>(VariableNames.PATHING_TARGETTRANSFORM, waypoints[newValue]);

        return TaskStatus.SUCCESS;
    }
}
