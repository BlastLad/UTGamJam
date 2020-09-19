using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialPopUp : MonoBehaviour
{
    SpriteRenderer spriteBody;// Start is called before the first frame update
    public Text attatchedText;

    public bool OnEnterActivate = false;
    void Start()
    {
        spriteBody = GetComponent<SpriteRenderer>();
        if (OnEnterActivate) { 
            spriteBody.enabled = false;
            attatchedText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && OnEnterActivate)
        {
            spriteBody.enabled = true;
            attatchedText.enabled = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            spriteBody.enabled = false;
            attatchedText.enabled = false;
        }
    }
}
