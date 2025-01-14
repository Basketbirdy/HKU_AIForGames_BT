using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTBaseAttackNode : BTBaseNode
{
    protected float range;
    protected float damage;
    protected Vector3 offset;

    protected LayerMask hitMask;

    public BTBaseAttackNode(float _range, float _damage, Vector3 _offset, LayerMask _hitMask)
    {
        range = _range;
        damage = _damage;
        offset = _offset;
        hitMask = _hitMask;
    }

    protected override TaskStatus OnUpdate()
    {
        Collider[] hits = GetHits();
        
        foreach (Collider hit in hits)
        {
            IDamagable damagable = hit.GetComponent<IDamagable>();
            if(damagable == null) { continue; }

            damagable.TakeDamage(damage, self.gameObject);
        }

        if(hits == null) { return TaskStatus.FAILURE; }
        return TaskStatus.SUCCESS;
    }

    protected abstract Collider[] GetHits();
}
