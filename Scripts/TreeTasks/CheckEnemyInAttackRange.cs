using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null)
        {
            //////
            /*if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                state = NodeState.FAILURE;
                return state;
            }*/
            //////

            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= SkeletonBT.attackRange)
        {
            //_animator.SetTrigger("Attacking1");
            _animator.SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            //Debug.Log("AAAAAAAAA");

            state = NodeState.SUCCESS;
            return state;
        }
        /*else
        {
            _animator.SetBool("Attacking", false);
            _animator.SetBool("Walking", true);
            state = NodeState.FAILURE;
            return state;

        }*/
        //Debug.Log("ฯฮาละ฿ห");
        _animator.SetBool("Attacking", false);
        _animator.SetBool("Walking", true);
        /*///////
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            state = NodeState.FAILURE;
            
        }
        else
        {
            state = NodeState.RUNNING;
        }
        /////*/
        state = NodeState.FAILURE;
        return state;
    }

}