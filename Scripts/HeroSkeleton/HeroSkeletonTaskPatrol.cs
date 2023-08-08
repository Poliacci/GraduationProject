using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class HeroSkeletonTaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 1;
    private int prevWaypointIndex = 0;
    private float _speed = 2f;
    private float waitCounter = 0f;

    private float exitTime = 2f;

    public bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private float jumpForce = 8f; // Сила прыжка
    private bool isJumping = false; // Флаг, указывающий, выполняется ли прыжок
    private float JumpTime = 3f; //1 //5
    private float JumpCounter = 3f;
    private static int obstLayer = 1 << 9;
    private static int projLayer = 1 << 10;
    private static int allyLayer = 1 << 6;

    private HeroSkeletonBT HSBT;

    public HeroSkeletonTaskPatrol(Transform transform, Transform[] waypoints)
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

                if (_currentWaypointIndex == _waypoints.Length - 1) //if (_currentWaypointIndex == 0)
            {
                    _currentWaypointIndex = 0;
                    prevWaypointIndex = _waypoints.Length;

                    //sprite.flipX = false;

                }
                else
                {
                    prevWaypointIndex = _currentWaypointIndex;
                    _currentWaypointIndex++;
                    //sprite.flipX = true;

                }
            }
            else
            {
                //////////////////////////////
                anim.SetBool("Walking", false);
                waitCounter += Time.deltaTime;
                if (waitCounter >= exitTime)
                {
                    _transform.position = Vector3.MoveTowards(_transform.position, wp.position, HeroSkeletonBT.speed * Time.deltaTime); //HeroSkeletonBT.speed * Time.deltaTime
                    CheckGround();
                    if (isGrounded) anim.SetBool("Walking", true);//////
                                                                  //anim.SetBool("IDLE", false);//////
/////////////////////////////////////////////////////////////////////////////////////
                    if (isJumping)
                    {
                        JumpCounter += Time.deltaTime;
                        Collider2D[] colliders = Physics2D.OverlapCircleAll(
                    _transform.position, 1f);

                        if (colliders.Length > 0 && JumpCounter >= JumpTime)
                        {
                            JumpCounter = 0f;
                            isJumping = false;

                        }
                    }
                    ///////////////////////////////////////////
                    // Прыжок
                    if (!isJumping && IsObstacleAhead())
                    {
                        Debug.Log("jumpeee");
                        Jump();
                    }
                    ////////////////////////////////////////////////////////////////

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

            }

        

        //}
        state = NodeState.RUNNING;
        return state;
    }

    ///////////////////

    private bool IsObstacleAhead()
    {
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, 3.5f);
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, _transform.right, 5.5f);
        /////RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.right, 3.5f);
        //if(hit.collider != null && hit.collider.CompareTag("Obstacle"))

        Collider2D[] collidersObst = Physics2D.OverlapCircleAll(
        _transform.position, 0.8f, obstLayer);

        Collider2D[] collidersProj = Physics2D.OverlapCircleAll(
        _transform.position, 2.5f, projLayer);

        Collider2D[] collidersAlly = Physics2D.OverlapCircleAll(
        _transform.position, 0.5f, allyLayer);

        if (collidersObst.Length > 0 || collidersProj.Length > 0) //|| collidersAlly.Length > 1
        {
            Debug.Log("I CAAAAAAAAAAAAN'T");
            return true;
        }
        else
        {
            if (collidersAlly.Length > 1)
            {
                int rnd = Random.Range(0, 5000);
                if (rnd >= 0 && rnd <= 25)
                {
                    Debug.Log("I CAAAAAAAAAAAAN'T");
                    return true;
                }
            }

            return false;
        }

        
        
        //return hit.collider != null && hit.collider.CompareTag("Obstacle");
        //return hit.collider != null;
    }

    private void Jump()
    {
        //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        rb.AddForce(_transform.up * jumpForce, ForceMode2D.Impulse);
        isJumping = true;
        JumpCounter = 0f;
    }



    //////////////////////
}
