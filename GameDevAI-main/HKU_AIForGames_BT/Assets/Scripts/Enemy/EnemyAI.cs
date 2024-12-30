using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    BTBaseNode testingTree;

    BTBaseNode patrolTree;
    BTBaseNode patrolPointTree;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    private float reachingDistance = .55f;

    private NavMeshAgent agent;
    [SerializeField] private Transform[] waypoints;

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
        blackboard.SetVariable<Transform[]>(VariableNames.PATHING_WAYPOINTS, waypoints);

        // tree setup
        patrolTree = new BTSequenceNode(
            new BTSetTargetToWaypointNode(VariableNames.PATHING_WAYPOINTS, VariableNames.PATHING_CURRENTWAYPOINT),
            new BTMoveToPositionNode(agent, reachingDistance),
            // TODO - Make enemy look around for 2 seconds, instead of waiting 2 seconds
            new BTWaitNode(2f),
            new BTIncrementIndexNode<Transform>(VariableNames.PATHING_CURRENTWAYPOINT, VariableNames.PATHING_WAYPOINTS)
        );

        patrolTree.SetupBlackboard(blackboard);
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        TaskStatus result = patrolTree.Tick();
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
