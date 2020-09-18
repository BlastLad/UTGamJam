using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeansTestPlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 5;
    public float playerRunSpeed = 5f;
    public float playerFallSpeed = 2f;
    private float projectileOffSet = 0;

    public float shotCoolDown;
    private float shotCoolDownTimer;
    private bool canShoot = true;

    [SerializeField]
    private float slowFallGravScale = 0.2f;
    private float regularGravScale = 1f;

    private bool isBarrierActive = false;


    public Transform fireOrigin;
    public GameObject energyShotPrefab;
    private Rigidbody2D rb;
    private CircleCollider2D energyBarrierTrigger;

    Vector3 startPosition;

    Vector3 moveVector;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        energyBarrierTrigger = GetComponent<CircleCollider2D>();
        startPosition = rb.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.y = 0;
        moveVector.z = 0;

        Vector3 shotRotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = startPosition;
        }

        if (Input.GetMouseButton(1))
        {
            DeployBarrier();
            Debug.Log("Right Button Down");
        }
        else { RetractBarrier(); }
    }

    private void FixedUpdate()
    {
        Vector3 movement = moveVector;
        transform.position += moveVector * playerSpeed * Time.fixedDeltaTime;//updated position area
        //rb.MovePosition(position);//actual movement of player
    }

    void FireWeapon()
    {
        if (canShoot && isBarrierActive == false)
        {
            Instantiate(energyShotPrefab, fireOrigin.transform.position, fireOrigin.rotation);
            shotCoolDownTimer = shotCoolDown;

        }
    }
    void DeployBarrier()
    {
        energyBarrierTrigger.enabled = true;
        rb.gravityScale = slowFallGravScale;
        isBarrierActive = true;
        playerSpeed = playerFallSpeed;
        ChargeBarTimerScript.Instance.SetIsBarActive(true);
    }

    void RetractBarrier()
    {
        energyBarrierTrigger.enabled = false;
        rb.gravityScale = regularGravScale;
        isBarrierActive = false;
        playerSpeed = playerRunSpeed;
        ChargeBarTimerScript.Instance.SetIsBarActive(false);
    }
}
