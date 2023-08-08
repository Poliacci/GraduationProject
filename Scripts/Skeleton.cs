using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/


    [SerializeField]
    private float speed = 3f;
    [SerializeField]
    private int lives = 5;
    [SerializeField]
    private float jumpForce = 5f;

    public bool isGrounded = false; //bil private

    public int kills = 0; //new
    public float m = 0;
    public int sce = 0;

    public bool jumped = false;
    public bool moved = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Skeleton Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Instance = this;

    }

    private void Walk()
    {
        if (isGrounded) State = States.Walk;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;

        anim.SetBool("Walking", true);//////
        anim.SetBool("IDLE", false);//////

    }

    private void Update()
    {
        if (lives <= 0) SceneManager.LoadScene(1);
        if (isGrounded) State = States.IDLE;
        if (isGrounded) anim.SetBool("IDLE", false); //////

        if (Input.GetButton("Horizontal"))
            Walk();
        if (isGrounded && Input.GetButtonDown("Jump")) //if (Input.GetButtonDown("Jump"))//
            Jump();
        if (kills >= 2)
        {
            kills = 0;

        }

    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    public void Jump()  // bil private
    {
        jumped = true;
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        m++;

    }

    private void CheckGround()
    {
        if (!isGrounded) State = States.Walk;  // ВЕРНУТЬ ДЛЯ АНИМАЦИИ ПРЫЖКА Jump
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1.2f); // 1.2f  10.8295f
        isGrounded = collider.Length > 1;
    }

    public void GetDamage()  // bil private // public override void GetDamage()
    {
        lives--;
        Debug.Log(lives);
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void GetDeath()  // bil private
    {
        lives = lives - 10;
        Debug.Log(lives);
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void GetKill()  // bil private
    {
        kills++;
        Debug.Log(kills);
        //rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        // Отрисовываем круг на сцене
        Gizmos.DrawWireSphere(transform.position, 1.2f);

        Gizmos.DrawWireSphere(transform.position, 4f);

        // Отрисовываем круг в консоли
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.2f);
        foreach (Collider2D collider in colliders)
        {
            Debug.DrawLine(transform.position, collider.transform.position, Color.red);
        }

        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, 4f);
        foreach (Collider2D collider in colliders2)
        {
            Debug.DrawLine(transform.position, collider.transform.position, Color.green);
        }
    }

}
public enum States
{
    IDLE,
    Walk
}
