using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = SeansTestPlayerController.Instance.gameObject;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // ENTER DAMAGE PLAYER FUNCTION HERE
            // ex: player.Damage( some int );

            // this implements knockback to player when taking dmg, can be used for dmg from enemies too
            // StartCoroutine(player.Knockback(float duration, float force, player.transform.position));
        }
    }
}

