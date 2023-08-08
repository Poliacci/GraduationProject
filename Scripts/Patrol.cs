using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int _currentWaypointIndex = 1;
    private float _speed = 2f;

    public bool isGrounded = false;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();


    }

    private void FixedUpdate()
    {
        CheckGround();


    }
    private void CheckGround()
    {
        //if (!isGrounded) State = States.Walk;  // ВЕРНУТЬ ДЛЯ АНИМАЦИИ ПРЫЖКА Jump
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1.2f); // 1.2f  10.8295f
        isGrounded = collider.Length > 1;
    }

    private void Update()
    {
        Transform wp = waypoints[_currentWaypointIndex];
        Vector3 dir = wp.position;
        int m = 0;
        if (m == 0)
        {
            m++;
            //sprite.flipX = true;
        }

        if (Vector3.Distance(transform.position, wp.position) < 1.1f)
        {
            //sprite.flipX = dir.x < 0.0f;
            //_currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
            if (_currentWaypointIndex == 0)
            {
                _currentWaypointIndex = 1;
                sprite.flipX = false;

            }
            else
            {
                _currentWaypointIndex = 0;
                sprite.flipX = true;

            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, wp.position, _speed * Time.deltaTime);
            //transform.LookAt(wp.position);
            //Vector3 dirr = wp.position;
            //sprite.flipX = dir.x < 0.0f;
            //sprite.flipX = true;
            //sprite.flipX = false;
        }


    }
}
