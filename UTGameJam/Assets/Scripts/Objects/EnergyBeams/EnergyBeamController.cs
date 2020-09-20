using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeamController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform laserHit;

    public bool isYellow = true;
    public bool isOnTimer = false;
    public float switchTime = 3f;

    public Material yellowBeam;
    public Material redBeam;

    public GameObject secondPositionControl;
    BoxCollider2D lineCollider;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.useWorldSpace = true;
        MakeLineVisible();
        if (isOnTimer) { StartCoroutine(TurnOffLine(switchTime)); }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        Debug.DrawLine(transform.position, hit.point);
        lineRenderer.SetPosition(0, transform.position);
        //Second Position
        lineRenderer.SetPosition(1, secondPositionControl.transform.position);

        if (lineCollider != null)
        {
            UpdateLineColldierPosition(lineRenderer, transform.position, secondPositionControl.transform.position);

        }

        //MakeLineVisible();

    }

    public void MakeLineVisible()
    {
        lineRenderer.enabled = true;
        if (isYellow) { lineRenderer.material = yellowBeam; }
        else { lineRenderer.material = redBeam; }
         
        AddColliderToLine(lineRenderer, transform.position, secondPositionControl.transform.position);
    }

    public void MakeLineInVisible()
    {

        lineRenderer.enabled = false;
        Destroy(lineCollider.gameObject);
        
        //Destroy(GetComponentInChildren<Transform>());
    }


    private void AddColliderToLine(LineRenderer line, Vector2 start, Vector2 end)
    {
        lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider2D>();

        lineCollider.transform.parent = line.transform;

        lineCollider.tag = "EnergyBeam";


        lineCollider.gameObject.layer = 13;//EnergyBeam

        lineCollider.isTrigger = false;

        float lineWidth = line.endWidth;

        float lineLength = Vector2.Distance(start, end);

        lineCollider.size = new Vector2(lineLength, lineWidth);

        Vector2 midPoint = (start + end) / 2;

        

        lineCollider.transform.position = midPoint;


        float angle = Mathf.Atan2((end.y - start.y), (end.x - start.x));

        angle *= Mathf.Rad2Deg;

        //angle *= -1;

        lineCollider.transform.Rotate(0, 0, angle);
    }


    public void UpdateLineColldierPosition(LineRenderer line, Vector2 start, Vector2 end)
    {
        float lineWidth = line.endWidth;

        float lineLength = Vector2.Distance(start, end);

        lineCollider.size = new Vector2(lineLength, lineWidth);

        Vector2 midPoint = (start + end) / 2;

        lineCollider.transform.position = midPoint;


        float angle = Mathf.Atan2((end.y - start.y), (end.x - start.x));

        angle *= Mathf.Rad2Deg;

        angle *= -1;

        //lineCollider.transform.position = start + (end - start) / 2;
        //lineCollider.transform.LookAt(start);


        //lineCollider.transform.rotation = new Quaternion(0, 0, angle);
    }

    private IEnumerator TurnOffLine(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        MakeLineInVisible();
        StartCoroutine(TurnOnLine(switchTime));
    }

    private IEnumerator TurnOnLine(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        MakeLineVisible();
        StartCoroutine(TurnOffLine(switchTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SeansTestPlayerController.Instance.TakeDamage(1);
        }
    }
}
