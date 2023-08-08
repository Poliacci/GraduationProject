using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInHeroSkeletonFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 7;

    private Transform _transform;
    private Animator _animator;
    private Transform _lastTarget;

    public CheckEnemyInHeroSkeletonFOVRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    IEnumerator WaitForFunction()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("Hello!");
    }

    public override NodeState Evaluate()
    {

        object t = GetData("target");
        ///


        ///
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, HeroSkeletonBT.fovRange, _enemyLayerMask);

            //OnDrawGizmos();

            if (colliders.Length > 0)
            {
                //parent.parent.SetData("target", colliders[0].transform); //parent.paretn т.к. находитс€ на 2 уровн€ выше
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", true);
                //_animator.SetTrigger("Reacting1");
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

                parent.parent.SetData("target", closestTarget); //parent.paretn т.к. находитс€ на 2 уровн€ выше
                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                ClearData("target");
                _animator.SetBool("Walking", false);
                //Debug.Log("ѕќ“≈–яЋ ÷≈Ћ№ »« ѕќЋя «–≈Ќ»я");///////////////
                state = NodeState.FAILURE;
                return state;
            }

        }
        else 
        {
            Transform target = (Transform)GetData("target");
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, HeroSkeletonBT.fovRange, _enemyLayerMask);


            if (colliders.Length > 0)
            {
                //parent.parent.SetData("target", colliders[0].transform); //parent.paretn т.к. находитс€ на 2 уровн€ выше
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", true);
                //_animator.SetTrigger("Reacting1");

                if (target != _lastTarget)
                {
                    //Debug.Log("Ќова€ цель");
                    //Debug.Log(t);
                    _lastTarget = target;
                    _animator.SetTrigger("Reacting1");
                    state = NodeState.SUCCESS;
                    return state;
                }
                if (target == _lastTarget)
                {
                    //Debug.Log("“а же цель");
                    //Debug.Log(t);
                    state = NodeState.SUCCESS;
                    return state;
                }



                state = NodeState.SUCCESS;
                return state;
            }
            else
            {
                ClearData("target");
                _animator.SetBool("Walking", false);
                Debug.Log("ѕќ“≈–яЋ ÷≈Ћ№ »« ѕќЋя «–≈Ќ»я");///////////////
                state = NodeState.FAILURE;
                return state;
            }
            
            

            
        }

        //state = NodeState.SUCCESS;
        //return state;
    }


}