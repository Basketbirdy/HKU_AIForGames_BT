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
    }

    public T GetGlobalVariable<T>(string _variableName, params GlobalBlackboardType[] _BBTypes)
    {
        foreach (GlobalBlackboardType code in _BBTypes)
        {
            if (!blackboards.ContainsKey(code)) { continue; }

            return blackboards[code].GetVariable<T>(_variableName);
        }

        Debug.LogWarning($"Action: Get | No accessable blackboard found using provided codes! Returning default value");
        return (T)default;
    }

    public void SetGlobalVariable<T>(string _variableName, T _variable, GlobalBlackboardType _BBType)
    {
        if (!blackboards.ContainsKey(_BBType)) { return; }
        blackboards[_BBType].SetVariable<T>(_variableName, _variable);

        Debug.LogWarning($"Action: Set | No accessable blackboard found using provided code!");
    }
}
