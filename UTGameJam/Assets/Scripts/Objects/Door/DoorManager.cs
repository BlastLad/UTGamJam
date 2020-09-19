using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject buttonForDoor;

    private bool isOpen = true;
    

    public void OpenOrCloseDoor(bool val)
    {
        //Animation play
        GetComponent<SpriteRenderer>().enabled =  val;
        GetComponent<BoxCollider2D>().enabled = val;
    }
}
