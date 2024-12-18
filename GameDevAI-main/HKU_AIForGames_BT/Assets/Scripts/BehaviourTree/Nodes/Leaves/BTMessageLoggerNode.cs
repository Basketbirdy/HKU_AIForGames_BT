using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogType { DEFAULT, WARNING, ERROR }

/// <summary>
/// Leaf node that debugs a provided message into the console
/// <br>if no LogType provided defaults to normal Debug.Log instead</br>
/// </summary>
public class BTMessageLoggerNode : BTBaseNode
{
    private string message;
    private LogType logType;

    public BTMessageLoggerNode(string _message, LogType _logType = LogType.DEFAULT)
    {
        message = _message;
        logType = _logType;
    }

    protected override TaskStatus OnUpdate()
    {
        switch (logType)
        {
            case LogType.DEFAULT:
                Debug.Log($"[{blackboard.GetVariable<string>(VariableNames.OBJECT_NAME)}] {message}");
                break;
            case LogType.WARNING:
                Debug.LogWarning($"[{blackboard.GetVariable<string>(VariableNames.OBJECT_NAME)}] {message}");
                break;
            case LogType.ERROR: 
                Debug.LogError($"[{blackboard.GetVariable<string>(VariableNames.OBJECT_NAME)}] {message}");
                break;
        }

        return TaskStatus.SUCCESS;   
    }
}
