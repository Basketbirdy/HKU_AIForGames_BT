using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SmokeCloud : MonoBehaviour, ISetup<Nullable>
{
    private bool alive;
    private float timeElapsed;
    private Vector3 originalScale;

    [Header("Data")]
    [SerializeField] private float lifetime;

    [Header("Animation")]
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;

    public void Setup(Nullable _data)
    {
        originalScale = transform.localScale;

        StartCoroutine(SmokeCloudLifeTime());
    }

    private IEnumerator SmokeCloudLifeTime()
    {
        alive = true;

        while(alive)
        {
            if(timeElapsed >= lifetime) 
            { 
                alive = false;
            }

            timeElapsed += Time.deltaTime;

            float scaleOffset = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * (timeElapsed / lifetime));
            Debug.Log($"Sine: {scaleOffset}");

            transform.localScale = originalScale * (1 + scaleOffset);

            yield return null;
        }

        Destroy(gameObject);
    }
}
