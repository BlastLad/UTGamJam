using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public int maxHealth = 5;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyProjectile")
        {
            TakeDamage(1);
        }
        
        if (other.gameObject.tag == "Enemy2")
        {
            TakeDamage(1);
        }

    }

    void Update()
    {
        if (currentHealth == 0)
        {
            Scene CS = SceneManager.GetActiveScene();
            SceneManager.LoadScene(CS.name);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
