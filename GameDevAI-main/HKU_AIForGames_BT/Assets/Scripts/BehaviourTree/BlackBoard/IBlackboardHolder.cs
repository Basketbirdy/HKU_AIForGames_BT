using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlackboardHolder
{
    public Dictionary<GlobalBlackboardType, Blackboard> Blackboards { get; }

    public void SetGlobalVariable<T>(string _variableName, T _variable, GlobalBlackboardType _BBType);
    public T GetGlobalVariable<T>(string _variableName, params GlobalBlackboardType[] _BbTypes);
}
