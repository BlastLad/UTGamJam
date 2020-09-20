using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherScript : MonoBehaviour
{
    public float timeValue;

    private AudioSource motherAudio;
    //public GameObject cameraItem;
    
    
    // Start is called before the first frame update
    void Start()
    {
        motherAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.EnterEvent();
            Debug.Log("Playef found");
           
            PutOnAShow();
            
        }
    }

    private void PutOnAShow()
    {
        motherAudio.Play();
        //Animation???
         StartCoroutine(LoadNextSceneTime(timeValue));
        ShakeBehavior.Instance.gameObject.GetComponent<CameraFollow>().SetPosInEvent();
        ShakeBehavior.Instance.TriggerShake();
    }

    private IEnumerator LoadNextSceneTime(float timeToWait)
    {
        Debug.Log("load scene called);");
        yield return new WaitForSeconds(timeToWait);
        GameManager.Instance.LoadNextScene();
    }
}
