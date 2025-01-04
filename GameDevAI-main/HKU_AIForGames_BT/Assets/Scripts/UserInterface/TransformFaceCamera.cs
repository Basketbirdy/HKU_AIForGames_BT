using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFaceCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Camera cameraToLookAt = Camera.main;
        Vector3 v = cameraToLookAt.transform.position - transform.position;
        v.x = 0.0f;
        v.z = 0.0f;
        transform.LookAt(cameraToLookAt.transform.position - v);
        transform.Rotate(0, 180, 0);
    }
}
