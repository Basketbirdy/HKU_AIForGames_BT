using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IStatusHaver
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

    [Header("Status")]
    [SerializeField] private Dictionary<StatusType, float> statusTimers;

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
    [SerializeField] private Vector3 attackOffset;
    [SerializeField] private LayerMask attackMask;

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

        statusTimers = new Dictionary<StatusType, float>();
    }

    private void Start()
    {
        Blackboard bb = new Blackboard();
        // waypoints
        bb.SetVariable<Transform[]>("Waypoints", waypoints);
        bb.SetVariable<Transform>("CurrentWaypoint", waypoints[0]);
        bb.SetVariable<int>("CurrentWaypointIndex", 0);
        // weapons
        bb.SetVariable<bool>("HasWeapon", false);

        patrolTree = 
            new BTCheckTimestampNode("Player_LastSeen", detectionDuration, BlackboardType.ENEMY, TimestampCheck.ISFINISHED, 
                new BTSequenceNode(
                    new BTChangeDynamicTextNode($"State: Patrolling"),
                    new BTSetTargetToWaypointNode("CurrentWaypoint", "Waypoints", "CurrentWaypointIndex"),
                    new BTMoveTowardsNode(agent, "CurrentWaypoint", speed * .75f, keepDistance),
                    new BTIncrementIndexNode<Transform>("CurrentWaypointIndex", "Waypoints"),
                    
                    new BTChangeDynamicTextNode($"State: Waiting"),
                    new BTWaitNode(3f)
                    )
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
                                        new BTCheckTimestampNode("AttackCooldown", attackCooldown, BlackboardType.LOCAL, TimestampCheck.ISFINISHED,
                                            new BTSequenceNode(
                                                new BTDebugLogNode($"ATTACK!!"),
                                                new BTSimpleMeleeAttackNode(attackRange, 10f, attackOffset, attackMask),
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
                    new BTCheckStatusEffectNode(ref statusTimers, StatusType.BLINDNESS, false,
                        behaviourTree
                        )
                    )
                );



        mainTree.SetupSelf(transform);
        mainTree.SetupBlackboard(bb);
    }

    private void FixedUpdate()
    {
        TaskStatus result = mainTree.Tick();
    }

    public void ApplyStatusEffect(StatusType _status, float _duration)
    {
        if (statusTimers.ContainsKey(_status)) 
        {
            statusTimers[_status] = Time.time + _duration; 
            return; 
        }

        StartCoroutine(StartStatusTimer(_status, _duration));
    }

    public IEnumerator StartStatusTimer(StatusType _type, float _duration)
    {
        Debug.Log($"[{gameObject.name}] starting {_type} timer");
        statusTimers.Add(_type, Time.time + _duration);

        while (Time.time < statusTimers[_type])
        {
            Debug.Log($"[{gameObject.name}] Affected by {_type}");
            yield return null;
        }

        statusTimers.Remove(_type);
    }
}
