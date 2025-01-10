using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboardManager : MonoBehaviour
{
    [Header("On Awake")]
    [SerializeField] private Transform player;

    private void Awake()
    {
        VariableSetup();
    }

    private void Start()
    {
        
    }

    private void VariableSetup()
    {
        // global blackboard
        GlobalBlackboard.instance.SetGlobalVariable<Transform>("PlayerTransform", player, GlobalBlackboardType.GLOBAL);

        // global enemy blackboard
        GlobalBlackboard.instance.SetGlobalVariable<float>("PlayerSpottedTimer", 999, GlobalBlackboardType.ENEMY);
    }
}
