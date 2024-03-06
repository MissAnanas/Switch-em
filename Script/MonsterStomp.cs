using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    public int ennemiesCount;

    void Start()
    {
        ennemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weak Point")
        {
            Destroy(collision.gameObject);
        }
    }
}
