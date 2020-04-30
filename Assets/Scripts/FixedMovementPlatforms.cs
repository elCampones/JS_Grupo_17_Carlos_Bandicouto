using System.Collections;
using UnityEngine;

public class FixedMovementPlatforms : MonoBehaviour
{
    [SerializeField]
    private Transform[] movementPoints = null;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float waitTime = .3f;

    private void Start() => StartCoroutine(FollowPath());

    private IEnumerator FollowPath()
    {
        int i = 0;
        while (true)
        {
            yield return StartCoroutine(MoveToDestination(movementPoints[i]));
            yield return new WaitForSeconds(waitTime);
            i = (i + 1) % movementPoints.Length;
        }
    }

    private IEnumerator MoveToDestination(Transform destination)
    {
        while (transform.position != destination.position)
        {
            Debug.Log("I'm here " + transform.position);
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            Debug.Log("Moving towards: " + Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime));
            yield return null;
        }
    }
}
