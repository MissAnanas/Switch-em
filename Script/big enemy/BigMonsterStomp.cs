using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterStomp : MonoBehaviour
{
    public int damage;
    public MonsterHealth monsterHealth;

    public int ennemiesCount2;

    void Start()
    {
        ennemiesCount2 = GameObject.FindGameObjectsWithTag("BigEnemy").Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weak Point 2")
        {
            monsterHealth.TakeDamage(damage);
        }
    }
}
