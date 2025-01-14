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
    [SerializeField] private float blindnessDuration;
    [SerializeField] private LayerMask smokeMask;

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
            //Debug.Log($"Sine: {scaleOffset}");

            transform.localScale = originalScale * (1 + scaleOffset);

            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        // check if collision is object in smokeMask
        if((smokeMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            IStatusHaver target = other.transform.gameObject.GetComponent<IStatusHaver>();
            if(target == null) 
            {
                return; 
            }

            target.ApplyStatusEffect(StatusType.BLINDNESS, blindnessDuration);
        }
    }
}
