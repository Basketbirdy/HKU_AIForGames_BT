using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable, IStatusHaver
{
    [Header("Attacked state")]
    [SerializeField] private float attackedStateDuration = 5f;
    [SerializeField] private bool attacked = false;
    [SerializeField] private float attackedTimer = 0;
    private Coroutine attackedCoroutine;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float health;
    [HideInInspector] public float Health { get => health; set => health += value; }

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            TakeDamage(10f, gameObject); 
        }
    }

    public void TakeDamage(float _damage, GameObject _attacker)
    {
        // do damage (not neccessary)
        health -= _damage;
        if(health <= 0) { Die(); }

        //Debug.Log($"[{gameObject.name}] Hit by {_attacker.name} for {_damage} damage");

        GlobalBlackboard.instance.SetGlobalVariable<Transform>("LastKnownAttacker", _attacker.transform, BlackboardType.ALLY);

        if (attackedCoroutine != null) { attackedTimer = 0f; }
        attackedCoroutine = StartCoroutine(AttackedTimer());
    }

    public void Die() { }

    public IEnumerator AttackedTimer()
    {
        attacked = true;
        GlobalBlackboard.instance.SetGlobalVariable<bool>("PlayerAttacked", true, BlackboardType.ALLY);

        attackedTimer = 0f;

        while (attacked)
        {
            if(attackedTimer >= attackedStateDuration) 
            {
                attacked = false;
                GlobalBlackboard.instance.SetGlobalVariable<bool>("PlayerAttacked", false, BlackboardType.ALLY);
                attackedCoroutine = null;
                yield return null;
            }
            attackedTimer += Time.deltaTime;
            yield return null;
        }
    }

    public void ApplyStatusEffect(StatusType _type, float _duration)
    {

    }
}
