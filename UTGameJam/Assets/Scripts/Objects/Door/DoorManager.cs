using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject buttonForDoor;

    private bool isOpen = true;

    public Animator doorAnim;
    

    public void OpenOrCloseDoor(bool val)
    {
        //Animation play
        doorAnim.SetBool("DoorClosed", val);
        buttonForDoor.gameObject.SetActive(val);
        GetComponent<SpriteRenderer>().enabled =  val;
        GetComponent<BoxCollider2D>().enabled = val;
    }
}
