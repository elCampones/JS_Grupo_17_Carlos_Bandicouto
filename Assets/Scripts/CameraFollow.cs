using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public GameObject windParticles;

    private bool reset = false;

    // Update is called once per frame
    void LateUpdate()
    {
        if (reset)
        {
            transform.position = new Vector3(transform.position.x, -1.78f, -10.06f);
            reset = false;
        }
        Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 velocity = Vector3.zero;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(target);
    }


    public void ResetCameraFollowPosition()
    {
        reset = true;
    }
}
