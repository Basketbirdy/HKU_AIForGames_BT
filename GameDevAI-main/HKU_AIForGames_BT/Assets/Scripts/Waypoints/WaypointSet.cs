using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointSet
{
    [Header("Waypoints")]
    [SerializeField] private Transform[] points;

    private int currentIndex;

    public int AdvanceWaypoint()
    {
        return default(int);
    }

    public int GetCurrentWaypoint()
    {
        return default(int);
    }
}
