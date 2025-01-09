using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attacked state")]
    [SerializeField] private float attackedStateDuration = 5f;
    [SerializeField] private bool attacked = false;
    [SerializeField] private float attackedTimer = 0;
    private Coroutine attackedCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            GlobalBlackboard.instance.SetGlobalVariable<Transform>("AttackingEnemy", transform, GlobalBlackboardType.ALLY);
            TakeDamage(10f); 
        }
    }

    public void TakeDamage(float _damage)
    {
        if(attackedCoroutine != null) { attackedTimer = 0f; }
        attackedCoroutine = StartCoroutine(AttackedTimer());
    }

    public IEnumerator AttackedTimer()
    {
        attacked = true;
        GlobalBlackboard.instance.SetGlobalVariable<bool>("PlayerAttacked", true, GlobalBlackboardType.ALLY);

        attackedTimer = 0f;

        while (attacked)
        {
            if(attackedTimer >= attackedStateDuration) 
            {
                attacked = false;
                GlobalBlackboard.instance.SetGlobalVariable<bool>("PlayerAttacked", false, GlobalBlackboardType.ALLY);
                attackedCoroutine = null;
                yield return null;
            }
            attackedTimer += Time.deltaTime;
            yield return null;
        }
    }
}
