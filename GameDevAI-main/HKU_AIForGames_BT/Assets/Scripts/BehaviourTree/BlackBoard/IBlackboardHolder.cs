using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlackboardHolder
{
    public Dictionary<BlackboardType, Blackboard> Blackboards { get; }

    public void SetGlobalVariable<T>(string _variableName, T _variable, BlackboardType _BBType);
    public T GetGlobalVariable<T>(string _variableName, BlackboardType _BbType);
}
