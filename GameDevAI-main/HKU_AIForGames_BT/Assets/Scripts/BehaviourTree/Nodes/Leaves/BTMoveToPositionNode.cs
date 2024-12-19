using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToPositionNode : BTBaseNode
{
    private NavMeshAgent agent;
    private float reachingDistance;
    private Vector3 targetPosition;

    public BTMoveToPositionNode(NavMeshAgent _agent, float _reachingDistance)
    {
        agent = _agent;
        reachingDistance = _reachingDistance;
    }

    protected override TaskStatus OnUpdate()
    {
        // get current speed
        agent.speed = blackboard.GetVariable<float>(VariableNames.MOVING_CURRENTSPEED);

        // status checks
        if(agent == null) { return TaskStatus.FAILURE; }                                    
        if (agent.pathPending) { return TaskStatus.RUNNING; }
        if(agent.hasPath && agent.path.status == NavMeshPathStatus.PathInvalid) {  return TaskStatus.FAILURE; }

        if(agent.pathEndPosition != targetPosition)
        {
            agent.SetDestination(targetPosition);
        }

        float distance = Vector3.Distance(targetPosition, agent.transform.position);
        if(distance <= reachingDistance) { Debug.Log("Am i returning success?"); return TaskStatus.SUCCESS; }
        return TaskStatus.RUNNING;
    }

    protected override void OnEnter()
    {
        agent.stoppingDistance = reachingDistance;
        targetPosition = blackboard.GetVariable<Vector3>(VariableNames.PATHING_TARGETPOSITION);
    }
}
