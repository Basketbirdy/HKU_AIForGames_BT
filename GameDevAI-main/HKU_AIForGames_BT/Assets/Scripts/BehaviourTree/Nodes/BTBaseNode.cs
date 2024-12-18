using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskStatus { SUCCESS, FAILURE, RUNNING }

/// <summary>
/// abstract base node class, includes all standard Behaviour tree functionality for each node
/// </summary>
public abstract class BTBaseNode
{
    protected Blackboard blackboard;        // stores all data for the behaviour tree
    private bool wasEntered = false;        // stores state of the node

    public virtual void OnReset()           // called when tree is interrupted
    {

    }

    public TaskStatus Tick()                // main function in which protected function are handled
    {
        if(!wasEntered)                     // if node has not been entered since last result, execute OnEnter function and set wasEntered to true
        {
            OnEnter();
            wasEntered = true;
        }

        TaskStatus result = OnUpdate();

        if(result != TaskStatus.RUNNING)    // if the node is no longer running, execute all exit code
        {
            OnExit();
            wasEntered = false;
        }
        return result;
    }

    protected abstract TaskStatus OnUpdate();

    /// <summary>
    /// <para>function called when a node is entered for the first time, since it last finished running</para>
    /// override to implement enter functionality
    /// </summary>
    protected virtual void OnEnter() 
    {
    
    }

    /// <summary>
    /// <para>function called when a node has finished running</para>
    /// override to implement exit functionality
    /// </summary>
    protected virtual void OnExit()
    {

    }

    public virtual void SetupBlackboard(Blackboard blackboard)
    {
        this.blackboard = blackboard;
    }
}
