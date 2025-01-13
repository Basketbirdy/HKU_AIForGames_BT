using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAI : MonoBehaviour
{
    private BTBaseNode tree;

    private NavMeshAgent agent;

    [Header("Generic")]
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float keepDistance = 2f;
    [SerializeField] private float visionRange;

    [Header("Follow")]
    [SerializeField] private Transform leader;
    [SerializeField] private float followRange = 5f;

    [Header("Smokebomb action")]
    [SerializeField] private Transform throwOrigin;
    [SerializeField] GameObject smokeBombPrefab;
    [SerializeField] private LayerMask coverMask;
    [SerializeField] private SmokebombData smokeBombData;
    [SerializeField] private float postThrowCooldown;

    [Header("References")]
    [SerializeField] private GameObject worldDataManager;
    private IBlackboardHolder globalBlackboards;

    private int count = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        globalBlackboards = worldDataManager.GetComponent<IBlackboardHolder>();
    }

    void Start()
    {
        Blackboard bb = new Blackboard();
        bb.SetVariable<Transform>("FollowTarget", leader);
        bb.SetVariable<Transform>("ThrowOrigin", throwOrigin);

        tree =
            new BTSelectorNode(

                // check if leader is being attacked
                new BTBooleanConditionNode("PlayerAttacked", true, BlackboardType.ALLY,
                    new BTSequenceNode(
                        new BTChangeDynamicTextNode($"State: Looking"),
                        new BTWaitNode(1f),
                        new BTChangeDynamicTextNode($"State: Moving to cover"),
                        new BTFindObjectNode(visionRange, coverMask),
                        new BTMoveTowardsNode(agent, VariableNames.DATA_FOUNDOBJECT, speed, keepDistance),
                        new BTWaitNode(.5f),
                        new BTChangeDynamicTextNode($"State: Throwing"),
                        new BTInstantiateNode<SmokebombData>("ThrowOrigin", Vector3.zero, smokeBombPrefab, smokeBombData),
                        new BTWaitNode(.5f),
                        new BTChangeDynamicTextNode($"State: Cooldown"),
                        new BTWaitNode(postThrowCooldown)
                        )
                    ),

                new BTTargetWithinDistanceNode("FollowTarget", followRange, ConditionalCheckType.GreaterThanOrEqual,
                    new BTSequenceNode(
                        new BTChangeDynamicTextNode($"State: Following"),
                        new BTMoveTowardsNode(agent, "FollowTarget", speed, keepDistance)
                        )
                    ),

                new BTChangeDynamicTextNode($"State: Idle")

                );


        tree.SetupSelf(transform, globalBlackboards);
        tree.SetupBlackboard(bb);
    }

    void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
        count++;

        if(count >= 60)
        {
            Debug.Log($"tree result: {result.ToString()}");
            count = 0;
        }
    }
}
