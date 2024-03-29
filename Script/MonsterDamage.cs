using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    public MonsterStomp stomp;
    public BigMonsterStomp stomp2;
    public PathSecret path;
    public PathSecret2 path2;
    public PlayerMovements playerMovement;

    public int damage;
    public PlayerHealth playerHealth;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                playerMovement.KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                playerMovement.KnockFromRight = false;
            }
            playerHealth.TakeDamage(damage);
        }
    }
    private void OnDestroy()
    {
        stomp.ennemiesCount -= 1;
        path.DestroyPath(stomp.ennemiesCount);
        stomp2.ennemiesCount2 -= 1;
        path2.DestroyPath2(stomp2.ennemiesCount2);
    }


}

