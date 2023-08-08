using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 1;
    private float _speed = 2f;
    private float waitCounter = 0f;

    private float exitTime = 2f;

    public bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        _waypoints = waypoints;

        sprite = transform.GetComponentInChildren<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }



    public override NodeState Evaluate()
    {
        /////////////////////////////////////////////////////////
        void CheckGround()
        {
            if (!isGrounded) anim.SetBool("Walking", false); //State = States.Walk;  // ВЕРНУТЬ ДЛЯ АНИМАЦИИ ПРЫЖКА Jump
            Collider2D[] collider = Physics2D.OverlapCircleAll(_transform.position, 1.2f); // 1.2f  10.8295f
            isGrounded = collider.Length > 1;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        Transform wp = _waypoints[_currentWaypointIndex];
        Vector3 dir = wp.position;
        float dirX = _transform.position.x;
        bool right = true;

        if (Vector3.Distance(_transform.position, wp.position) < 1.1f)
        {

            if (_currentWaypointIndex == 0)
            {
                _currentWaypointIndex = 1;
                //sprite.flipX = false;

            }
            else
            {
                _currentWaypointIndex = 0;
                //sprite.flipX = true;

            }
        }
        else
        {
            //////////////////////////////
            anim.SetBool("Walking", false);
            waitCounter += Time.deltaTime;
            if(waitCounter >= exitTime)
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, SkeletonBT.speed * Time.deltaTime);
                CheckGround();
                if (isGrounded) anim.SetBool("Walking", true);//////
                //anim.SetBool("IDLE", false);//////
                if (_transform.position.x >= wp.position.x)
                {
                    right = false;

                }
                else
                {
                    right = true;
                }
                if ((_transform.position.x > wp.position.x) && (right == false))
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
            }

            /////////////////
            ///
            /// 
            /// 
            /*
            _transform.position = Vector3.MoveTowards(_transform.position, wp.position, SkeletonBT.speed * Time.deltaTime);
            CheckGround();
            if (isGrounded) anim.SetBool("Walking", true);//////
            anim.SetBool("IDLE", false);//////
            if (_transform.position.x >= wp.position.x)
            {
                right = false;
                
            }
            else
            {
                right = true;
            }
            if ((_transform.position.x > wp.position.x)&&(right==false))
            {
                sprite.flipX = true;
                //Debug.Log(_transform.position.x);
                //Debug.Log(right);
            }
            else
            {
                if(right == true)
                {
                    sprite.flipX = false;
                    //Debug.Log(right);
                }
            }
            */

        }
        state = NodeState.RUNNING;
        return state;
    }
}
