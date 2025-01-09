using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTestAI : MonoBehaviour
{
    private BTBaseNode tree;

    private NavMeshAgent agent;

    [Header("Generic")]
    [SerializeField] private float speed;
    [SerializeField] private float keepDistance = 2f;

    [Header("Follow")]
    [SerializeField] private Transform leader;
    [SerializeField] private float followRange = 5f;

    [Header("References")]
    [SerializeField] private GameObject WorldDataManager;
    private IBlackboardHolder globalBlackboards;

    private int count = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        globalBlackboards = GetComponent<IBlackboardHolder>();
    }

    void Start()
    {
        Blackboard bb = new Blackboard();
        bb.SetVariable<Transform>("FollowTarget", leader);

        tree =
            new BTSelectorNode(

                // check if leader is being attacked
                    // find cover
                    // move to cover
                    // throw smokebomb at enemy attacking the player

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
