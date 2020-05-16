using System.Collections;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{

    [SerializeField]
    private float platformSpeed = 4f;

    [SerializeField]
    private float waitTime = 2f;

    public Transform endpoint;

    private bool hasEntered = false;

    private IEnumerator descent()
    {
        yield return new WaitForSeconds(waitTime);
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
