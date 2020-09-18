using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2Controller : MonoBehaviour
{
    public int maxHitNum = 4;
    private int currentHitNum = 0;
    public float speed = 3.0f;
    public bool isVertical;
    public float changeTime = 3.0f;
    public Animator animator;
    bool isReverse = false;


    Rigidbody2D rb;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }
    private void FixedUpdate()
    {
        
    }

  

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        //What changes Enemy's Direction
        if (timer < 0)
        {
            direction = -direction;
            if (isReverse == false)
            {
                isReverse = true;
            }
            else
            {
                isReverse = false;
            }
            timer = changeTime;
            
        }
        PositionMover();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Health Lost");
            DirectionalKnockback(other.gameObject);
        }
    }
    public void TakeDamage(int damageVal)
    {
        currentHitNum++;
        rb.velocity = new Vector2(0, 0);
        if (currentHitNum >= maxHitNum)
        {
            Destroy(gameObject);
        }
    }

    public void PositionMover()
    {
        Vector2 position = rb.position;

        if (isVertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            //transform.position += new Vector2(position.x, position.y) * speed * Time.fixedDeltaTime;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }
        Debug.Log("MoveCalled");
        rb.MovePosition(position);
    }

    private void DirectionalKnockback(GameObject other)
    {

    }
}
