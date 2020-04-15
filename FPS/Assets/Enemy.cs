
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public Healthbar healthbar;


    public void Start()
    {
        health = health;
        healthbar.SetMaxHealth(health);
    }



    public void TakeDamage(float amount)
    {
        health -= amount;
        healthbar.SetHealth(health);
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
