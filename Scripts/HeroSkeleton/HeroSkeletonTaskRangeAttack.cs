using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class HeroSkeletonTaskRangeAttack : Node
{
    private Animator _animator;
    private Transform _transform;
    private Transform slashTransform;
    private Rigidbody2D rb;
    private Rigidbody2D slashRB;
    private SpriteRenderer SlashSprite;

    private GameObject _slash;
    private GameObject _Lslash;
    private GameObject _instantiatedSlash;

    private Transform _lastTarget;
    private EnemyManagerS _enemyManager; // EnemyManagerS EnemyManager

    private float _attackTime = 10f; //1f
    private float _attackCounter = 11f; ////0f 15
    private float swing = 0f; //0
    private float attackSpeed = 1.3f; //2f
    private float animationTime = 1.3f; //2f
    private float animationTimeCounter = 0f;

    private bool strikeT;
    public string animationClipName = "Attack";
    private bool enemyIsDead2 = false;
    private bool AttackIsPossible = true;



    public HeroSkeletonTaskRangeAttack(Transform transform, GameObject slash, GameObject Lslash)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        _slash = slash;
        _Lslash = Lslash;
    }


    private bool Check()
    {
        // œÓ‚ÂˇÂÏ, Á‡‚Â¯ÂÌ‡ ÎË ‡ÌËÏ‡ˆËˇ
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) //(_animator.GetCurrentAnimatorStateInfo(0).IsName(animationClipName))
        {
            // ¿ÌËÏ‡ˆËˇ Á‡‚Â¯ÂÌ‡
            strikeT = true;
            return strikeT;
        }
        else
        {
            strikeT = false;
            return strikeT;
        }
    }



    public void SpawnSlash()
    {
        //Transform target = (Transform)GetData("target");

        GameObject instantiatedObject = GameObject.Instantiate(_slash, _transform.position, _transform.rotation);

        slashTransform = instantiatedObject.GetComponent<Transform>();
        slashRB = slashTransform.GetComponent<Rigidbody2D>();
        SlashSprite = slashTransform.GetComponent<SpriteRenderer>();



        slashRB.AddForce(slashTransform.right * 55f, ForceMode2D.Impulse);


    }

    public void SpawnLSlash()
    {

        GameObject instantiatedObject = GameObject.Instantiate(_Lslash, _transform.position, _transform.rotation);

        slashTransform = instantiatedObject.GetComponent<Transform>();
        slashRB = slashTransform.GetComponent<Rigidbody2D>();
        SlashSprite = slashTransform.GetComponent<SpriteRenderer>();

        slashRB.AddForce(slashTransform.right * -55f, ForceMode2D.Impulse);

    }


    public override NodeState Evaluate()
    {

        //Check();
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManagerS>(); // EnemyManagerS EnemyManager
            _lastTarget = target;
            //enemyIsDead = false;
        }


        _attackCounter += Time.deltaTime;


        if (Vector3.Distance(_transform.position, target.position) > HeroSkeletonBT.attackRange) //>
        {
            /*if (Check())
            {
                _animator.SetBool("Attacking", false);
                Debug.Log("FALSE LOL");
                //_animator.SetBool("Walking", true);
                state = NodeState.FAILURE;
                return state;

            }*/

        }

        if (enemyIsDead2)
        {
            ClearData("target");
            _animator.SetBool("Attacking", false);
            state = NodeState.RUNNING; //SUCCESS
            return state;

        }


        if (_attackCounter >= _attackTime)
        {
            int hp = _enemyManager._healthpoints;
            if (hp <= 0)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                state = NodeState.RUNNING; //SUCCESS
                return state;


            }

            swing += Time.deltaTime;

            if(AttackIsPossible == false)
            {
                if(animationTimeCounter >= animationTime)
                {
                    AttackIsPossible =true;
                    animationTimeCounter = 0f;
                }
                animationTimeCounter += Time.deltaTime;
            }
            _animator.SetTrigger("Attacking1");/////////////////////////////////////////////////////////////////////////// ¿ÌËÏ‡ˆËˇ ÔÂÂ‰ ÔÓÎÂÚÓÏ ‚ÓÎÌ˚
            if (enemyIsDead2 == false && swing > attackSpeed && AttackIsPossible == true) //0.2f  >=
            {

                Debug.Log("ƒ¿À‹Õﬂﬂ ¿“¿ ¿");
                //_animator.SetTrigger("Attacking1"); //Attacking1  HeavyAttack
                Debug.Log("2!!! !!! !!!");
                _animator.SetBool("Wait", true);

                AttackIsPossible = false;
                //SpawnSlash();
                if (_transform.position.x >= target.position.x)
                {
                    SpawnLSlash();
                }
                else
                {
                    SpawnSlash();
                }
                Debug.Log("À≈“»“ ¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿");


                //bool enemyIsDead = _enemyManager.TakeHit();
                //enemyIsDead2 = enemyIsDead;

                swing = 0f;
                _attackCounter = 0f;

                state = NodeState.RUNNING; //SUCCESS   ///////NEW
                return state;///////////////////////
            }
            else
            {
                
                state = NodeState.FAILURE;
                return state;
            }


        }

        state = NodeState.FAILURE;
        return state;
    }

}