using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HeroSkeletonTaskHeavyAttack : Node
{
    private Animator _animator;
    private Transform _transform;

    private Transform _lastTarget;
    private EnemyManagerS _enemyManager; // EnemyManagerS EnemyManager

    private float _HeavyAttackTime = 3f; //1 //5
    private float _HeavyAttackCounter = 0f;
    private float swing = 0f;
    private float attackSpeed = 2.3f; //2f

    private bool strikeT;
    public string animationClipName = "HeavyAttack";
    private bool enemyIsDead2 = false;



    public HeroSkeletonTaskHeavyAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();

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


    public override NodeState Evaluate()
    {

        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManagerS>(); // EnemyManagerS EnemyManager
            _lastTarget = target;

        }


        _HeavyAttackCounter += Time.deltaTime;


        if (Vector3.Distance(_transform.position, target.position) > HeroSkeletonBT.attackRange) //>
        {
            if (Check())
            {
                _animator.SetBool("Attacking", false);

                state = NodeState.FAILURE;
                return state;

            }

        }
        if (enemyIsDead2)
        {
            ClearData("target");
            _animator.SetBool("Attacking", false);

            state = NodeState.RUNNING; //SUCCESS
            return state;

        }


        if (_HeavyAttackCounter >= _HeavyAttackTime)
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

            if (enemyIsDead2 == false && swing >= attackSpeed) //0.2f
            {
                _animator.SetTrigger("HeavyAttack"); //Attacking1  HeavyAttack
                Debug.Log("2!!!!!!");

                _animator.SetBool("Wait", true);

                bool enemyIsDead = _enemyManager.TakeHit();
                enemyIsDead = _enemyManager.TakeHit();
                enemyIsDead2 = enemyIsDead;

                swing = 0f;
                _HeavyAttackCounter = 0f;

                state = NodeState.RUNNING; //SUCCESS   ///////NEW
                return state;///////////////////////
            }
            else//////////////////////////////////////////////
            {/////////////////////////////////////////////////
                //Debug.Log("¬“Œ–Œ… Õ≈ —ÃŒ√ —ƒ≈À¿“‹ “ﬂ∆≈À”ﬁ ¿“¿ ”!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                state = NodeState.FAILURE;////////////////////
                return state;/////////////////////////////////
            }/////////////////////////////////////////////////
            

        }
        else///////////////////////////////////////////////////////////› —œ≈–»Ã≈Õ“
        {
            state = NodeState.FAILURE;////////////////////
            return state;/////////////////////////////////

        }///////////////////////////////////////////////////////////////

        //state = NodeState.RUNNING; //state = NodeState.RUNNING;
        //return state;
    }

}