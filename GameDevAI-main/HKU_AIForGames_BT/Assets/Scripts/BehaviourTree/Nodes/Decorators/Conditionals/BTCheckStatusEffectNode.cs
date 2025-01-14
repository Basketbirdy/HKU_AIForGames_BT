using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCheckStatusEffectNode : BTConditionalDecoratorNode
{
    private Dictionary<StatusType, float> activeEffects;
    private StatusType type;
    private bool isActive;

    public BTCheckStatusEffectNode(ref Dictionary<StatusType, float> _activeEffects, StatusType _type, bool _isActive, BTBaseNode _child) : base(_child)
    {
        activeEffects = _activeEffects;
        type = _type;

        isActive = _isActive;
    }

    protected override bool TryCondition()
    {
        if (activeEffects.ContainsKey(type)) 
        {
            //Debug.Log($"Is affected by {type}");

            if (isActive) { return true; }
            return false;
        }
        else 
        {
            if (isActive) { return false; }
            return true; 
        }
    }
}
