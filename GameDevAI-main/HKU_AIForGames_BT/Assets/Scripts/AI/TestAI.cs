using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestAI : MonoBehaviour
{
    BTBaseNode allyTree;

    BTBaseNode reactiveTest;

    [Header("General")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [Space]
    [SerializeField] private float interactRange;

    private NavMeshAgent agent;
    private float reachingDistance = .55f;

    [SerializeField] private Transform[] waypoints;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Blackboard blackboard = new Blackboard();

        blackboard.SetVariable<float>(VariableNames.MOVING_CURRENTSPEED, walkSpeed);
        blackboard.SetVariable<Transform[]>(VariableNames.PATROL_WAYPOINTS, waypoints);

        allyTree = 
            new BTSequenceNode(
                new BTChangeDynamicTextNode($"State: Patrolling", transform),
                new BTSetTargetToWaypointNode(VariableNames.PATROL_WAYPOINTS, VariableNames.PATROL_CURRENTWAYPOINT),
                new BTMoveToPositionNode(agent, reachingDistance, VariableNames.PATHING_TARGETTRANSFORM),
                new BTChangeDynamicTextNode($"State: Waiting", transform),
                new BTWaitNode(2f),
                new BTIncrementIndexNode<Transform>(VariableNames.PATROL_CURRENTWAYPOINT, VariableNames.PATROL_WAYPOINTS)
        );

        reactiveTest =
            new BTReactiveSequenceNode(
                new BTDebugLogNode($"I should be doing stuff"),
                new BTSequenceNode(
                    new BTDebugLogNode($"I should NOT be doing stuff"),
                    new BTWaitNode(5f),
                    new BTDebugLogNode($"After five seconds I should do stuff")
                    )
                );

        allyTree.SetupBlackboard(blackboard);
        //reactiveTest.SetupBlackboard(blackboard);
    }

    private void FixedUpdate()
    {
        TaskStatus result = allyTree.Tick();
        //TaskStatus results = reactiveTest.Tick();
    }
}
