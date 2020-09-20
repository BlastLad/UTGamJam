using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public float cameraSmoothing = 3.0f;
    public bool isInEvent = false;


    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position && !isInEvent)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, cameraSmoothing);
        }
        else
        {
            Debug.Log("no target on camera");
        }
    }

    public void SetPosInEvent()
    {
        transform.position = target.position;
        isInEvent = true;

    }
}
