using UnityEngine;

public class EnemyManagerS : MonoBehaviour
{
    public Animator anim;
    private Transform _transform;
    private BoxCollider2D boxCollider;
    public int _healthpoints;
    private bool died = false;

    private void Awake()
    {
        _healthpoints = 5;
        _transform = transform;
        anim = transform.GetComponent<Animator>();
        boxCollider = _transform.GetComponent<BoxCollider2D>();

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
            gameObject.layer = 9; //1
            //_transform.layer = 1;
            //GetComponent<Collider2D>().enabled = false;
            GetComponent<SkeletonBT>().enabled = false;

            boxCollider.size = new Vector2(0.1f, 0.1f);
            boxCollider.offset = new Vector2(0f, -0.1f);
            this.enabled = false;
            //Destroy(gameObject);
            died = true;
        }


    }
}