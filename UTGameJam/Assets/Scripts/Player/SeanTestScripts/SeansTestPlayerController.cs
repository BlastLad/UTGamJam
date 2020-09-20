using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeansTestPlayerController : MonoBehaviour
{
    public static SeansTestPlayerController Instance { get; private set; }

    private AudioSource playerAudio;
    public AudioClip damageSFX;
    public AudioClip shootSFX;
    public AudioClip barrierSFX;

    [SerializeField]
    public int maxHealth = 5;
    public int currentHealth;
    private HealthBar healthBar;


    private float playerSpeed = 5;
    public float playerRunSpeed = 5f;
    public float playerFallSpeed = 2f;
    private float projectileOffSet = 0;



    public float shotCoolDown;
    private float shotCoolDownTimer;
    public bool canShoot = true;
    public bool canChangeColor = true;
    public bool isYellow = true;

    [SerializeField]
    private float slowFallGravScale = 0.2f;
    private float regularGravScale = 1f;

    public bool isBarrierActive = false;
    private bool isInvincible = false;

    public Transform fireOrigin;
    public GameObject energyShotPrefab;
    public GameObject energyField;
    private Rigidbody2D rb;
    private CircleCollider2D energyBarrierTrigger;
    public Animator playerAnim;

    public Transform followPoint;

    Vector3 startPosition;

    Vector3 moveVector;
    private void Awake()
    {
        Instance = this;
        playerAudio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        energyBarrierTrigger = GetComponentInChildren<CircleCollider2D>();
        startPosition = rb.transform.position;
       
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar = HealthBar.Instance;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = 0;
        moveVector.z = 0;
        if (moveVector.x != 0) { playerAnim.SetBool("IsMoving", true); }
        else { playerAnim.SetBool("IsMoving", false); }


        Vector3 shotRotation = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        Debug.Log(Input.mousePosition);
        float rotationOnZ = Mathf.Atan2(shotRotation.y, shotRotation.x) * Mathf.Rad2Deg;
        fireOrigin.rotation = Quaternion.Euler(0f, 0f, rotationOnZ + projectileOffSet);

        if (shotCoolDownTimer <= 0)
        {
            if (Input.GetButtonDown("Fire1")) { FireWeapon(); }
        }
        else
        {
            shotCoolDownTimer -= Time.deltaTime;
        }
       
        if (Input.GetKeyDown(KeyCode.LeftShift)) { ChangeColor(); }


        if (Input.GetMouseButton(1))
        {
            playerAnim.SetBool("IsShielding", true);
            DeployBarrier();
            Debug.Log("Right Button Down");
        }
        else { RetractBarrier();
            playerAnim.SetBool("IsShielding", false);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = moveVector;
        transform.position += moveVector * playerSpeed * Time.fixedDeltaTime;//updated position area

        //Vector3 position = rb.position;//Current Position of Player
        //position = position + moveVector * playerSpeed * Time.fixedDeltaTime;//updated position area
        //rb.MovePosition(position);

    }

    void FireWeapon()
    {
        if (canShoot && isBarrierActive == false)
        {
            playerAudio.PlayOneShot(shootSFX, 0.35f);
            playerAnim.SetTrigger("Shoot");
            Instantiate(energyShotPrefab, fireOrigin.transform.position, fireOrigin.rotation);
            shotCoolDownTimer = shotCoolDown;

        }
    }
    void DeployBarrier()
    {
        if (!isBarrierActive) { rb.velocity = new Vector2(rb.velocity.x, 0); }

        if (ChargeBarTimerScript.Instance.canUseBarrier == true)
        {
            //if (!playerAudio.isPlaying)
            //{
              //  playerAudio.PlayOneShot(barrierSFX, .2f);
            //}
            playerAnim.SetBool("IsShielding", true);
            energyBarrierTrigger.enabled = true;
            rb.gravityScale = slowFallGravScale;
            isBarrierActive = true;           
            if (rb.velocity.y <= -3.0f) { rb.velocity = new Vector2(0, -3.0f); }
            //rb.velocity = new Vector2(0,0); If this version make fallspeed 7
            playerSpeed = playerFallSpeed;
            ChargeBarTimerScript.Instance.SetIsBarActive(true);
        }
    }

    public void TakeDamage(int damageVal)
    {
        rb.velocity = new Vector2(0, 0);
        if (!isInvincible)
        {
            currentHealth -= damageVal;
            playerAudio.PlayOneShot(damageSFX, 0.4f);
            healthBar.SetHealth(currentHealth);
            isInvincible = true;
            StartCoroutine(Invincibile(2f));
            if (currentHealth <= 0)
            {
                GameManager.Instance.ResetCurrentScene();
            }
        }
    }

    private IEnumerator Invincibile(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isInvincible = false;

    }

    void ChangeColor()
    {
        if (isBarrierActive == false && canChangeColor)
        {
            isYellow = !isYellow;
            //if (isYellow) { playerAnim.SetBool("IsYellow", isYellow); }
            playerAnim.SetBool("IsYellow", isYellow);
            energyField.GetComponent<EnergyField>().SwitchColor();
            ChargeBarTimerScript.Instance.ChangeImage();
            Debug.Log(isYellow);
        }
    }
    public void RetractBarrier()
    {
        
        energyBarrierTrigger.enabled = false;
        rb.gravityScale = regularGravScale;
        isBarrierActive = false;
        playerSpeed = playerRunSpeed;
        ChargeBarTimerScript.Instance.SetIsBarActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnergyBeam" && isBarrierActive == false)
        {
            TakeDamage(1);
        }
    }

}

