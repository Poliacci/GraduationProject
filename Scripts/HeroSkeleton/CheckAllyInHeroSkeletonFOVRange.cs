using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckAllyInHeroSkeletonFOVRange : Node
{
    private static int _allyLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;
    private Transform _lastTarget;

    public CheckAllyInHeroSkeletonFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {

        object t = GetData("ally");

        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, HeroSkeletonBT.RangeAttackRange, _allyLayerMask);


            if (colliders.Length > 1) //0
            {
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", true);

                int l = colliders.Length;
                object closest = colliders[0];
                Transform closestTarget = colliders[0].transform;
                for (int i = 0; i < l; i++)
                {
                    Transform targetTransform = colliders[i].transform;
                    if (Vector3.Distance(_transform.position, targetTransform.position) <= Vector3.Distance(_transform.position, closestTarget.position))
                    {
                        closestTarget = targetTransform;
                    }
                }

                parent.parent.SetData("ally", closestTarget); //parent.paretn Ú.Í. Ì‡ıÓ‰ËÚÒˇ Ì‡ 2 ÛÓ‚Ìˇ ‚˚¯Â
                Debug.Log("Õ¿ÿ≈À —Œﬁ«Õ» ¿ ¬ œŒÀ≈ «–≈Õ»ﬂ");
                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                ClearData("ally");
                _animator.SetBool("Walking", false);
                state = NodeState.SUCCESS;
                return state;
            }

        }
        else
        {
            Transform target = (Transform)GetData("ally");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, HeroSkeletonBT.RangeAttackRange, _allyLayerMask);


            if (colliders.Length > 0)
            {
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", true);


                if (target != _lastTarget)
                {

                    _lastTarget = target;
                    _animator.SetTrigger("Reacting1");
                    state = NodeState.FAILURE;
                    return state;
                }
                if (target == _lastTarget)
                {

                    state = NodeState.FAILURE;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }
            else
            {
                ClearData("ally");
                _animator.SetBool("Walking", false);
                Debug.Log("œŒ“≈–ﬂÀ —Œﬁ«Õ» ¿ »« œŒÀﬂ «–≈Õ»ﬂ");///////////////
                state = NodeState.SUCCESS;
                return state;
            }
        }
    }
}
