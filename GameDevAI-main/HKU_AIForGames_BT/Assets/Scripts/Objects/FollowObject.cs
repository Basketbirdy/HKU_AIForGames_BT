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
    [Space]
    [SerializeField] bool usesLocalPosition = false;

    private void FixedUpdate()
    {
        transform.position = GetNewPos();
    }

    private Vector3 GetNewPos()
    {
        Vector3 newPos = transform.position;

        if (x) { newPos.x = target.position.x; }
        else { newPos.x = 0; }
        if (y) { newPos.y = target.position.y; }
        else { newPos.y = 0; }
        if (z) { newPos.z = target.position.z; }
        else { newPos.z = 0; }
        
        newPos = newPos + offset;

        return newPos;
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }
}
