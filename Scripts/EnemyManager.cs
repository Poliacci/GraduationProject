using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int _healthpoints;

    private void Awake()
    {
        _healthpoints = 3;
    }

    public bool TakeHit()
    {
        _healthpoints -= 1;
        bool isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        //Destroy(gameObject);

    }
}