using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = 1 << 6;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInFOVRange(Transform transform)
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
        if (t == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, SkeletonBT.fovRange, _enemyLayerMask);

            //OnDrawGizmos();

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform); //parent.paretn т.к. находится на 2 уровня выше
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", true);
                _animator.SetTrigger("Reacting1");

                /*//Wait for 4 seconds
                float counter = 0;
                float waitTime = 60004;
                bool itIsTime = false;
                while (itIsTime == false)
                {
                    if (counter >= waitTime)
                    {
                        itIsTime = true;
                    }
                    //Increment Timer until counter >= waitTime
                    counter += Time.deltaTime;
                }
                */

                state = NodeState.SUCCESS;
                return state;
            }
            /////
            if(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
            {
                state = NodeState.FAILURE;
                return state;
            }
            /////
            
            //state = NodeState.FAILURE;
            //return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }

    void OnDrawGizmos()
    {
        // Отрисовываем круг на сцене
        Gizmos.DrawWireSphere(_transform.position, SkeletonBT.fovRange);

        // Отрисовываем круг в консоли
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, SkeletonBT.fovRange); // Collider2d[] Physics2D
        foreach (Collider2D collider in colliders)
        {
            Debug.DrawLine(_transform.position, collider.transform.position, Color.red);
        }
    }

}