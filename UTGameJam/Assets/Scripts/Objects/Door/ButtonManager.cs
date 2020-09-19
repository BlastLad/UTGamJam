using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject attachedDoor;
        
        
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "PlayerProjectile" || other.gameObject.tag == "EnemyProjectile")
        {
            attachedDoor.GetComponent<DoorManager>().OpenOrCloseDoor(false);
        }
    }
}
