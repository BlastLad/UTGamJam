using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyProjectile : MonoBehaviour
{
    public float projectileSpeed = 3;

    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * projectileSpeed;
        Invoke("DestroyTimer", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy1")
        {
            other.gameObject.GetComponent<EnemyType1Controller>().TakeDamage(1);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemy2")
        {
            other.gameObject.GetComponent<EnemyType2Controller>().TakeDamage(1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DestroyTimer()
    {
        Destroy(gameObject);
    }
}
