using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    BTBaseNode patrolTree;
    BTBaseNode enemyTree;


    [Header("General")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;

    private NavMeshAgent agent;
    private float reachingDistance = .55f;

    [Header("Patrol")]
    [SerializeField] private Transform[] waypoints;

    [Header("Player detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform player;

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
        blackboard.SetVariable<Transform[]>(VariableNames.PATROL_WAYPOINTS, waypoints);

        blackboard.SetVariable<float>(VariableNames.CHECK_CURRENTRANGE, detectionRange);

        // tree setup
        patrolTree = new BTSequenceNode(
            new BTSetTargetToWaypointNode(VariableNames.PATROL_WAYPOINTS, VariableNames.PATROL_CURRENTWAYPOINT),
            new BTMoveToPositionNode(agent, reachingDistance),
            // TODO - Make enemy look around for 2 seconds, instead of waiting 2 seconds
            new BTWaitNode(2f),
            new BTIncrementIndexNode<Transform>(VariableNames.PATROL_CURRENTWAYPOINT, VariableNames.PATROL_WAYPOINTS)
        );

        enemyTree = new BTSequenceNode(
                new BTSelectorNode(
                    new BTSequenceNode(
                            new BTCheckObjectInRangeNode(transform, playerMask),                                        // check for player
                            new BTSetBlackboardVariableNode<Transform>(VariableNames.PATHING_TARGETTRANSFORM, player)   // TODO - ask about a better way to get player transform
                        ),
                    patrolTree           
                    )
            );

        enemyTree.SetupBlackboard(blackboard);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        TaskStatus result = enemyTree.Tick();
    }

        //testingTree = new BTSequenceNode(
        //    new BTDebugLogNode($"----------------------"),                                 // should appear 
        //    new BTDebugLogNode($"Hello, the tree works?"),                                 // should appear 
        //    new BTDebugLogNode($"How is it going?", LogType.WARNING),                      // should appear 
        //    new BTSelectorNode(
        //            new BTInverterNode(
        //                    new BTDebugLogNode($"first, I think I should be here")         // should appear 
        //                ),
        //            new BTDebugLogNode($"second, I think I should be here"),               // should appear 
        //            new BTDebugLogNode($"Third, I am not supposed to be here")
        //        ),
        //    new BTWaitNode(2f),

        //    new BTSetBlackboardVariableNode<Vector3>(VariableNames.PATHING_TARGETPOSITION, blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[blackboard.GetVariable<int>(VariableNames.PATHING_CURRENTWAYPOINT)].position),
        //    new BTMoveToPositionNode(agent, reachingDistance),
        //    new BTWaitNode(2f),
        //    new BTSetBlackboardVariableNode<Vector3>(VariableNames.PATHING_TARGETPOSITION, blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[blackboard.GetVariable<int>(VariableNames.PATHING_CURRENTWAYPOINT)].position),
        //    new BTMoveToPositionNode(agent, reachingDistance),
        //    new BTWaitNode(2f),
        //    new BTSetBlackboardVariableNode<Vector3>(VariableNames.PATHING_TARGETPOSITION, blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[blackboard.GetVariable<int>(VariableNames.PATHING_CURRENTWAYPOINT)].position),
        //    new BTMoveToPositionNode(agent, reachingDistance),
        //    new BTWaitNode(2f),
        //    new BTSetBlackboardVariableNode<Vector3>(VariableNames.PATHING_TARGETPOSITION, blackboard.GetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS)[blackboard.GetVariable<int>(VariableNames.PATHING_CURRENTWAYPOINT)].position),
        //    new BTMoveToPositionNode(agent, reachingDistance),
        //    new BTWaitNode(2f),
        //    new BTDebugLogNode($"----------------------")
        //    );
}
