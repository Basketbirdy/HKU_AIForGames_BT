using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //private BTBaseNode oldTree;
    private BTBaseNode mainTree;
    private BTBaseNode behaviourTree;
    private BTBaseNode patrolTree;

    private NavMeshAgent agent;

    [Header("Generic")]
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float keepDistance;

    [Header("Target detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private float detectionDuration;

    [Header("Weapon detection")]
    [SerializeField] private float pickupDuration;
    [SerializeField] private LayerMask weaponMask;

    [Header("Attack")]
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;

    [Header("Patrol")]
    [SerializeField] private float pauseDuration;
    [SerializeField] private Transform[] waypoints;

    [Header("References")]
    [SerializeField] private GameObject worldDataManager;
    private IBlackboardHolder globalBlackboards;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        globalBlackboards = worldDataManager.GetComponent<IBlackboardHolder>();
    }

    private void Start()
    {
        Blackboard bb = new Blackboard();
        // waypoints
        bb.SetVariable<Transform[]>("Waypoints", waypoints);
        bb.SetVariable<int>("CurrentWaypointIndex", 0);
        // weapons
        bb.SetVariable<bool>("HasWeapon", false);

        patrolTree =
            new BTSequenceNode(
                new BTChangeDynamicTextNode($"State: Patrolling")
                
                );

        behaviourTree =
            new BTSelectorNode(
                new BTCheckTimestampNode("Player_LastSeen", detectionDuration, BlackboardType.ENEMY, TimestampCheck.ISRUNNING,
                    new BTSelectorNode(
                        new BTSequenceNode(
                            // check for weapon
                            new BTBooleanConditionNode("HasWeapon", true, BlackboardType.LOCAL,
                                // if this enemy has a weapon
                                new BTSequenceNode(
                                    new BTChangeDynamicTextNode($"Chasing target"),
                                    new BTParallelNode(
                                        new BTCheckTimestampNode("AttackCooldown", attackCooldown, BlackboardType.LOCAL, TimestampCheck.ISFINSIHED,
                                            new BTSequenceNode(
                                                new BTDebugLogNode($"ATTACK!!"),
                                                new BTSetTimestampNode("AttackCooldown", BlackboardType.LOCAL)
                                                )
                                            ),
                                        new BTMoveTowardsNode(agent, "Player_LastSeenPosition", speed, attackRange, BlackboardType.ENEMY)
                                        )
                                    )
                                )
                            ),
                        new BTSequenceNode(
                            // find weapon
                            new BTChangeDynamicTextNode($"Finding weapon"),
                            new BTGetFromListByDistanceNode("Weapon_LocatedList", "Weapon_Target", BlackboardType.ENEMY, BlackboardType.LOCAL),
                            new BTMoveTowardsNode(agent, "Weapon_Target", speed, keepDistance),
                            new BTDebugLogNode($"Picking up weapon"),
                            new BTSetBlackboardVariableNode<bool>("HasWeapon", true),
                            new BTInteractNode("Weapon_Target"),
                            new BTRemoveFromListNode<Transform>("Weapon_LocatedList", "Weapon_Target", BlackboardType.ENEMY, BlackboardType.LOCAL)
                            )
                        )
                    ),
                patrolTree
                );
                


        mainTree =
            new BTParallelNode(

                // player in range check
                new BTSequenceNode(
                    new BTFindObjectNode(detectionRange, detectionMask, "Player_LastSeenPosition", BlackboardType.ENEMY),
                    new BTSetTimestampNode("Player_LastSeen", BlackboardType.ENEMY)
                    ),

                new BTSequenceNode(
                    new BTFindObjectNode(detectionRange, weaponMask),
                    new BTAddToListNode<Transform>("Weapon_LocatedList", VariableNames.DATA_FOUNDOBJECT, BlackboardType.ENEMY, BlackboardType.LOCAL)
                    ),

                new BTSequenceNode(
                    //new BTDebugLogNode($"[EnemyAI; {gameObject.name}] Running main behaviour inside parallel node"),
                    behaviourTree
                    )
                );



        mainTree.SetupSelf(transform);
        mainTree.SetupBlackboard(bb);
    }

    private void FixedUpdate()
    {
        TaskStatus result = mainTree.Tick();
    }

    //oldTree =
    //        new BTSelectorNode(

    //            new BTTimerConditionNode("PlayerSpottedTimer", detectionDuration, BlackboardType.ENEMY,
    //                // Do this if timer is running - player is spotted

    //                new BTSequenceNode(
    //                    new BTSelectorNode(
    //                        // check if blackboard has weapon
    //                        new BTBooleanConditionNode("HasWeapon", true, BlackboardType.GLOBAL,
    //                            new BTSequenceNode(
    //                                new BTSelectorNode(

    //                                    // TODO - Parallel node
    //                                    new BTSequenceNode(
    //                                        new BTFindObjectNode(attackRange, detectionMask),
    //                                        new BTChangeDynamicTextNode($"State: Attacking player")
    //                                        ),

    //                                    new BTSequenceNode(
    //                                        new BTChangeDynamicTextNode($"State: Chasing player")
    //                                        )

    //                                    )

    //                                )

    //                            ),

    //                        // TODO - weapon check stuff
    //                        new BTSequenceNode(
    //                            new BTChangeDynamicTextNode($"State: Looking for weapon"),
    //                            new BTFindObjectNode(detectionRange, weaponMask),
    //                            new BTChangeDynamicTextNode($"State: Moving to weapon"),
    //                            new BTMoveTowardsNode(agent, VariableNames.DATA_FOUNDOBJECT, speed, keepDistance),
    //                            new BTChangeDynamicTextNode($"State: Picking up weapon"),
    //                            new BTWaitNode(pickupDuration),
    //                            new BTSetBlackboardVariableNode<bool>("HasWeapon", true)
    //                            )

    //                        )

    //                    )

    //                ),

    //            new BTSequenceNode(

    //                new BTFindObjectNode(detectionRange, detectionMask),
    //                new BTSetBlackboardVariableNode<float>("PlayerSpottedTimer", 0f, BlackboardType.ENEMY)

    //                ),

    //            new BTChangeDynamicTextNode($"State: Patrolling")

    //            );

    //    oldTree.SetupSelf(transform);
    //    oldTree.SetupBlackboard(bb);
}
