using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimestampCheck { ISFINSIHED, ISRUNNING }
public class BTCheckTimestampNode : BTConditionDecoratorNode
{
    private string timestampBBVariable;
    private float delay;

    private TimestampCheck check;
    private BlackboardType blackboardType;

    public BTCheckTimestampNode(string _timestampBBVariable, float _delay, BlackboardType _blackboardType, TimestampCheck _check, BTBaseNode _child) : base(_child)
    {
        timestampBBVariable = _timestampBBVariable;
        delay = _delay;

        check = _check;
        blackboardType = _blackboardType;
    }

    protected override bool TryCondition()
    {
        float timestamp;
        if (blackboardType != BlackboardType.LOCAL) { timestamp = GlobalBlackboard.instance.GetGlobalVariable<float>(timestampBBVariable, blackboardType); }
        else { timestamp = blackboard.GetVariable<float>(timestampBBVariable); }

        switch (check)
        {
            case TimestampCheck.ISFINSIHED:
                if (Time.time >= timestamp + delay) { return true; }
                else { return false; }
        }

        if (Time.time >= timestamp + delay || timestamp == 0) { return false; }
        else { return true; }
    }
}
