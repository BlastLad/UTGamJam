using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private GameObject player;
    private AudioSource spikeAudio;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        spikeAudio = GetComponent<AudioSource>();
        player = SeansTestPlayerController.Instance.gameObject;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SeansTestPlayerController.Instance.TakeDamage(1);
            if (!spikeAudio.isPlaying)
            {
                spikeAudio.PlayOneShot(clip, 0.15f);
            }
            // this implements knockback to player when taking dmg, can be used for dmg from enemies too
            // StartCoroutine(player.Knockback(float duration, float force, player.transform.position));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
            SeansTestPlayerController.Instance.TakeDamage(1);
            // this implements knockback to player when taking dmg, can be used for dmg from enemies too
            // StartCoroutine(player.Knockback(float duration, float force, player.transform.position));
        }
    }
}

