using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HeroSkeletonTaskGoToTarget : Node
{
    private static int _enemyLayerMask = 1 << 7;

    private Transform _transform;
    private SpriteRenderer sprite;
    private Animator _animator;
    private Rigidbody2D rb;

    ////////
    private float jumpForce = 5f; // Сила прыжка
    private bool isJumping = false; // Флаг, указывающий, выполняется ли прыжок
    private float JumpTime = 3f; //1 //5
    private float JumpCounter = 3f;
    private static int obstLayer = 1 << 9;
    private static int projLayer = 1 << 10; //10
    //


    public HeroSkeletonTaskGoToTarget(Transform transform)
    {
        _transform = transform;
        sprite = transform.GetComponentInChildren<SpriteRenderer>();
        _animator = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        
        float dirX = _transform.position.x;
        Transform target = (Transform)GetData("target");
        object t = GetData("target");

        Collider2D[] collidersFOV = Physics2D.OverlapCircleAll(
                _transform.position, HeroSkeletonBT.fovRange, _enemyLayerMask);

        if(t != null)
        {
            if (Vector3.Distance(_transform.position, target.position) > 1.3f) //0.01
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, target.position, HeroSkeletonBT.speed * Time.deltaTime);
                _animator.SetBool("Walking", true); //true
                _animator.SetBool("Reacting", false);

                if(isJumping)
                {
                    JumpCounter += Time.deltaTime;
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(
                _transform.position, 1f);

                    if(colliders.Length > 0 && JumpCounter >= JumpTime)
                    {
                        JumpCounter = 0f;
                        isJumping = false;

                    }
                }

                // Прыжок
                if (!isJumping && IsObstacleAhead())
                {
                    Debug.Log("jumpeee");
                    Jump();
                }
                ////////////////////////////////

                if (Vector3.Distance(_transform.position, target.position) == 1.2f)
                {
                    Debug.Log("ПОДОШЕЛ");
                    _animator.SetBool("Walking", false);
                    _animator.SetBool("Attacking", true);
                    //state = NodeState.SUCCESS;
                    //return state; ////////////////////////убрал 01.06
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

                }
                else
                {
                    if (right == true)
                    {
                        sprite.flipX = false;

                    }
                }

            }
            state = NodeState.RUNNING;
            return state;

        }
        
        state = NodeState.FAILURE; //RUNNING
        return state;
    }
    ///////////////////

    /*private bool IsObstacleAhead()
    {
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, 3.5f);
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, _transform.right, 5.5f);
        RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, 1f);
        Debug.Log("I CAN'T");
        return hit.collider != null && hit.collider.CompareTag("Obstacle");
        //return hit.collider != null;
    }*/


    private bool IsObstacleAhead()
    {
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.down, 3.5f);
        //RaycastHit2D hit = Physics2D.Raycast(_transform.position, _transform.right, 5.5f);
        /////RaycastHit2D hit = Physics2D.Raycast(_transform.position, Vector2.right, 3.5f);
        //if(hit.collider != null && hit.collider.CompareTag("Obstacle"))

        Collider2D[] collidersObst = Physics2D.OverlapCircleAll(
        _transform.position, 0.8f, obstLayer);

        //Collider2D[] collidersProj = Physics2D.OverlapCircleAll(
        //_transform.position, 0.5f, projLayer);

        if (collidersObst.Length > 0) //if (collidersObst.Length > 0 || collidersProj.Length > 1) //
        {
            Debug.Log("I CAAAAAAAAAAAAN'T");
            return true;
        }
        else
        {
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