using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckStatusEffectNode : BTConditionalDecoratorNode
{
    private Dictionary<StatusType, float> activeEffects;
    private StatusType type;
    private bool ifIsActive;

    public BTCheckStatusEffectNode(ref Dictionary<StatusType, float> _activeEffects, StatusType _type, bool _ifIsActive, BTBaseNode _child) : base(_child)
    {
        activeEffects = _activeEffects;
        type = _type;

        ifIsActive = _ifIsActive;
    }

    protected override bool TryCondition()
    {
        if (activeEffects.ContainsKey(type)) 
        {
            //Debug.Log($"Is affected by {type}");

            if (ifIsActive) { return true; }
            return false;
        }
        else 
        {
            if (ifIsActive) { return false; }
            return true; 
        }
    }
}
