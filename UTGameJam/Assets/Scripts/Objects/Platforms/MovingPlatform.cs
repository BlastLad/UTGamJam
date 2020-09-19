using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPos;

    Vector3 nextPos;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, pos1.position) < 0.1f)
        {
            Debug.Log(pos1 + "Reached");
            nextPos = pos2.position;
        }
        if (Vector2.Distance(transform.position, pos2.position) < 0.1f)
        {
            Debug.Log(pos2 + " 2 Reached");
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        


    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.collider.transform.SetParent(null);
        }
    }

    void OnDrawGizmos()
    {
        //OnDrawGizmos();
        // Draw a yellow line at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
