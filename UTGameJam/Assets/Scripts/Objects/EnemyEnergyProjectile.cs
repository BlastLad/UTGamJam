using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergyProjectile : MonoBehaviour
{
    public bool isYellow = true;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //rb.velocity = transform.right * projectileSpeed;
        Invoke("DestroyTimer", 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnergyField")
        {          
            if (SeansTestPlayerController.Instance.isBarrierActive)
            {
                if(SeansTestPlayerController.Instance.isYellow == isYellow)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeansTestPlayerController.Instance.TakeDamage(1);
            
        }
        Destroy(gameObject);
    }

    private void DestroyTimer()
    {
        Destroy(gameObject);
    }
}
