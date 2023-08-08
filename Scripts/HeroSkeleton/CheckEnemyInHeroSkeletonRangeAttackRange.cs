using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInHeroSkeletonRangeAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInHeroSkeletonRangeAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            _animator.SetBool("Attacking", false);

            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= HeroSkeletonBT.RangeAttackRange)
        {

            _animator.SetBool("Attacking", true); //ÏÅÐÅÕÎÄ Â ÔÀÇÓ ÀÒÀÊÈ
            _animator.SetBool("Walking", false);


            state = NodeState.SUCCESS;
            return state;
        }

        _animator.SetBool("Attacking", false);
        _animator.SetBool("Walking", true);

        state = NodeState.FAILURE;
        return state;
    }

}