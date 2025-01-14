        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSimpleMeleeAttackNode : BTBaseAttackNode
{
    public BTSimpleMeleeAttackNode(float _range, float _damage, Vector3 _offset, LayerMask _hitMask) : base(_range, _damage, _offset, _hitMask) { }

    protected override Collider[] GetHits()
    {
        Vector3 attackPos = self.position + self.forward * offset.x + self.right * offset.z + self.up * offset.y;

        return Physics.OverlapSphere(attackPos, range, hitMask);
    }
}
