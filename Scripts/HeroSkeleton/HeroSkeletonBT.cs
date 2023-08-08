using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class HeroSkeletonBT : Treee
{
    public UnityEngine.Transform[] waypoints;
    public GameObject slash;
    public GameObject Lslash;

    public static float speed = 2f;
    public static float fovRange = 5f; //4f
    public static float attackRange = 1.8f; //1.2
    public static float RangeAttackRange = 7.5f; //1.2
    //public static float delayTime = 0f; //1.2

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
                new CheckEnemyInHeroSkeletonRangeAttackRange(transform),

                new CheckAllyInHeroSkeletonFOVRange(transform),////

                new HeroSkeletonTaskRangeAttack(transform, slash, Lslash),
            }),



            new Sequence(new List<Node>
            {
                new CheckEnemyInHeroSkeletonAttackRange(transform),

                new Selector(new List<Node>
                {
                    //new HeroSkeletonTaskRangeAttack(transform, slash),

                    new HeroSkeletonTaskHeavyAttack(transform),

                    new HeroSkeletonTaskAttack(transform),

                }),/**/
            }),



            

            new Sequence(new List<Node>
            {
                new CheckEnemyInHeroSkeletonFOVRange(transform),

                new HeroSkeletonTaskGoToTarget(transform),

            }),


            new Sequence(new List<Node>
            {
                new Delay(transform),
                new HeroSkeletonTaskPatrol(transform, waypoints),

            }),


        });

        return root;
    }

}
