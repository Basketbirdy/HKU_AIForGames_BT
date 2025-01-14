using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetup<T> where T : struct
{
    public void Setup(T _data);
}

public struct Nullable
{

}
