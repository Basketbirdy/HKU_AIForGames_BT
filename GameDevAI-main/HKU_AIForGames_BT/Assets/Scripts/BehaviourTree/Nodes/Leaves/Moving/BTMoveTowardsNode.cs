using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveTowardsNode : BTBaseNode
{
    // dynamic
    private float speed;
    private Transform target;

    // static
    private NavMeshAgent agent;
    private float keepDistance;

    private string targetBBVariable;

    public BTMoveTowardsNode(NavMeshAgent _agent, string _targetBBVariable, float _speed, float _keepDistance)
    {
        agent = _agent;
        targetBBVariable = _targetBBVariable;
        speed = _speed;
        keepDistance = _keepDistance;
    }

    protected override void OnEnter()
    {
        agent.speed = speed;
        target = blackboard.GetVariable<Transform>(targetBBVariable);
    }

    protected override void OnExit()
    {

    }

    protected override TaskStatus OnUpdate()
    {
        if(agent == null) { return TaskStatus.FAILURE; }
        if(agent.pathPending) { return TaskStatus.RUNNING; }
        if(agent.hasPath && agent.path.status == NavMeshPathStatus.PathInvalid) { return TaskStatus.FAILURE; }
        if(agent.pathEndPosition != target.position)
        {
            agent.SetDestination(target.position);
        }

        float distance = Vector3.Distance(agent.transform.position, target.position);
        if(distance <= keepDistance) { return TaskStatus.SUCCESS; }

        return TaskStatus.RUNNING;
    }

    public override void OnReset()
    {
        agent.ResetPath();
    }
}
