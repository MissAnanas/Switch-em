using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumper : MonoBehaviour
{
    public float bounce;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
        }
    }
}


