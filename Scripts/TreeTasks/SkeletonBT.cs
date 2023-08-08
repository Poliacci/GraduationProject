using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class SkeletonBT : Treee
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 4f;
    public static float attackRange = 1.8f; //1.2

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new HeroSkeletonTaskDamage(transform),

            }),

            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),

            }),

            new TaskPatrol(transform, waypoints),
        });

        return root;
    }
}
