using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPlatform : MonoBehaviour
{

    [SerializeField]
    private float radius = 5f;

    [SerializeField]
    private float angularSpeed = 1f;

    [SerializeField]
    private Transform platform = null;

    [SerializeField]
    private bool isClockwise = true;

    private int moveDir;

    private float currTime;

    private void Start() => moveDir = isClockwise ? -1 : 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(moveDir);
        float zPos = transform.position.z + moveDir * radius * Mathf.Cos(angularSpeed * currTime);
        float yPos = transform.position.y + radius * Mathf.Sin(angularSpeed * currTime);
        platform.position = new Vector3(platform.position.x, yPos, zPos);

    }

    private void Update() => currTime += Time.deltaTime;
}
