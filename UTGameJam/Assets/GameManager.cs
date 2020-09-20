using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentScene;
    public float timeToClose = 3f;

    public GameObject crashTextBox;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (currentScene > 1)
        {
            crashTextBox.SetActive(true);
            StartCoroutine(CloseWindow(timeToClose));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator CloseWindow(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        crashTextBox.SetActive(false);
    }

    public void EnterEvent()
    {
        SeansTestPlayerController.Instance.gameObject.GetComponent<PlayerMovement>().enabled = false;
        SeansTestPlayerController.Instance.enabled = false;
    }

    public void LoadNextScene()
    {
        int CSnum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 2);
    }

    public void ResetCurrentScene()
    {
        Scene CS = SceneManager.GetActiveScene();
        SceneManager.LoadScene(CS.name);
    }
}
