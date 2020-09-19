using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1Controller : MonoBehaviour
{
    //ENEMY SHOULD HAVE A CIRCULAR RANGE BEFORE FIRING AT THE PLAYER
    public int maxHitNum = 7;
    private int currentHitNum = 0;
    public float projectileSpeed = 5f;
    private bool shotCoolDown = false;
    public float maxShootRange = 30f;

    [SerializeField]
    private GameObject projectilePrefab;//Starts Yellow

    [SerializeField]
    private GameObject[] projectileColors;//0 yellow 1 red

    private GameObject targetPlayer;
    
    private float timeBetweenShotsTimer = 0;
    public float startTimeBetweenShotsTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = SeansTestPlayerController.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(targetPlayer.transform.position, transform.position) < maxShootRange)
        {
            if (shotCoolDown == true)
            {
                timeBetweenShotsTimer -= Time.deltaTime;
                if (timeBetweenShotsTimer < 0)
                {
                    shotCoolDown = false;
                }
            }


            if (shotCoolDown == false)
            {
                shotCoolDown = true;
                timeBetweenShotsTimer = startTimeBetweenShotsTime;
                SpawnProjectile();


            }
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeansTestPlayerController.Instance.TakeDamage(1);
            DirectionalKnockback(other.gameObject);
        }
    }

    public void TakeDamage(int damageVal)
    {
        currentHitNum++;
        if (currentHitNum >= maxHitNum) 
        {
            Destroy(gameObject);
        }
    }

    private void DirectionalKnockback(GameObject other)
    {
        
    }

    private void SpawnProjectile()
    {
        
            int i = Random.Range(0, 2);

            projectilePrefab = projectileColors[i];
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(targetPlayer.transform.position - transform.position).normalized);
            projectile.GetComponent<Rigidbody2D>().velocity = (targetPlayer.transform.position - transform.position).normalized * projectileSpeed;
            Destroy(projectile, 7f);
            shotCoolDown = true;
       
    }
}
