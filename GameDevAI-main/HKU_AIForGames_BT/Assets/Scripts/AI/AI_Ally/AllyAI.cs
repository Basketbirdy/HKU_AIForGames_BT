using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AllyAI : MonoBehaviour
{
    // trees
    BTBaseNode allyTree;

    [Header("General")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintMultiplier;
    [Space]
    [SerializeField] private float interactRange;
    [SerializeField] private float visionRange;

    [Header("Follow")]
    [SerializeField] private GameObject leader;
    [SerializeField] private float followRange;

    [Header("Smokebomb action")]
    [SerializeField] private LayerMask coverMask;
    
    private NavMeshAgent agent;
    private float reachingDistance = .55f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        Blackboard blackboard = new Blackboard();

        allyTree =
            new BTSelectorNode(
                new BTBooleanConditionNode(VariableNames.DATA_GETTINGATTACKED, true,
                    new BTSequenceNode(
                        new BTFindObjectNode(transform, visionRange, coverMask),
                        new BTTargetWithinDistanceNode(VariableNames.DATA_FOUNDOBJECT, interactRange, ConditionalCheckType.GreaterThanOrEqual,        //change second transform into the transform of the cover
                            new BTMoveToPositionNode(agent, reachingDistance, VariableNames.DATA_FOUNDOBJECT)
                            ),
                        // TODO - Throw Smokebomb at enemy that is attacking
                        new BTDebugLogNode($"Throwing smokebomb at enemy")
                            )
                    ),
                // TODO - check if player is being attacked
                new BTTargetWithinDistanceNode(VariableNames.PATHING_TARGETTRANSFORM, followRange, ConditionalCheckType.GreaterThanOrEqual,              // change targettransform string into string for follow target transform
                    new BTMoveToPositionNode(agent, reachingDistance, "Leader")
                    ),
                // TODO - Idle behavior
                new BTChangeDynamicTextNode($"State: Idle")
                );

        allyTree.SetupBlackboard(blackboard);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        TaskStatus result = allyTree.Tick();
    }
}
