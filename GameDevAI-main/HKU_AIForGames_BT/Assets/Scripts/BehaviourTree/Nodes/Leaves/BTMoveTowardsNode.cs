using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BTMoveTowardsNode : BTBaseNode
{
    private float speed;
    private float reachDistance;
    private Vector3 target;
    private Transform transform;

    public BTMoveTowardsNode(Vector3 _target, float _reachDistance, Transform _transform)
    {
        transform = _transform;
        target = _target;
        reachDistance = _reachDistance;
    }

    protected override void OnEnter()
    {
        speed = blackboard.GetVariable<float>(VariableNames.MOVING_CURRENTSPEED);
    }

    protected override TaskStatus OnUpdate()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        float distanceToTarget = Vector3.Distance(transform.position, target);
        if(distanceToTarget < reachDistance) { return TaskStatus.SUCCESS; }

        return TaskStatus.RUNNING;
    }
}
