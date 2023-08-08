using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskGoToTarget : Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;
    private SpriteRenderer sprite;
    private Animator _animator;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
        sprite = transform.GetComponentInChildren<SpriteRenderer>();
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        
        /*float counter = 0;
        //_animator.SetBool("Walking", false);
        //_animator.SetTrigger("Reacting1");
        //Wait for 4 seconds
        float waitTime = 40004;
        bool itIsTime = false;
        while (itIsTime == false)
        {
            if(counter >= waitTime)
            {
                itIsTime = true;
            }
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
        }*/
        //_animator.SetTrigger("Reacting1");
        float dirX = _transform.position.x;
        Transform target = (Transform)GetData("target");

        Collider2D[] collidersFOV = Physics2D.OverlapCircleAll(
                _transform.position, SkeletonBT.fovRange, _enemyLayerMask);

        if (collidersFOV.Length <= 0)
        {
            ClearData("target");
            _animator.SetBool("Walking", true); //true
            _animator.SetBool("Reacting", false);
            _animator.SetBool("Attacking", false);
            state = NodeState.FAILURE;
            return state;

        }

        if (Vector3.Distance(_transform.position, target.position) > 1.3f) //0.01
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, SkeletonBT.speed * Time.deltaTime);
            _animator.SetBool("Walking", true); //true
            _animator.SetBool("Reacting", false);

            if(Vector3.Distance(_transform.position, target.position) == 1.2f)
            {
                Debug.Log("ондньек");
                _animator.SetBool("Walking", false);
                _animator.SetBool("Attacking", true);
                state = NodeState.SUCCESS;
                return state;
            }

            bool right = true;
            if (_transform.position.x >= target.position.x)
            {
                right = false;

            }
            else
            {
                right = true;
            }
            if ((_transform.position.x > target.position.x) && (right == false))
            {
                sprite.flipX = true;
                //Debug.Log(_transform.position.x);
                //Debug.Log(right);
            }
            else
            {
                if (right == true)
                {
                    sprite.flipX = false;
                    //Debug.Log(right);
                }
            }
            //_transform.LookAt(target.position);
        }

        

        state = NodeState.RUNNING;
        return state;
    }

}