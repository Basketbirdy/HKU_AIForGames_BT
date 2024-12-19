using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    BTBaseNode tree;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    private float reachingDistance = .55f;

    private NavMeshAgent agent;
    [SerializeField] private Transform patrolWaypoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Blackboard blackboard = new Blackboard();
        blackboard.SetVariable<string>(VariableNames.OBJECT_NAME, gameObject.name);

        blackboard.SetVariable<float>(VariableNames.MOVING_CURRENTSPEED, walkSpeed);

        // get waypoints
        Transform[] patrolPoints = patrolWaypoints.GetComponentsInChildren<Transform>();
        blackboard.SetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS, patrolPoints);

        tree = new BTSequenceNode(
            new BTDebugLogNode($"----------------------"),                                 // should appear 
            new BTDebugLogNode($"Hello, the tree works?"),                                 // should appear 
            new BTDebugLogNode($"How is it going?", LogType.WARNING),                      // should appear 
            new BTSelectorNode(
                    new BTInverterNode(
                            new BTDebugLogNode($"first, I think I should be here")         // should appear 
                        ),
                    new BTDebugLogNode($"second, I think I should be here"),               // should appear 
                    new BTDebugLogNode($"Third, I am not supposed to be here")
                ),
            new BTWaitNode(2f),

            new BTSetTargetPositionNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[0].position),
            new BTMoveToPositionNode(agent, reachingDistance),
            new BTWaitNode(2f),
            new BTSetTargetPositionNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[1].position),
            new BTMoveToPositionNode(agent, reachingDistance),
            new BTWaitNode(2f),
            new BTSetTargetPositionNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[2].position),
            new BTMoveToPositionNode(agent, reachingDistance),
            new BTWaitNode(2f),
            new BTSetTargetPositionNode(blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[3].position),
            new BTMoveToPositionNode(agent, reachingDistance),
            new BTWaitNode(2f),
            new BTDebugLogNode($"----------------------")
            );

        tree.SetupBlackboard(blackboard);
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
    }
}
