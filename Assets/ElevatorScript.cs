using System.Collections;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    [SerializeField]
    private float platformSpeed = 4f;

    public Transform endpoint;

    private bool hasEntered = false;

    private IEnumerator descent()
    {
        while (transform.position != endpoint.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, endpoint.position, platformSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BoxMcGuffin" && !hasEntered)
        {
            StartCoroutine(descent());
            hasEntered = true;
        }
    }
}
