using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    BTBaseNode tree;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;

    [SerializeField] private Transform patrolWaypoints;

    private void Awake()
    {
        Blackboard blackboard = new Blackboard();
        blackboard.SetVariable<string>(VariableNames.OBJECT_NAME, gameObject.name);

        blackboard.SetVariable<float>(VariableNames.MOVING_CURRENTSPEED, walkSpeed);

        // get waypoints
        Transform[] patrolPoints = patrolWaypoints.GetComponentsInChildren<Transform>();
        blackboard.SetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS, patrolPoints);

        tree = new BTSequenceNode(
            new BTMessageLoggerNode($"----------------------"),                                 // should appear 
            new BTMessageLoggerNode($"Hello, the tree works?"),                                 // should appear 
            new BTWaitNode(2f),
            new BTMessageLoggerNode($"How is it going?", LogType.WARNING),                      // should appear 
            new BTWaitNode(2f),
            new BTSelectorNode(
                    new BTInverterNode(
                            new BTMessageLoggerNode($"first, I think I should be here")         // should appear 
                        ),
                    new BTMessageLoggerNode($"second, I think I should be here"),               // should appear 
                    new BTMessageLoggerNode($"Third, I am not supposed to be here")
                ),
            new BTWaitNode(2f),
            new BTSequenceNode(
                    new BTMessageLoggerNode($"{blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[0]}"),              // should appear 
                    new BTMessageLoggerNode($"{blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[1]}"),              // should appear 
                    //new BTInverterNode(
                    //        new BTMessageLoggerNode($"{blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[2]}")       // should appear 
                    //    ),
                    new BTMessageLoggerNode($"{blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[3]}")
                ),
            new BTMoveTowardsNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[0].position, .5f, transform),
            new BTMoveTowardsNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[1].position, .5f, transform),
            new BTMoveTowardsNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[2].position, .5f, transform),
            new BTMoveTowardsNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[3].position, .5f, transform),
            new BTMessageLoggerNode($"----------------------")
            );

        tree.SetupBlackboard(blackboard);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
    }
}
