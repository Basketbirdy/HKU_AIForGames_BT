using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public static class Utils
{
    public static float Remap(float _value, float _fromMin, float _fromMax)
    {
        return (_value - _fromMin) / (_fromMax - _fromMin);
    }
}
