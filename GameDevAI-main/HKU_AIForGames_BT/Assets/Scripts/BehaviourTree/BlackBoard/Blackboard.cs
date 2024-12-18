using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<string, object> data = new Dictionary<string, object>();

    public T GetVariable<T>(string variableName)
    {
        if (data.ContainsKey(variableName))
        {
            return (T)data[variableName];
        }
        return default(T);
    }

    public void SetVariable<T>(string variableName, T variable) 
    {
        if (data.ContainsKey(variableName))
        {
            data[variableName] = variable;
        }
        else
        {
            data.Add(variableName, variable);
        }
    }
}
