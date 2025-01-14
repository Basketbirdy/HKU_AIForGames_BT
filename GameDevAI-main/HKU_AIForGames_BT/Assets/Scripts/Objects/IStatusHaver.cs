using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusType 
{ 
    BLINDNESS,
}

public interface IStatusHaver
{
    public void ApplyStatusEffect(StatusType _type, float _duration);
}
