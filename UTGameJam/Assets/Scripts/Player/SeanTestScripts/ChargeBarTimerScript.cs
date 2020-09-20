using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChargeBarTimerScript : MonoBehaviour
{
    public static ChargeBarTimerScript Instance { get; private set; }

    public GameObject yellowSprite;
    public GameObject RedSprite;

    [SerializeField]
    GameObject timerBar;

    public bool canUseBarrier = true;
    public bool isBarActive = false;
    public float BarTime;
    public float BarTimer;
    public Slider slider;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        BarTimer = BarTime;
        SetMax(BarTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (canUseBarrier == true && isBarActive == true)
        {
            BarTimer -= Time.deltaTime;
            SetTimer(BarTimer);       
            if (BarTimer <= 0)
            {
                //isBarActive = false;
                canUseBarrier = false;
                SeansTestPlayerController.Instance.RetractBarrier();
                StartCoroutine(BarCoolDown(2.5f));
            }
        }
        else if (isBarActive == false && slider.value < slider.maxValue && canUseBarrier == true)
        {
            if (BarTimer <= slider.maxValue)
            {
                BarTimer += Time.deltaTime;
                SetTimer(BarTimer);
            }
                    
        }
    }


    private IEnumerator BarCoolDown(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        canUseBarrier = true;
    }
    public void SetIsBarActive(bool val)
    {
        isBarActive = val;
        Debug.Log("baractive called" + val);
    }

    public void SetTimer(float time)
    {
        slider.value = time;
    }

    public void SetMax(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void ChangeImage()
    {
        if(yellowSprite.gameObject.activeSelf)
        {
            yellowSprite.SetActive(false);
            RedSprite.SetActive(true);
        }
        else
        {
            yellowSprite.SetActive(true);
            RedSprite.SetActive(false);
        }
        
    }
}
