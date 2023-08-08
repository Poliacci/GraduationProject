using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform _transform;
    private SpriteRenderer sprite;
    //private static int _enemyLayerMask = 1 << 7;
    //private static int _enemyLayerMask2 = 1 << 6;
    //private EnemyManagerS _enemyManager;
    //private EnemyManagerSH _enemyManager;

    // Start is called before the first frame update
    //Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, HeroSkeletonBT.fovRange, _enemyLayerMask);
    void Start()
    {
        _transform = transform;
        sprite = transform.GetComponentInChildren<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(this.gameObject);
        Debug.Log("BOOOOM");
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Destroy(this.gameObject);
        //Debug.Log("BOOOOM");
    }/**/

}
