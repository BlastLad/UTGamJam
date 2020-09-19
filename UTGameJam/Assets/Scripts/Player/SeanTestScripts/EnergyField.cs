using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyField : MonoBehaviour
{
    private bool isYellow = true;// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnergyBeam")
        {
            if (other.gameObject.GetComponentInParent<EnergyBeamController>().isYellow == isYellow)
            {
                other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
            {
               SeansTestPlayerController.Instance.TakeDamage(1);
            }
        }
        else if (other.gameObject.tag == "Player")
        {
            SeansTestPlayerController.Instance.TakeDamage(1);
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnergyBeam")
        {
            if (other.gameObject.GetComponentInParent<EnergyBeamController>().isYellow == isYellow)
            {
                other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }
    }*/



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnergyBeam")
        {
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    public void SwitchColor()
    {
        isYellow = !isYellow;
    }
}
