using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = health;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        slider.value = health;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
