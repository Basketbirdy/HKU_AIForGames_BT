using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smokebomb : MonoBehaviour, ISetup<SmokebombData>
{
    private Vector3 target;
    private float speed;

    [Header("Throw arc")]
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float curveAmplitude;

    [Header("Smokecloud")]
    [SerializeField] private GameObject smokeCloudPrefab;

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

        ISetup<Nullable> smokeCloud = Instantiate(smokeCloudPrefab, transform.position, Quaternion.identity).GetComponent<ISetup<Nullable>>();
        smokeCloud.Setup(new Nullable());
        Debug.Log($"Spawned smokecloud: {smokeCloud.ToString()}");

        Destroy(gameObject);
    }

    public void Setup(SmokebombData _data)
    {
        target = GlobalBlackboard.instance.GetGlobalVariable<Transform>(_data.targetBBVariable, GlobalBlackboardType.ALLY).position;
        speed = _data.speed;
        if(_data.curve != null) { curve = _data.curve; }

        origin = transform.position;

        FireSmokebomb();
    }

    public void FireSmokebomb()
    {
        StartCoroutine(MoveSmokebomb());
    }
}

[System.Serializable]
public struct SmokebombData
{
    public string targetBBVariable;
    public float speed;
    public AnimationCurve curve;

    public SmokebombData(string _targetBBVariable, float _speed, AnimationCurve _curve = null)
    {
        targetBBVariable = _targetBBVariable;
        speed = _speed;
        curve = _curve;
    }
}
