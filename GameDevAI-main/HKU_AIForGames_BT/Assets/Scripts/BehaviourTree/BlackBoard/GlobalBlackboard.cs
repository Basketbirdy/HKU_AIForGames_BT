using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GlobalBlackboardType { GLOBAL, ENTITY, ENEMY, ALLY }
public class GlobalBlackboard : MonoBehaviour, IBlackboardHolder
{
    public static GlobalBlackboard instance;

    private Dictionary<GlobalBlackboardType, Blackboard> blackboards = new Dictionary<GlobalBlackboardType, Blackboard>();
    public Dictionary<GlobalBlackboardType, Blackboard> Blackboards => blackboards;

    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Destroy(instance.gameObject); }

        SetupBlackboards();
    }

    private void Start()
    {
        
    }

    public T GetGlobalVariable<T>(string _variableName, GlobalBlackboardType _BBType)
    {
        if (!blackboards.ContainsKey(_BBType)) 
        {
            Debug.LogWarning($"Action: Get | No accessable blackboard found using provided codes! Returning default value");
            return (T)default; 
        }
        return blackboards[_BBType].GetVariable<T>(_variableName);
    }

    public void SetGlobalVariable<T>(string _variableName, T _variable, GlobalBlackboardType _BBType)
    {
        if (!blackboards.ContainsKey(_BBType)) 
        {
            Debug.LogWarning($"Action: Set | No accessable blackboard found using provided code!");
            return; 
        }
        blackboards[_BBType].SetVariable<T>(_variableName, _variable);
    }

    private void SetupBlackboards()
    {
        foreach(GlobalBlackboardType bb in Enum.GetValues(typeof(GlobalBlackboardType)))
        {
            blackboards.Add(bb, new Blackboard());
        }
    }
}
