using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokebomb : MonoBehaviour
{
    private Vector3 target;
    private float speed;

    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float curveAmplitude;

    private Vector3 origin;
    private Vector3 ghostPosition;

    private IEnumerator MoveSmokebomb()
    {
        ghostPosition = origin;
        ghostPosition.y += 1;

        while(ghostPosition != target)
        {
            ghostPosition = Vector3.MoveTowards(ghostPosition, target, speed * Time.deltaTime);

            float distanceTraveled = Vector3.Distance(ghostPosition, origin);
            float maxDistance = Vector3.Distance(origin, target);
            float t = Utils.Remap(distanceTraveled, 0f, maxDistance);
            Debug.Log($"t = {t}; curve = {curve.Evaluate(t)}");

            float yOffset = curve.Evaluate(t) * curveAmplitude;
            Debug.Log($"yOffset: {yOffset}");

            Vector3 finalPos = ghostPosition;
            finalPos.y += yOffset;

            transform.position = finalPos;

            yield return null;
        }
    }

    public void SmokebombSetup(Vector3 _target, float _speed, AnimationCurve _curve = null)
    {
        target = _target;
        speed = _speed;
        if(_curve != null) { curve = _curve; }

        origin = transform.position;

        FireSmokebomb();
    }

    public void FireSmokebomb()
    {
        StartCoroutine(MoveSmokebomb());
    }
}
