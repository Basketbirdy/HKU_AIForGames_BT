using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToPositionNode : BTBaseNode
{
    private NavMeshAgent agent;
    private float reachingDistance;

    private Vector3 targetPosition;
    private string targetPositionVariable;

    public BTMoveToPositionNode(NavMeshAgent _agent, float _reachingDistance, string _targetPositionVariable)
    {
        agent = _agent;
        reachingDistance = _reachingDistance;
        targetPositionVariable = _targetPositionVariable;
    }

    protected override void OnEnter()
    {
        agent.stoppingDistance = reachingDistance;
        targetPosition = blackboard.GetVariable<Transform>(targetPositionVariable).position;
    }

    protected override void OnExit()
    {
        blackboard.SetVariable<Transform>(VariableNames.PATHING_TARGETTRANSFORM, default);
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

        float distance = Vector3.Distance(agent.transform.position, targetPosition);
        if(distance <= reachingDistance) 
        { 
            return TaskStatus.SUCCESS; 
        }

        return TaskStatus.RUNNING;
    }

    public override void OnReset()
    {
        agent.isStopped = true;
    }
}
