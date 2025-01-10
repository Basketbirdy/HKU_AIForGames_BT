using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private BTBaseNode tree;

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

        tree =
            new BTSelectorNode(

                new BTTimerNode("PlayerSpottedTimer", detectionDuration, true, GlobalBlackboardType.ENEMY,
                    // Do this if timer is running - player is spotted

                    new BTSequenceNode(

                        new BTSelectorNode(

                            // check if blackboard has weapon
                            new BTBooleanConditionNode("HasWeapon", true, false, GlobalBlackboardType.GLOBAL,

                                new BTSequenceNode(

                                    new BTSelectorNode(
                                        // TODO - Parallel node
                                        
                                        new BTSequenceNode(

                                            new BTFindObjectNode(attackRange, detectionMask),
                                            new BTChangeDynamicTextNode($"State: Attacking player")

                                            ),

                                        new BTSequenceNode(

                                            new BTChangeDynamicTextNode($"State: Chasing player")

                                            )

                                        )

                                    )

                                ),

                            // TODO - weapon check stuff
                            new BTSequenceNode(

                                new BTChangeDynamicTextNode($"State: Looking for weapon"),
                                new BTFindObjectNode(detectionRange, weaponMask),
                                new BTChangeDynamicTextNode($"State: Moving to weapon"),
                                new BTMoveTowardsNode(agent, VariableNames.DATA_FOUNDOBJECT, speed, keepDistance),
                                new BTChangeDynamicTextNode($"State: Picking up weapon"),
                                new BTWaitNode(pickupDuration),
                                new BTSetBlackboardVariableNode<bool>("HasWeapon", true, false)

                                )

                            )

                        )

                    ),

                new BTSequenceNode(

                    new BTFindObjectNode(detectionRange, detectionMask),
                    new BTSetBlackboardVariableNode<float>("PlayerSpottedTimer", 0f, true, GlobalBlackboardType.ENEMY)

                    ),

                new BTChangeDynamicTextNode($"State: Patrolling")

                );

        tree.SetupSelf(transform, globalBlackboards);
        tree.SetupBlackboard(bb);
    }

    private void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
    }
}
