using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
   
    public float projectileSpeed = 5f;
    private bool shotCoolDown = false;
    public float maxShootRange = 30f;
    private AudioSource turretAudio;
    public AudioClip shootSFX;

    [SerializeField]
    private GameObject projectilePrefab;//Starts Yellow

    [SerializeField]
    private GameObject[] projectileColors;//0 - 2 Yellow 3 Red
    private GameObject targetPlayer;

    private float timeBetweenShotsTimer = 0;
    public float startTimeBetweenShotsTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        turretAudio = GetComponent<AudioSource>();
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


    private void SpawnProjectile()
    {
        int i = Random.Range(0, 4);
        turretAudio.PlayOneShot(shootSFX, .55f);
        projectilePrefab = projectileColors[i];
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(targetPlayer.transform.position - transform.position).normalized);
        projectile.GetComponent<Rigidbody2D>().velocity = (targetPlayer.transform.position - transform.position).normalized * projectileSpeed;
        Destroy(projectile, 7f);
        shotCoolDown = true;
    }
}
