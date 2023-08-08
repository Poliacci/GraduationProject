using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HeroSkeletonTaskAttack : Node
{
    private Animator _animator;
    private Transform _transform;

    private Transform _lastTarget;
    private EnemyManagerS _enemyManager; // EnemyManagerS EnemyManager

    private float _attackTime = 1.4f; //1
    private float _attackCounter = 0f;
    private float swing = 0f;
    private float attackSpeed = 1.5f; //2f
    private float exitTime = 2f;
    private bool strikeT;
    public string animationClipName = "Attack";
    private bool enemyIsDead2 = false;




    public HeroSkeletonTaskAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();

    }


    private bool Check()
    {
        // Проверяем, завершена ли анимация
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) //(_animator.GetCurrentAnimatorStateInfo(0).IsName(animationClipName))
        {
            // Анимация завершена
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
            if (Check())
            {
                _animator.SetBool("Attacking", false);
                Debug.Log("FALSE LOL");
                //_animator.SetBool("Walking", true);
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

            if (enemyIsDead2 == false && swing >= attackSpeed) //0.2f
            {
                Debug.Log("ОБЫЧНАЯ АТАКА");
                _animator.SetTrigger("Attacking1"); //Attacking1  HeavyAttack
                Debug.Log("2!!!");
                _animator.SetBool("Wait", true);
                bool enemyIsDead = _enemyManager.TakeHit();
                enemyIsDead2 = enemyIsDead;

                swing = 0f;
                _attackCounter = 0f;

                state = NodeState.RUNNING; //SUCCESS   ///////NEW
                return state;///////////////////////
            }
            else//////////////////////////////////////////////
            {/////////////////////////////////////////////////
                //Debug.Log("ВТОРОЙ НЕ СМОГ СДЕЛАТЬ ОБЫЧНУЮ АТАКУ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                state = NodeState.FAILURE;////////////////////
                return state;/////////////////////////////////
            }/////////////////////////////////////////////////


        }

        state = NodeState.RUNNING;
        return state;
    }

}