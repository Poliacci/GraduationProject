using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class Delay : Node
{
    private Animator anim;
    private Transform _transform;
    private float exitTime = 5f;
    private float timer = 0f;

    //public Delay() : base() { }
    //public Delay(List<Node> children) : base(children) { }

    public Delay(Transform transform)
    {
        _transform = transform;
        anim = transform.GetComponent<Animator>();
        //_transform = transform;
    }

    public override NodeState Evaluate()
    {
        //float timer += Time.deltaTime;
        timer += Time.deltaTime;
        bool wait = anim.GetBool("Wait");
        if(wait)
        {
            if (timer >= exitTime)
            {
                timer = 0f;
                Debug.Log("TIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIME");
                anim.SetBool("Wait", false);
                state = NodeState.SUCCESS;
                return state;

            }
            else
            {
                timer += Time.deltaTime;
                //Debug.Log("NAH");
                anim.SetBool("Wait", true);
                state = NodeState.RUNNING;
                return state;
            }

        }
        timer = 0f;
        state = NodeState.SUCCESS;
        return state;



    }
}
