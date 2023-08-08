using UnityEngine;

public class EnemyManagerSH : MonoBehaviour
{
    public Animator anim;
    private Transform _transform;
    private BoxCollider2D boxCollider;
    public int _healthpoints;
    private bool died = false;

    private void Awake()
    {
        _healthpoints = 7;
        _transform = transform;
        anim = transform.GetComponent<Animator>();
    }

    public bool TakeHit()
    {
        
        anim.SetTrigger("TakeHit");
        _healthpoints -= 1;
        bool isDead = _healthpoints <= 0;
        if(!died)
        {
            if (isDead) _Die();
        }
        
        return isDead;


        //Wait for 4 seconds
        /*float counter = 0;
        float waitTime = 56004;
        bool itIsTime = false;
        while (itIsTime == false)
        {
            if (counter >= waitTime)
            {
                itIsTime = true;
            }
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
        }}*/
        //bool isDead = false;
        //bool time = (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        //if (time)
        //{


        //}

        //if (isDead) _Die();
        //return isDead;


    }

    

    private void _Die()
    {
        if(!died)
        {
            anim.SetBool("Death", true);
            gameObject.layer = 9; //1;
            //_transform.layer = 1;
            GetComponent<HeroSkeletonBT> ().enabled = false;

            Collider2D[] collider = Physics2D.OverlapCircleAll(_transform.position, 1.2f); // 1.2f  10.8295f
            /*bool isGrounded = collider.Length > 1;

            if (isGrounded)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                //GetComponent<Rigidbody2D>().BodyType = static;

            }*/

            this.enabled = false;
            //Destroy(gameObject);
            died = true;
        }


    }
}