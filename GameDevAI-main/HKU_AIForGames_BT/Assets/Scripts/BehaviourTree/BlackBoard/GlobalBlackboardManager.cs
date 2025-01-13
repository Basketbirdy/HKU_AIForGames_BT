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
        GlobalBlackboard.instance.SetGlobalVariable<Transform>("PlayerTransform", player, BlackboardType.GLOBAL);

        // global enemy blackboard
        //GlobalBlackboard.instance.SetGlobalVariable<float>("Player_LastSeen", 999, BlackboardType.ENEMY);
    }
}
