using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that waits for a specified amount of time before continueing
/// <br>(with time in seconds)</br>
/// </summary>
public class BTWaitNode : BTBaseNode
{
    private float maxWaitTime;
    private float elapsedTime;

    public BTWaitNode(float _maxWaitTime)
    {
        maxWaitTime = _maxWaitTime;
    }

    protected override void OnEnter()
    {
        elapsedTime = 0;
    }

    protected override TaskStatus OnUpdate()
    {
        if(elapsedTime < maxWaitTime) 
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime < maxWaitTime) { return TaskStatus.RUNNING; }
        }

        return TaskStatus.SUCCESS;
    }

    public override void OnReset()
    {
        elapsedTime = 0;
    }

}
