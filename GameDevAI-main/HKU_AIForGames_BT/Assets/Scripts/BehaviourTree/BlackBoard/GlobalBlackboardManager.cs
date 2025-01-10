using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBlackboardManager : MonoBehaviour
{
    [Header("On start")]
    [SerializeField] private Transform player;

    private void Start()
    {
        VariableSetup();
    }

    private void VariableSetup()
    {
        GlobalBlackboard.instance.SetGlobalVariable<Transform>("PlayerTransform", player, GlobalBlackboardType.GLOBAL);
    }
}
