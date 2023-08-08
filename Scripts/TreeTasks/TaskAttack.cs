using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;
    private Transform _transform;

    private Transform _lastTarget;
    private EnemyManagerSH _enemyManager; // EnemyManagerS EnemyManager

    private float _attackTime = 1.4f; //1
    private float _attackCounter = 0f;
    private float swing = 0f;
    private float attackSpeed = 1.5f; //2f
    private float exitTime = 2f;
    private bool strikeT;
    public string animationClipName = "Attack";
    private bool enemyIsDead2 = false;


    /*[AnimationEvent]
    public void OnAnimationComplete()
    {
        // Код для выполнения после завершения анимации
        // ...
    }*/

    public TaskAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        //_transform = transform;
    }

    /*private void Update()
    {

        //strikeT = true;
    }*/

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
            _enemyManager = target.GetComponent<EnemyManagerSH>(); // EnemyManagerS EnemyManager
            _lastTarget = target;
            //enemyIsDead = false;
        }


        _attackCounter += Time.deltaTime;


        if (Vector3.Distance(_transform.position, target.position) > SkeletonBT.attackRange) //>
        {
            if (Check())
            {
                _animator.SetBool("Attacking", false);
                //_animator.SetBool("Walking", true);
                state = NodeState.FAILURE;
                return state;

            }

            /*_animator.SetBool("Attacking", false);
            _animator.SetBool("Walking", true);
            state = NodeState.FAILURE;
            return state;*/

        }
        //bool timeStrike = true;

        /*if (_attackCounter >= _attackTime)
        {
            bool strike = (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            if(strike)
            {
                _animator.SetTrigger("Attacking1");//////////////////////////////////////
                strike = (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);


                //bool enemyIsDead = false;
                //bool enemyIsDead = _enemyManager.TakeHit();
                if (strike)
                {
                    bool enemyIsDead = _enemyManager.TakeHit();
                    if (enemyIsDead)
                    {
                        ClearData("target");
                        _animator.SetBool("Attacking", false);
                        _animator.SetBool("Walking", true);
                        state = NodeState.FAILURE;
                        return state;
                    }
                    else
                    {
                        _attackCounter = 0f;
                    }

                }
                
            }
            _attackCounter = 0f;



        }*/
        //_attackCounter += Time.deltaTime;
        
        if (_attackCounter >= _attackTime)
        {
            int hp = _enemyManager._healthpoints;
            if (hp <= 0)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                //_animator.SetBool("Walking", true);
                //state = NodeState.FAILURE;
                //return state;

            }    
            //_animator.SetTrigger("Attacking1");//////////////////////////////////////
            //Debug.Log("a");
            //strike = (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            swing += Time.deltaTime;

            if (enemyIsDead2 == false && swing >= attackSpeed) //0.2f
            {
                _animator.SetTrigger("Attacking1");

                _animator.SetBool("Wait", true);

                Debug.Log("!!!");
                bool enemyIsDead = _enemyManager.TakeHit();
                enemyIsDead2 = enemyIsDead;
                /*if (enemyIsDead)
                {
                    ClearData("target");
                    _animator.SetBool("Attacking", false);
                    _animator.SetBool("Walking", true);
                    //state = NodeState.FAILURE;
                    //return state;
                }*/
                swing = 0f;
                _attackCounter = 0f;

            }

            if (enemyIsDead2)
            {
                ClearData("target");
                _animator.SetBool("Attacking", false);
                //_animator.SetBool("Walking", true);
                //state = NodeState.FAILURE;
                //return state;
            }
            //_attackCounter = 0f;

            /*bool strike = (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
            if (strike)
            {
                
            }*/
            //bool enemyIsDead = false;
            //bool enemyIsDead = _enemyManager.TakeHit();









        }

        state = NodeState.RUNNING;
        return state;
    }

}