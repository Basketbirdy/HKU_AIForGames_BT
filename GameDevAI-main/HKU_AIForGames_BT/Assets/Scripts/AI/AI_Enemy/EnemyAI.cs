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

    [Header("Target detection")]
    [SerializeField] private float detectionRange;
    [SerializeField] private LayerMask detectionMask;
    [SerializeField] private float detectionDuration;

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

        tree =
            new BTSelectorNode(

                new BTTimerNode("PlayerSpottedTimer", detectionDuration, true, GlobalBlackboardType.ENEMY,                          
                    // Do this if timer is running - player is spotted

                    new BTSequenceNode(

                        new BTSequenceNode(

                            new BTFindObjectNode(detectionRange, detectionMask),
                            new BTSetBlackboardVariableNode<float>("PlayerSpottedTimer", 0f, true, GlobalBlackboardType.ENEMY)

                            ),

                        new BTSelectorNode(

                            // TODO - weapon check stuff
                            new BTChangeDynamicTextNode($"State: Checking weapon")

                            )
                        
                        )

                    ),

                // always fail this
                new BTSequenceNode(
                        
                    new BTFindObjectNode(detectionRange, detectionMask),
                    new BTSetBlackboardVariableNode<float>("PlayerSpottedTimer", 0f, true, GlobalBlackboardType.ENEMY)
                        
                    ),

                new BTSequenceNode(
                    
                    new BTChangeDynamicTextNode($"State: Patrolling"),
                    // TODO - do MoveTowards inside of float conditional
                    new BTFloatConditionNode("PlayerSpottedTimer", detectionRange, ConditionalCheckType.GreaterThanOrEqual, true, GlobalBlackboardType.ENEMY,
                        
                        new BTSequenceNode(
                            
                            // move to patrol point 
                            // set next patrol point
                            
                            )
                        
                        )

                    )

                );

        tree.SetupSelf(transform, globalBlackboards);
        tree.SetupBlackboard(bb);
    }

    private void FixedUpdate()
    {
        TaskStatus result = tree.Tick();
    }
}
