using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeProjectile : Projectille
{

    public override void MoveProjectile()
    {
        //base.MoveProjectile();
        MoveHitToTarget(target);
    }

    void MoveHitToTarget(Vector3 point)
    {
        transform.position = point;
    }
}
