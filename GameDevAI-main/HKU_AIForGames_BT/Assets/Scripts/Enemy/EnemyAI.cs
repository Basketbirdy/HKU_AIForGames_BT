using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // main behaviour tree
    BTBaseNode enemyTree;

    // modular behaviour tree pieces
    BTBaseNode playerCheckTree;
    BTBaseNode weaponFindTree;
    BTBaseNode patrolTree;

    [Header("General")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [Space]
    [SerializeField] private float interactRange;

    private NavMeshAgent agent;
    private float reachingDistance = .55f;

    [Header("Patrol")]
    [SerializeField] private Transform[] waypoints;

    [Header("Player detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform player;

    [Header("Attacks")]
    [SerializeField] private float attackRange;

    [Header("Weapons")]
    [SerializeField] private LayerMask weaponMask;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Blackboard blackboard = new Blackboard();
        // characteristics & stats
        blackboard.SetVariable<string>(VariableNames.OBJECT_NAME, gameObject.name);
        blackboard.SetVariable<float>(VariableNames.MOVING_CURRENTSPEED, walkSpeed);
        // waypoints
        blackboard.SetVariable<Transform[]>(VariableNames.PATROL_WAYPOINTS, waypoints);
        // detection
        blackboard.SetVariable<float>(VariableNames.CHECK_CURRENTRANGE, detectionRange);
        // data
        blackboard.SetVariable<bool>(VariableNames.DATA_PLAYERSPOTTED, false);


        // DEBUG
        blackboard.SetVariable<bool>(VariableNames.DATA_HASWEAPON, true);

        // tree setup
        patrolTree = 
            new BTSequenceNode(
                new BTChangeDynamicTextNode($"State: Patrolling", transform),
                new BTSetTargetToWaypointNode(VariableNames.PATROL_WAYPOINTS, VariableNames.PATROL_CURRENTWAYPOINT),
                new BTMoveToPositionNode(agent, reachingDistance),
                new BTWaitNode(2f),
                new BTIncrementIndexNode<Transform>(VariableNames.PATROL_CURRENTWAYPOINT, VariableNames.PATROL_WAYPOINTS)
        );

        playerCheckTree = 
            new BTSelectorNode(
                new BTSequenceNode(
                    new BTChangeDynamicTextNode($"State: PlayerCheck", transform),
                    new BTSelectorNode(
                        new BTSequenceNode(
                            new BTCheckObjectInRangeNode(detectionRange, transform, playerMask),
                            // TODO - Shoot raycast to player pos to check for walls
                            // TODO - Reset player spotted timer
                            // TODO - set player spotted to true in blackboard
                            new BTSetBlackboardVariableNode<bool>(VariableNames.DATA_PLAYERSPOTTED, true)
                        ),
                        new BTSequenceNode(
                            // TODO - check if the timer is finished
                            // TODO - set player spotted to false
                            new BTSetBlackboardVariableNode<bool>(VariableNames.DATA_PLAYERSPOTTED, false)
                        )
                    )
                )
            );

        weaponFindTree = 
            new BTSelectorNode(
                new BTSequenceNode(
                    new BTDebugLogNode($"Finding weapon"),
                    new BTChangeDynamicTextNode($"State: FindWeapon", transform),
                    // TODO - Find a weapon
                    new BTSequenceNode(
                        new BTCheckObjectInRangeNode(detectionRange, transform, weaponMask),
                        new BTWaitNode(2f),                                                             // search for 2 seconds
                        new BTSelectorNode(
                            new BTSequenceNode(
                                new BTCheckObjectInRangeNode(interactRange, transform, weaponMask),
                                new BTInverterNode(
                                    new BTWaitNode(2f)
                                )                                                        
                            )
                            // TODO - Approach the weapon
                        )
                        // TODO - Pick weapon up + despawn/disable weapon object
                    )
                    // TODO - run away behaviour (fleeing)
                )
            );

        enemyTree = 
            new BTReactiveSequenceNode(
                //TODO - Update timer node
                new BTDebugLogNode($"Updating timers"),
                new BTSelectorNode(
                    new BTInverterNode(
                        new BTSucceederNode(
                            playerCheckTree
                            )
                        ),
                new BTSelectorNode(
                    new BTSequenceNode(
                        new BTCheckBlackboardVariableNode<bool>(VariableNames.DATA_PLAYERSPOTTED, true),
                        new BTSelectorNode(
                            new BTCheckBlackboardVariableNode<bool>(VariableNames.DATA_HASWEAPON, true),
                            weaponFindTree
                                ),
                        new BTSelectorNode(
                            new BTSequenceNode(
                                new BTCheckObjectInRangeNode(attackRange, transform, playerMask),
                                 //TODO - Attack player
                                new BTDebugLogNode($"Attack time")
                                ),
                             //TODO - set enemy target to player and move towards it
                            new BTDebugLogNode($"Move/chase to player")
                            )
                        ),
                    patrolTree
                    )
                )
            );

        //enemyTree = new BTReactiveSequenceNode(
        //        new BTSelectorNode(
        //            new BTSequenceNode(
        //                    new BTChangeDynamicTextNode("State: Checking vicinity", transform),
        //                    new BTCheckObjectInRangeNode(detectionRange, transform, playerMask),                                            // check for player
        //                    new BTSetBlackboardVariableNode<Transform>(VariableNames.PATHING_TARGETTRANSFORM, player),      // TODO - ask about a better way to get player transform
        //                    new BTMoveToPositionNode(agent, reachingDistance)
        //                ),
        //            patrolTree
        //            )
        //    );

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
