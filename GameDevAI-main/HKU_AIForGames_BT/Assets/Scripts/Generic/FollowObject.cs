using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Space]
    [SerializeField] private bool x;
    [SerializeField] private bool y;
    [SerializeField] private bool z;
    [Space]
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        transform.position = GetNewPos();
    }

    private Vector3 GetNewPos()
    {
        Vector3 newPos = transform.position;

        if (x) { newPos.x = target.position.x; }
        if (y) { newPos.y = target.position.y; }
        if (z) { newPos.z = target.position.z; }
        
        newPos = newPos + offset;

        return newPos;
    }
}
