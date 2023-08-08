using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HeroSkeletonTaskDamage : Node
{
    private static int _prLayerMask = 1 << 10;

    private Transform _transform;
    private Animator _animator;
    private Transform _lastTarget;

    public HeroSkeletonTaskDamage(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public void DestroyProjectile(GameObject projectile)
    {
        //Destroy(projectile);
    }

    public override NodeState Evaluate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, 0.27f, _prLayerMask);

        if (colliders.Length > 0)
        {
            object pr = colliders[0];
            Transform closestTarget = colliders[0].transform;
            GameObject p;
            p = colliders[0].gameObject;
            _animator.SetTrigger("TakeHit");
            ////UnityEngine.Object.Destroy(p);
            //DestroyProjectile(p);

            state = NodeState.SUCCESS;
            return state;

        }


        state = NodeState.FAILURE;
        return state;
    }
}